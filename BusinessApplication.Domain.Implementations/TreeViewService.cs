using BusinessApplication.DAL.Contracts;
using BusinessApplication.DAL.Contracts.Enteties;
using BusinessApplication.Domain.Contracts;
using BusinessApplication.Domain.Contracts.Interfaces;
using BusinessApplication.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessApplication.Domain.Implementations
{
	public class TreeViewService : ITreeViewService
	{
		private readonly IDbRepository _repository;

		public TreeViewService(IDbRepository repository)
		{
			_repository = repository;
		}
		
		public List<TaskTreeElementModel> GetTreeList()
		{
			var treeList = _repository.GetAllElements<TaskItemEntity>().Select(t =>
				new TaskTreeElementModel { Id = t.Id, ParentId = t.ParentTaskId, TaskName = t.TaskName }).ToList();

			return treeList;
		}		
	}
}
