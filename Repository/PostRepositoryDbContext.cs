using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository;

public class PostRepositoryDbContext : IPostRepo
{
    private readonly StudentBloggDbContext _dbContext;
    public PostRepositoryDbContext(StudentBloggDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Post?> AddPostAsync(Post post)
    {
        var postToAdd = await _dbContext.Posts.AddAsync(post);
        await _dbContext.SaveChangesAsync();

        if (postToAdd != null)
        {
            return postToAdd.Entity;
        }
        return null;
    }

    public async Task<Post?> DeletePostByIdAsync(int Id)
    {
        var postToDelete = await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == Id);
        if (postToDelete != null)
        {
            var entity = _dbContext.Posts.Remove(postToDelete);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }
        return null;
    }

    public async Task<Post?> GetPostByIdAsync(int Id)
    {
        return await _dbContext.Posts.FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task<ICollection<Post>> GetPostsAsync(int pageNr, int pageSize)
    {
        return await _dbContext.Posts
            .OrderBy(x => x.Id)
            .Skip((pageNr - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    public async Task<Post?> UpdatePostAsync(int Id, Post post)
    {
        await _dbContext.Posts.Where(u => u.Id == Id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(x => x.Id, post.Id)
            .SetProperty(x => x.UserId, post.UserId)
            .SetProperty(x => x.Title, post.Title)
            .SetProperty(x => x.Content, post.Content)
            .SetProperty(x => x.Updated, DateTime.Now));
        await _dbContext.SaveChangesAsync();

        var postToUpdate = await _dbContext.Posts.FirstOrDefaultAsync(u => u.Id == Id);
        if (postToUpdate == null) return null;

        return postToUpdate;
    }
}
