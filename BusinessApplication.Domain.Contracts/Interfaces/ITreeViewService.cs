using BusinessApplication.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApplication.Domain.Contracts.Interfaces
{
	public interface ITreeViewService
	{
		List<TaskTreeElementModel> GetTreeList();
	}
}
