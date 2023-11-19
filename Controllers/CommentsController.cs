using Microsoft.AspNetCore.Mvc;
using StudentBloggAPI.Models.DTOs;
using StudentBloggAPI.Services.Interfaces;

namespace StudentBloggAPI.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet(Name = "GetAllComments")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetCommentsAsync(int pageNr = 1, int pageSize = 10)
    {
        return Ok(await _commentService.GetAllCommentsAsync(pageNr, pageSize));
    }
    
    [HttpPost("{postId}", Name = "AddComment")]
    public async Task<ActionResult<CommentDTO>> AddCommentAsync(int postId, CommentDTO commentDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var commentToAdd = await _commentService.AddCommentAsync(postId, commentDTO);
        if (commentToAdd == null)
        {
            return BadRequest("Kunne ikke legge til kommentar");
        }

        return Ok(commentToAdd);
    }
    
    [HttpPut("{commentId}", Name = "UpdateComment")]
    public async Task<ActionResult<CommentDTO>> UpdateCommentAsync(int commentId, CommentDTO commentDTO)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var commentToUpdate = await _commentService.UpdateCommentAsync(commentId, commentDTO);
        if (commentToUpdate == null)
        {
            return NotFound("Fant ikke kommentar");
        }
        else if (commentToUpdate.UserId != (int)HttpContext.Items["UserId"]!)
        {
            return Unauthorized("Ingen tilgang");
        }
        return Ok(commentToUpdate);
    }

    [HttpDelete("{commentId}", Name = "DeleteComment")]
    public async Task<ActionResult<CommentDTO>> DeleteCommentAsync(int commentId)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        var commentToDelete = await _commentService.DeleteCommentAsync(commentId);
        if (commentToDelete == null)
        {
            return NotFound("Fant ikke kommentar");
        }
        else if (commentToDelete.UserId != (int)HttpContext.Items["UserId"]!)
        {
            return Unauthorized("Ingen tilgang");
        }
        return Ok(commentToDelete);
    }
}
