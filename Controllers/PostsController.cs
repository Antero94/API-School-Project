using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet(Name = "GetPosts")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsAsync()
    {
        return Ok(await _postService.GetAllPostsAsync());
    }

    [HttpGet("{postId}", Name = "GetPostId")]
    public async Task<ActionResult<string>> GetPostById(int Id)
    {
        var post = await _postService.GetPostsByIdAsync(Id);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpPost(Name = "AddPost")]
    public async Task<ActionResult<PostDTO>> AddPostAsync(PostDTO postDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var postToAdd = await _postService.AddPostAsync(postDTO);
        if (postToAdd == null)
        {
            return BadRequest("Kunne ikke legge til bruer");
        }
        return Ok(postToAdd);
    }

    [HttpPut("{postId}", Name = "UpdatePost")]
    public async Task<ActionResult<PostDTO>> UpdatePostAsync(int Id, PostDTO postDTO)
    {
        var postToUpdate = await _postService.UpdatePostAsync(Id, postDTO);
        if (postToUpdate == null)
        {
            return NotFound();
        }
        return Ok(postToUpdate);
    }

    [HttpDelete("{postId}", Name = "DeletePost")]
    public async Task<ActionResult<PostDTO>> DeletePost(int Id)
    {
        var postToDelete = await _postService.DeletePostAsync(Id);
        if (postToDelete == null)
        {
            return NotFound();
        }
        return Ok(postToDelete);
    }
}
