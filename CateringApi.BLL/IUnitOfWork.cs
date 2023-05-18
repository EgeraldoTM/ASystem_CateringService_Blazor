namespace CateringApi.BLL
{
	public interface IUnitOfWork
	{
		Task CompleteAsync();
	}
}
