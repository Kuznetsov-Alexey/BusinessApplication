using BusinessApplication.Domain.Contracts.Enums;
using BusinessApplication.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessApplication.Web.ViewModels
{
	public class ShowTaskViewModel
	{
		public int Id { get; set; }

		[Display(Name = "TaskName")]
		public string TaskName { get; set; }
		[Display(Name ="Description")]
		public string Description { get; set; }

		[Display(Name ="Executors")]
		public string Executors { get; set; }
		
		[Display(Name = "CreationDate")]
		[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
		public DateTime CreationDate { get; set; }

		[Display(Name ="Status")]
		public TaskStatusEnum Status { get; set; }

		[Display(Name = "LaborPlan")]
		public int LaborPlan { get; set; }

		[Display(Name = "LaborFact")]
		public int LaborFact { get; set; }

		[Display(Name = "FinishDate")]
		[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
		public DateTime FinishDate { get; set; }

		public List<TaskItemModel> ChildTasks { get; set; }

		[Display(Name = "TotalLaborPlan")]
		public int TotalLaborPlan { get; set; }

		[Display(Name = "TotalLaborFact")]
		public int TotalLaborFact { get; set; }
	}
}
