using Microsoft.AspNetCore.Mvc;
using OzMateApi.DataAccess.Repository.IRepository;
using OzMateApi.Models;

namespace OzMateApi.Controllers
{

    public class CreatePostRequest
    {
        public string Content { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string FirebasePhotoURL { get; set; }
        public bool EmailVerified { get; set; }
    }

    public class UpdatePostRequest
    {
        public string FirebaseUid { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string FirebasePhotoURL { get; set; }
        public bool EmailVerified { get; set; }
    }

    public class PostDTO: BaseModel
    {
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<Media>? Medias { get; set; }
    }


    [ApiController]
    [Route("api/posts/")]
    public class PostsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/posts
        [HttpGet]
        public IActionResult GetAllPosts()
        {
            try
            {
                IEnumerable<Post> data = _unitOfWork.Post.GetAll(includeProperties:"User");
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong and cannot fetch.");
            }
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public IActionResult GetPostById(string id)
        {
            try
            {
                var postGuid = new Guid(id);
                var post = _unitOfWork.Post.Get(e => e.Id.Equals(postGuid),includeProperties: "User");

                if (post == null)
                {
                    return NotFound();
                }
                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong and cannot fetch.");
            }
        }

        // POST: api/posts
        [HttpPost]
        //public IActionResult CreatePost([FromForm] CreatePostRequest request)
        //{
        //    try
        //    {
        //        PostModel post = new PostModel();
        //        post.Content = postDto.Message;

        //        foreach (var file in postDto.Files)
        //        {
        //            // Save the file, or do whatever you need to do with it
        //            // For example, you can use System.IO to save the file to the server
        //            // using (var stream = new FileStream(Path.Combine(uploadPath, file.FileName), FileMode.Create))
        //            // {
        //            //     file.CopyTo(stream);
        //            // }
        //        }
        //        _postService.CreatePost(post);
        //        return Ok(post);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong and cannot create.");
        //    }
        //}

        // PUT: api/posts/5
        [HttpPut("{id}")]
        //public IActionResult UpdatePost(string id, [FromBody] UpdatePostRequest request)
        //{
        //    try
        //    {
        //        var post = _unitOfWork.Post.Get(e => e.Equals(id));
        //        if(post == null)
        //        {
        //            return NotFound();
        //        }
        //        _unitOfWork.UpdatePost(id, post);
        //        return Ok(post);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong and cannot update.");
        //    }
        //}

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public IActionResult DeletePost(string id)
        {
            try
            {
                var post = _unitOfWork.Post.Get(e => e.Equals(id));
                if(post == null)
                {
                    return NotFound();
                }
                _unitOfWork.Post.Remove(post);
                _unitOfWork.Save();
;                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong and cannot delete.");
            }
        }

        // GET: api/posts/5/comments
        [HttpGet("{id}/comments")]
        public IActionResult GetPostComments(string id)
        {
            try
            {
                var postGuid = new Guid(id);
                var post = _unitOfWork.Post.Get(e => e.Id.Equals(postGuid));
                if (post == null)
                {
                    return NotFound();
                }

                var commentRepository = _unitOfWork.Comment;

                IEnumerable<Comment> comments = commentRepository.GetAll(e => e.PostId.Equals(postGuid), includeProperties:"User,Replies");
                return Ok(comments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Something went wrong and cannot fetch.");
            }
        }



    }

}