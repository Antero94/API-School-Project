using StudentBloggAPI.Models.Entities;

namespace StudentBloggAPI.Repository.Interfaces;

public interface ICommentRepo
{
    Task<Comment?> AddCommentAsync(Comment comment);
    Task<Comment?> UpdateCommentAsync(int Id, Comment comment);
    Task<Comment?> DeleteCommentByIdAsync(int Id);
    Task<Comment?> GetCommentByIdAsync(int Id);
    Task<ICollection<Comment>> GetCommentsAsync(int pageNr, int pageSize);
}
