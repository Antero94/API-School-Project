using Microsoft.AspNetCore.Http.HttpResults;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository.InMemoryDb;

public class PostRepoInMemory : IPostRepo
{
    private int _lastId = 0;
    private readonly List<Post> _posts = new List<Post>();

    public async Task<Post?> AddPostAsync(Post post)
    {
        await Task.Delay(10);
        _lastId++;
        post.Id = _lastId;
        _posts.Add(post);
        return post;
    }

    public async Task<Post?> DeletePostByIdAsync(int Id)
    {
        await Task.Delay(10);
        var postToDelete = _posts.FirstOrDefault(x => x.Id == Id);
        if (postToDelete != null)
        {
            _posts.Remove(postToDelete);
        }
        return null;
    }

    public async Task<ICollection<Post>> GetPostsAsync(int pageNr, int pageSize)
    {
        await Task.Delay(10);
        return _posts;
    }

    public async Task<Post?> GetPostByIdAsync(int Id)
    {
        await Task.Delay(10);
        var post = _posts.FirstOrDefault(posts => posts.Id == Id);
        if (post != null)
        {
            return post;
        }
        return null;
    }

    public async Task<Post?> UpdatePostAsync(int Id, Post post)
    {
        await Task.Delay(10);
        var postToUpdate = _posts.FirstOrDefault(post => post.Id == Id);
        if (postToUpdate != null && post != null)
        {
            postToUpdate.Title = post.Title;
            postToUpdate.Content = post.Content;
            return post;
        }
        return null;
    }
}
