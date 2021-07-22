using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApplication.Domain.Contracts.Models
{
	public class TaskTreeElementModel
	{
		public int Id { get; set; }
		public string TaskName { get; set; }
		public int? ParentId { get; set; }
	}
}
