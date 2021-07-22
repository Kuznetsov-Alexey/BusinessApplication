using BusinessApplication.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApplication.Web.ViewModels
{
	public class TreeListViewModel
	{
		public int? ParentId { get; set; }
		public List<TaskTreeElementModel> Tasks { get; set; }
	}
}
