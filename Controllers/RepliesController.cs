using Microsoft.AspNetCore.Mvc;
using OzMateApi.Models;
using OzMateApi.Entities;

namespace OzMateApi.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class RepliesController : ControllerBase
{
    private readonly ReplyService _replyService;

    public RepliesController(OzMateContext context)
    {
        _replyService = new ReplyService(context);
    }

    // GET: api/replies
    [HttpGet]
    public IActionResult GetAllReplies()
    {
        try
        {
            IEnumerable<ReplyModel> data = _replyService.GetReplies();
            return Ok(data);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // GET: api/replies/5
    [HttpGet("{id}")]
    public IActionResult GetReplyById(string id)
    {
        try
        {
            ReplyModel? data = _replyService.GetReplyById(id);

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

    // POST: api/replies
    [HttpPost]
    public IActionResult CreateReply([FromBody] ReplyModel reply)
    {
        try
        {
            _replyService.CreateReply(reply);
            return Ok(reply);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/replies/5
    [HttpPut("{id}")]
    public IActionResult UpdateReply(string id, [FromBody] ReplyModel reply)
    {
        try
        {
            _replyService.UpdateReply(id, reply);
            return Ok(reply);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // DELETE: api/replies/5
    [HttpDelete("{id}")]
    public IActionResult DeleteReply(string id)
    {
        try
        {
            _replyService.DeleteReply(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

