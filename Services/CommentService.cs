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
    private readonly IHttpContextAccessor _contextAccessor;
    public CommentService(IMapper<Comment, CommentDTO> commentMapper, ICommentRepo commentrepo, IHttpContextAccessor contextAccessor)
    {
        _commentMapper = commentMapper;
        _commentRepo = commentrepo;
        _contextAccessor = contextAccessor;

    }
    public async Task<CommentDTO?> AddCommentAsync(int Id, CommentDTO commentDTO)
    {
        var commentToAdd = _commentMapper.MapToModel(commentDTO);
        commentToAdd.PostId = Id;
        commentToAdd.UserId = (int)_contextAccessor.HttpContext!.Items["UserId"]!;
        var res = await _commentRepo.AddCommentAsync(commentToAdd);

        if (res == null)
        {
            return null; 
        }
        return commentDTO;
    }

    public async Task<CommentDTO?> DeleteCommentAsync(int Id)
    {
        var commentToDelete = await _commentRepo.GetCommentByIdAsync(Id);
        if (commentToDelete == null)
        {
            return null;
        }

        if (commentToDelete.UserId == (int)_contextAccessor.HttpContext!.Items["UserId"]!)
        {
            var res = await _commentRepo.DeleteCommentByIdAsync(Id);
            return res != null ? _commentMapper.MapToDTO(commentToDelete) : null;
        }
        return _commentMapper.MapToDTO(commentToDelete);
    }

    public async Task<ICollection<CommentDTO>> GetAllCommentsAsync(int pageNr, int pageSize)
    {
        var comments = await _commentRepo.GetCommentsAsync(pageNr, pageSize);
        var dto = comments.Select(_commentMapper.MapToDTO).ToList();

        return dto;
    }

    public async Task<CommentDTO?> GetCommentByIdAsync(int Id)
    {
        var comment = await _commentRepo.GetCommentByIdAsync(Id);
        if (comment == null)
        {
            return null;
        }
        return _commentMapper.MapToDTO(comment);
    }

    public async Task<CommentDTO?> UpdateCommentAsync(int Id, CommentDTO commentDTO)
    {
        var commentToUpdate = await _commentRepo.GetCommentByIdAsync(Id);
        if (commentToUpdate == null)
        {
            return null;
        }

        if (commentToUpdate.UserId == (int)_contextAccessor.HttpContext!.Items["UserId"]!)
        {
            var commentUpdated = _commentMapper.MapToModel(commentDTO);
            commentUpdated.Id = Id;
            commentUpdated.PostId = commentToUpdate.PostId;
            commentUpdated.UserId = (int)_contextAccessor.HttpContext!.Items["UserId"]!;

            var res = await _commentRepo.UpdateCommentAsync(Id, commentUpdated);
            return res != null ? _commentMapper.MapToDTO(commentUpdated) : null;
        }
        return _commentMapper.MapToDTO(commentToUpdate);
    }
}
