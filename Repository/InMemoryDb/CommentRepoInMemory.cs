using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository.InMemoryDb;

public class CommentRepoInMemory : ICommentRepo
{
    private int _lastId = 0;
    private readonly List<Comment> _comments = new List<Comment>();
    public async Task<Comment?> AddCommentAsync(Comment comment)
    {
        await Task.Delay(10);
        _lastId++;
        comment.Id = _lastId;
        _comments.Add(comment);
        return comment;
    }

    public async Task<Comment?> DeleteCommentByIdAsync(int Id)
    {
        await Task.Delay(10);
        var commentToDelete = _comments.FirstOrDefault(c => c.Id == Id);
        if (commentToDelete != null)
        {
            _comments.Remove(commentToDelete);
        }
        return null;
    }

    public async Task<Comment?> GetCommentByIdAsync(int Id)
    {
        await Task.Delay(10);
        var comment = _comments.FirstOrDefault(c => c.Id == Id);
        if (comment != null)
        {
            return comment;
        }
        return null;
    }

    public async Task<ICollection<Comment>> GetCommentsAsync()
    {
        await Task.Delay(10);
        return _comments;
    }

    public async Task<Comment?> UpdateCommentAsync(int Id, Comment comment)
    {
        await Task.Delay(10);
        var commentToUpdate = _comments.FirstOrDefault(c => c.Id == Id);
        if (commentToUpdate != null && comment != null)
        {
            commentToUpdate.Content = comment.Content;
            return comment;
        }
        return null;
    }
}
