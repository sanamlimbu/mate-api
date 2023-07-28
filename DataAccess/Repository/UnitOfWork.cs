using OzMateApi.DataAccess.Repository.IRepository;

namespace OzMateApi.DataAccess.Repository
{
	public class UnitOfWork: IUnitOfWork
	{
        private OzMateDbContext _db;
        public IUserRepository User { get; private set; }
        public IPostRepository Post { get; private set; }
        public ICommentRepository Comment { get; private set; }
        public IReplyRepository Reply { get; private set; }


        public UnitOfWork(OzMateDbContext db)
		{
            _db = db;
            User = new UserRepository(_db);
            Post = new PostRepository(_db);
            Comment = new CommentRepository(_db);
            Reply = new ReplyRepository(_db);
		}
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

