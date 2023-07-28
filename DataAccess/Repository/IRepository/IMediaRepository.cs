using OzMateApi.Models;

namespace OzMateApi.DataAccess.Repository.IRepository
{
    public interface IMediaRepository : IRepository<Media>
    {
        void Update(Media obj);
    }
}



