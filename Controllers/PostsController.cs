using Microsoft.AspNetCore.Mvc;
using OzMateApi.Models;
using OzMateApi.Entities;

namespace OzMateApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class PostsController : ControllerBase
{
    private readonly PostService _postService;

    public PostsController(OzMateContext context)
    { 
        _postService = new PostService(context);
    }

    // GET: api/posts
    [HttpGet]
    public IActionResult GetAllPosts()
    {
        try
        {
            IEnumerable<PostModel> data = _postService.GetPosts();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/posts/5
    [HttpGet("{id}")]
    public IActionResult GetPostById(string id)
    {
        try
        {
            PostModel? data = _postService.GetPostById(id);
       
            if(data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // POST: api/posts
    [HttpPost]
    public IActionResult CreatePost([FromBody] PostModel post)
    {
        try
        {
            _postService.CreatePost(post);
            return Ok(post);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/posts/5
    [HttpPut("{id}")]
    public IActionResult UpdatePost(string id, [FromBody] PostModel post)
    {
        try
        {
            _postService.UpdatePost(id, post);
            return Ok(post);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/posts/5
    [HttpDelete("{id}")]
    public IActionResult DeletePost(string id)
    {
        try
        {
            _postService.DeletePost(id);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/posts/5/comments
    [HttpGet("{id}/comments")]
    public IActionResult GetPostComments(string id)
    {
        try
        {
            IEnumerable<CommentModel> data = _postService.GetPostComments(id);
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}

