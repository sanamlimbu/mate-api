using OzMateApi.DataAccess;
using OzMateApi.Models;
using OzMateApi.DataAccess.Repository.IRepository;

namespace OzMateApi.DataAccess.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private OzMateDbContext _db;
        public CommentRepository(OzMateDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Comment obj)
        {
            _db.Comments.Update(obj);

        }
    }
}

