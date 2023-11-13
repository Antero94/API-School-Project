using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Services;

public class CommentService : ICommentService
{
    private readonly IMapper<Comment, CommentDTO> _commentMapper;
    private readonly ICommentRepo _commentRepo;
    public CommentService(IMapper<Comment, CommentDTO> commentMapper, ICommentRepo commentrepo)
    {
        _commentMapper = commentMapper;
        _commentRepo = commentrepo;
    }
    public Task<CommentDTO?> AddCommentAsync(CommentDTO commentDTO)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDTO?> DeleteCommentAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<ICollection<CommentDTO>> GetAllCommentsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CommentDTO?> GetCommentsByIdAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<CommentDTO?> UpdateCommentAsync(int Id, CommentDTO commentDTO)
    {
        throw new NotImplementedException();
    }
}
