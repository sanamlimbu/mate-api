using OzMateApi.DataAccess;
using OzMateApi.Models;
using OzMateApi.DataAccess.Repository.IRepository;

namespace OzMateApi.DataAccess.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private OzMateDbContext _db;
        public UserRepository(OzMateDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User obj)
        {
            _db.Users.Update(obj);

        }
    }
}

