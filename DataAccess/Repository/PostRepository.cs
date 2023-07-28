using OzMateApi.DataAccess;
using OzMateApi.Models;
using OzMateApi.DataAccess.Repository.IRepository;

namespace OzMateApi.DataAccess.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private OzMateDbContext _db;
        public PostRepository(OzMateDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Post obj)
        {
            _db.Posts.Update(obj);

        }
    }
}

