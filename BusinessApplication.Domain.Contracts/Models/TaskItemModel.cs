using BusinessApplication.Domain.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BusinessApplication.Domain.Contracts.Models
{
	public class TaskItemModel
	{
		public int Id { get; set; }		
		public string TaskName { get; set; }
		public string Description { get; set; }
		public string Executors { get; set; }		
		public DateTime CreationDate { get; set; }
		public TaskStatusEnum Status { get; set; }		
		public int LaborPlan { get; set; }		
		public int LaborFact { get; set; }		
		public DateTime FinishDate { get; set; }
		public TaskItemModel ParentTask { get; set; }
		public int? ParentTaskId { get; set; }
		public List<TaskItemModel> ChildTasks { get; set; }


		//New fields in model		
		public int TotalLaborPlan { get; set; }		
		public int TotalLaborFact { get; set; }
	}
}
