using StudentBloggAPI.Mappers.Interfaces;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;
using StudentBloggAPI.Services.Interfaces;


namespace StudentBloggAPI.Services;

public class PostService : IPostService
{
    private readonly IMapper<Post, PostDTO> _postMapper;
    private readonly IPostRepo _postRepo;
    private readonly IHttpContextAccessor _contextAccessor;

    public PostService(IMapper<Post, PostDTO> postMapper, IPostRepo postRepo, IHttpContextAccessor httpContextAccessor)
    {
        _postMapper = postMapper;
        _postRepo = postRepo;
        _contextAccessor = httpContextAccessor;
    }

    public async Task<PostDTO?> AddPostAsync(PostDTO postDTO)
    {
        var postToAdd = _postMapper.MapToModel(postDTO);
        postToAdd.UserId = (int)_contextAccessor.HttpContext!.Items["UserId"]!;
        var res = await _postRepo.AddPostAsync(postToAdd);

        if (res == null)
        {
            return null;
        }
        return postDTO;
    }

    public async Task<PostDTO?> DeletePostAsync(int Id)
    {
        var postToDelete = await _postRepo.GetPostByIdAsync(Id);
        if (postToDelete == null)
        {
            return null;
        }

        if (postToDelete.UserId == (int)_contextAccessor.HttpContext!.Items["UserId"]!)
        {
            var res = await _postRepo.DeletePostByIdAsync(Id);
            return res != null ? _postMapper.MapToDTO(postToDelete) : null;
        }
        return _postMapper.MapToDTO(postToDelete);
    }

    public async Task<ICollection<PostDTO>> GetAllPostsAsync(int pageNr = 1, int pageSize = 10)
    {
        var posts = await _postRepo.GetPostsAsync(pageNr, pageSize);
        var dto = posts.Select(_postMapper.MapToDTO).ToList();

        return dto;
    }

    public async Task<PostDTO?> GetPostByIdAsync(int Id)
    {
        var post = await _postRepo.GetPostByIdAsync(Id);
        if (post == null)
        {
            return null;
        }
        return _postMapper.MapToDTO(post);
    }

    public async Task<PostDTO?> UpdatePostAsync(int Id, PostDTO postDTO)
    {
        var postToUpdate = await _postRepo.GetPostByIdAsync(Id);
        if (postToUpdate == null)
        {
            return null;
        }

        if (postToUpdate.UserId == (int)_contextAccessor.HttpContext!.Items["UserId"]!)
        {
            var postUpdated = _postMapper.MapToModel(postDTO);
            postUpdated.Id = Id;
            postUpdated.UserId = (int)_contextAccessor.HttpContext!.Items["UserId"]!;

            var res = await _postRepo.UpdatePostAsync(Id, postUpdated);
            return res != null ? _postMapper.MapToDTO(postUpdated) : null;
        }
        return _postMapper.MapToDTO(postToUpdate);
    }
}
