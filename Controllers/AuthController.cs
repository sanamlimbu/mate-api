using System;
using Microsoft.AspNetCore.Mvc;
using OzMateApi.DataAccess;
using OzMateApi.DataAccess.Repository.IRepository;

namespace OzMateApi.Controllers
{
    public class CreateUserRequest
    {
        public string FirebaseUid { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string FirebasePhotoURL { get; set; }
        public bool EmailVerified { get; set; }
    }

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("api/auth/login")]
        [HttpPost]
        public IActionResult LoginUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var user = _unitOfWork.User.Get(e => e.Id.Equals(request.FirebaseUid));
                if (user == null)
                {
                    user.FirebaseUid = request.FirebaseUid;
                    user.DisplayName = request.DisplayName;
                    user.Email = request.Email;
                    user.EmailVerified = request.EmailVerified;
                    user.FirebasePhotoURL = request.FirebasePhotoURL;
                    _unitOfWork.User.Add(user);
                    _unitOfWork.Save();
                }
                else
                {
                    user.FirebaseUid = request.FirebaseUid;
                    user.DisplayName = request.DisplayName;
                    user.Email = request.Email;
                    user.EmailVerified = request.EmailVerified;
                    user.FirebasePhotoURL = request.FirebasePhotoURL;
                    _unitOfWork.User.Update(user);
                    _unitOfWork.Save();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}



