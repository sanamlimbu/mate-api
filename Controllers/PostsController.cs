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
        _postService = new (context);
    }

    // GET: api/posts
    [HttpGet]
    public IActionResult Get()
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
    [HttpGet("{id}", Name = "Get")]
    public IActionResult Get(string id)
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
    public IActionResult Post([FromBody] PostModel post)
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
    public IActionResult Put(string id, [FromBody] PostModel post)
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
    public IActionResult Delete(string id)
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
}

