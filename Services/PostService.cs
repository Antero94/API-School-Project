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

    public PostService(IMapper<Post, PostDTO> postMapper, IPostRepo postRepo)
    {
        _postMapper = postMapper;
        _postRepo = postRepo;
    }

    public async Task<PostDTO?> AddPostAsync(PostDTO postDTO)
    {
        var post = _postMapper.MapToModel(postDTO);
        var res = await _postRepo.AddPostAsync(post);

        if (res == null)
        {
            return null;
        }
        return postDTO;
    }

    public async Task<PostDTO?> DeletePostAsync(int Id)
    {
        var post = await _postRepo.GetPostByIdAsync(Id);
        if (post == null)
        {
            return null;
        }
        var res = await _postRepo.DeletePostByIdAsync(Id);
        if (res == null)
        {
            return null;
        }
        return _postMapper.MapToDTO(post);
    }

    public async Task<ICollection<PostDTO>> GetAllPostsAsync()
    {
        var posts = await _postRepo.GetPostsAsync();
        var dto = posts.Select(_postMapper.MapToDTO).ToList();

        return dto;
    }

    public async Task<PostDTO?> GetPostsByIdAsync(int Id)
    {
        var post = await _postRepo.GetPostByIdAsync(Id);
        if(post == null)
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
        var post = _postMapper.MapToModel(postDTO);
        post.Id = Id;

        var res = await _postRepo.UpdatePostAsync(Id, post);
        
        return res != null ? _postMapper.MapToDTO(post) : null;
    }
}
