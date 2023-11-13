using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ICollection<CommentDTO>> GetAllCommentsAsync();
        Task<CommentDTO?> AddCommentAsync(CommentDTO commentDTO);
        Task<CommentDTO?> UpdateCommentAsync(int Id, CommentDTO commentDTO);
        Task<CommentDTO?> DeleteCommentAsync(int Id);
        Task<CommentDTO?> GetCommentsByIdAsync(int Id);
    }
}
