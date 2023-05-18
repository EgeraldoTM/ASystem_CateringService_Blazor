using CateringApi.BLL.Repositories.Interfaces;
using CateringApi.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CateringApi.BLL.Repositories
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{
		protected readonly ApplicationDbContext _context;

		public Repository(ApplicationDbContext context)
        {
			_context = context;
		}

		public async Task<TEntity?> GetAsync(int id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}
		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			return await _context.Set<TEntity>().ToListAsync();
		}
		public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
		{
			return await _context.Set<TEntity>().Where(predicate).ToListAsync();
		}
		public void Add(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);
		}
		public void Delete(TEntity entity)
		{
			_context.Set<TEntity>().Remove(entity);
		}
	}
}
