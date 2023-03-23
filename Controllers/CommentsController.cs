using Microsoft.AspNetCore.Mvc;
using OzMateApi.Models;
using OzMateApi.Entities;

namespace OzMateApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CommentsController : ControllerBase
{
    private readonly CommentService _commentService;
    private readonly PostService _postService;

    public CommentsController(OzMateContext context)
    {
        _commentService = new CommentService(context);
        _postService = new PostService(context);
    }

    // GET: api/comments
    [HttpGet]
    public IActionResult Get()
    {
        try
        {
            IEnumerable<CommentModel> data = _commentService.GetComments();
            return Ok(data);
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/comments/5
    [HttpGet("{id}", Name = "Get")]
    public IActionResult Get(string id)
    {
        try
        {
            CommentModel? data = _commentService.GetCommentById(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/comments
    [HttpPost]
    public IActionResult Post([FromBody] string postId, [FromBody] CommentModel comment)
    {
        try
        {
            PostModel? post = _postService.GetPostById(postId);

            if (post == null)
            {
                return BadRequest("Post not found.");
            }

            comment.PostId = postId;
            _commentService.CreateComment(comment);

            return Ok(comment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/comments/5
    [HttpPut("{id}")]
    public IActionResult Put(string id, [FromBody] CommentModel comment)
    {
        try
        {
            _commentService.UpdateComment(id, comment);
            return Ok(comment);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/Posts/5
    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        try
        {
            _commentService.DeleteComment(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(HttpResponseHandler.GetExceptionResponse(ex));
        }
    }
}


