using OzMateApi.Models;

namespace OzMateApi.DataAccess.Repository.IRepository
{
    public interface IReplyRepository : IRepository<Reply>
    {
        void Update(Reply obj);
    }
}

