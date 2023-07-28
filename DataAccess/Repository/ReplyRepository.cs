using OzMateApi.DataAccess;
using OzMateApi.Models;
using OzMateApi.DataAccess.Repository.IRepository;

namespace OzMateApi.DataAccess.Repository
{
    public class ReplyRepository : Repository<Reply>, IReplyRepository
    {
        private OzMateDbContext _db;
        public ReplyRepository(OzMateDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Reply obj)
        {
            _db.Replies.Update(obj);

        }
    }
}

