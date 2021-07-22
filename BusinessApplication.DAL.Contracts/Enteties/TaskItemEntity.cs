using BusinessApplication.DAL.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessApplication.DAL.Contracts.Enteties
{
	public class TaskItemEntity : IEntity
	{
		public int Id { get; set; }
		public string TaskName { get; set; }
		public string Description { get; set; }
		public string Executors { get; set; }
		public DateTime CreationDate { get; set; }
		public TaskItemStatus Status { get; set; }
		public int LaborPlan { get; set; }
		public int LaborFact { get; set; }
		public DateTime FinishDate { get; set; }
		public TaskItemEntity ParentTask { get; set; }
		public int? ParentTaskId { get; set; }
		public List<TaskItemEntity> ChildTasks { get; set; }
	}
}
