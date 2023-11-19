using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ICommentService _commentService;
    public PostsController(IPostService postService, ICommentService commentService)
    {
        _postService = postService;
        _commentService = commentService;
    }

    [HttpGet(Name = "GetAllPosts")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPostsAsync(int pageNr = 1, int pageSize = 10)
    {
        return Ok(await _postService.GetAllPostsAsync(pageNr, pageSize));
    }

    [HttpGet("{postId}", Name = "GetPostById")]
    public async Task<ActionResult<PostDTO>> GetPostByIdAsync(int postId)
    {
        var post = await _postService.GetPostByIdAsync(postId);
        if (post == null)
        {
            return NotFound("Fant ikke post");
        }
        return Ok(post);
    }

    [HttpGet("{postId}/comments", Name = "GetPostComments")]
    public async Task<ActionResult<CommentDTO>> GetPostCommentsAsync(int postId, int pageNr = 1, int pageSize = 10)
    {
        var postComments = await _commentService.GetAllCommentsAsync(pageNr, pageSize);
        var dto = postComments.Where(x => x.PostId == postId).ToList();

        if (dto == null)
        {
            return NotFound("Fant ikke kommentarer");
        }
        return Ok(dto);
    }

    [HttpPost(Name = "AddPost")]
    public async Task<ActionResult<PostDTO>> AddPostAsync(PostDTO postDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var postToAdd = await _postService.AddPostAsync(postDTO);
        if (postToAdd == null)
        {
            return BadRequest("Kunne ikke legge til post");
        }
        return Ok(postToAdd);
    }

    [HttpPut("{postId}", Name = "UpdatePost")]
    public async Task<ActionResult<PostDTO>> UpdatePostAsync(int postId, PostDTO postDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var postToUpdate = await _postService.UpdatePostAsync(postId, postDTO);
        if (postToUpdate == null)
        {
            return NotFound("Fant ikke post");
        }
        else if (postToUpdate.UserId != (int)HttpContext.Items["UserId"]!)
        {
            return Unauthorized("Ingen tilgang");
        }
        return Ok(postToUpdate);
    }

    [HttpDelete("{postId}", Name = "DeletePost")]
    public async Task<ActionResult<PostDTO>> DeletePostAsync(int postId)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var postToDelete = await _postService.DeletePostAsync(postId);
        if (postToDelete == null)
        {
            return NotFound("Fant ikke post");
        }
        else if (postToDelete.UserId != (int)HttpContext.Items["UserId"]!)
        {
            return Unauthorized("Ingen tilgang");
        }
        return Ok(postToDelete);
    }
}
