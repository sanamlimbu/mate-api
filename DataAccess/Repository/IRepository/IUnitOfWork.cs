namespace OzMateApi.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IUserRepository User { get; }
		IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        void Save();
	}
}

