using OzMateApi.Models;

namespace OzMateApi.DataAccess.Repository.IRepository
{
    public interface IPostRepository : IRepository<Post>
    {
        void Update(Post obj);
    }
}

