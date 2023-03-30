using System;
using OzMateApi.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OzMateApi.Models
{
    public class UserModel
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? GoogleId { get; set; }
        public string? FacebookId { get; set; }
        public string? Gender { get; set; }
        public string? Location { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }

    public class UserService: DbService
    {
        public UserService(OzMateContext context)
            :base(context)
        {
        }

        // CRUD Users
        public List<UserModel> GetUsers()
        {
            List<UserModel> resp = new List<UserModel>();
            var dataList = _context.Users.ToList();

            dataList.ForEach(row => resp.Add(
                new UserModel()
                {
                    Id = row.Id.ToString(),
                    Name = row.Name,
                    GoogleId = row.GoogleId,
                    FacebookId = row.FacebookId,
                    Gender = row.Gender,
                    CreatedAt = row.CreatedAt,
                    UpdatedAt = row.UpdatedAt,
                    DeletedAt = row.DeletedAt,
                }
                )); ;

            return resp;
        }

        public UserModel? GetUserById(string id)
        {
            var guid = new Guid(id);
            UserModel user = new UserModel();
            var data = _context.Users.Where(d => d.Id.Equals(guid)).FirstOrDefault();

            if (data != null)
            {
                user.Id = data.Id.ToString();
                user.Name = data.Name;
                user.Gender = data.Gender;
                user.GoogleId = data.GoogleId;
                user.FacebookId = data.FacebookId;
                user.CreatedAt = data.CreatedAt;
                user.UpdatedAt = data.UpdatedAt;
                user.DeletedAt = data.DeletedAt;

                return user;
            }
            return null;
        }

        public void CreateUser(UserModel model)
        {
            User user = new User();

            user.Name = model.Name;
            user.Gender = model.Gender;
            user.GoogleId = model.GoogleId;
            user.FacebookId = model.FacebookId;

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(string id, UserModel model)
        {
            var guid = new Guid(id);
            var user = _context.Users.Where(u => u.Id.Equals(guid)).FirstOrDefault();

            if (user != null)
            {
                user.Name = model.Name;
                user.Gender = model.Gender;
                user.FacebookId = model.FacebookId;
                user.GoogleId = model.GoogleId;
                user.CreatedAt = model.CreatedAt;
                user.UpdatedAt = model.UpdatedAt;
                user.DeletedAt = model.DeletedAt;

                _context.SaveChanges();
            }
        }

        public void DeleteUser(string id)
        {
            var guid = new Guid(id);
            var user = _context.Users.Where(u => u.Id.Equals(guid)).FirstOrDefault();

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}


