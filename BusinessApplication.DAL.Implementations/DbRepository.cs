using BusinessApplication.DAL.Contracts;
using BusinessApplication.DAL.Contracts.Enteties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApplication.DAL.Implementations
{
	public class DbRepository : IDbRepository
	{
		private readonly MyDbContext _dbContext;

		public DbRepository(MyDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task DeleteById<T>(int id) where T : class, IEntity
		{
			var entity = await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
			await Task.Run(() => _dbContext.Set<T>().Remove(entity));

			await _dbContext.SaveChangesAsync();
		}

		public IQueryable<T> GetAllElements<T>() where T : class, IEntity
		{
			return _dbContext.Set<T>().AsQueryable();
		}

		public async Task<T> GetElementById<T>(int id) where T : class, IEntity
		{
			return await _dbContext.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
		}

		public async Task SaveChangesAsync()
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task Update<T>(T entity) where T : class, IEntity
		{
			await _dbContext.SaveChangesAsync();
		}

		public async Task AddElement<T>(T entity) where T : class, IEntity
		{
			await _dbContext.Set<T>().AddAsync(entity);
		}
	}
}
