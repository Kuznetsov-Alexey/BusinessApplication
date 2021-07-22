using BusinessApplication.DAL.Contracts.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApplication.DAL.Contracts
{
	public interface IDbRepository
	{
		IQueryable<T> GetAllElements<T>() where T : class, IEntity;
		
		Task<T> GetElementById<T>(int id) where T : class, IEntity;

		Task AddElement<T>(T entity) where T : class, IEntity;

		Task DeleteById<T>(int id) where T : class, IEntity;

		Task Update<T>(T entity) where T : class, IEntity;

		Task SaveChangesAsync();
	}
}
