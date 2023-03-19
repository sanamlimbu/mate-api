using Microsoft.AspNetCore.Mvc;
using OzMateApi.Models;

namespace OzMateApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class PostController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetPost")]
    public ActionResult<PostController> Get()
    {
       
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Post> Create(Post post)
    {
        post.Id = _
        return CreatedAtAction(nameof(GetById),)


    }


}

