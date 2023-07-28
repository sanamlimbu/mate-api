using OzMateApi.DataAccess;
using OzMateApi.Models;
using OzMateApi.DataAccess.Repository.IRepository;

namespace OzMateApi.DataAccess.Repository
{
    public class MediaRepository : Repository<Media>, IMediaRepository
    {
        private OzMateDbContext _db;
        public MediaRepository(OzMateDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Media obj)
        {
            _db.Media.Update(obj);

        }
    }
}

