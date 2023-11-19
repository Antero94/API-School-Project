using StudentBloggAPI.Models.DTOs;

namespace StudentBloggAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<ICollection<CommentDTO>> GetAllCommentsAsync(int pageNr, int pageSize);
        Task<CommentDTO?> AddCommentAsync(int Id, CommentDTO commentDTO);
        Task<CommentDTO?> UpdateCommentAsync(int Id, CommentDTO commentDTO);
        Task<CommentDTO?> DeleteCommentAsync(int Id);
        Task<CommentDTO?> GetCommentByIdAsync(int Id);
    }
}
