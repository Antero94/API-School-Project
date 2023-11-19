using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository;

public class CommentRepositoryDbContext : ICommentRepo
{
    private readonly StudentBloggDbContext _dbContext;
    public CommentRepositoryDbContext(StudentBloggDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment?> AddCommentAsync(Comment comment)
    {
        var commentToAdd = await _dbContext.Comments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();

        if (commentToAdd != null)
        {
            return commentToAdd.Entity;
        }
        return null;
    }

    public async Task<Comment?> DeleteCommentByIdAsync(int Id)
    {
        var commentToDelete = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == Id);
        if (commentToDelete != null)
        {
            var entity = _dbContext.Comments.Remove(commentToDelete);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }
        return null;
    }

    public async Task<Comment?> GetCommentByIdAsync(int Id)
    {
        return await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == Id);
    }

    public async Task<ICollection<Comment>> GetCommentsAsync(int pageNr, int pageSize)
    {
        return await _dbContext.Comments
            .OrderBy(c => c.Id)
            .Skip((pageNr - 1) * pageSize)
            .Take(pageSize).ToListAsync();
    }

    public async Task<Comment?> UpdateCommentAsync(int Id, Comment comment)
    {
        await _dbContext.Comments.Where(u => u.Id == Id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(x => x.Id, comment.Id)
            .SetProperty(x => x.PostId, comment.PostId)
            .SetProperty(x => x.UserId, comment.UserId)
            .SetProperty(x => x.Content, comment.Content)
            .SetProperty(x => x.Updated, DateTime.Now));
        await _dbContext.SaveChangesAsync();

        var commentToUpdate = await _dbContext.Comments.FirstOrDefaultAsync(u => u.Id == Id);
        if (commentToUpdate == null) return null;

        return commentToUpdate;
    }
}
