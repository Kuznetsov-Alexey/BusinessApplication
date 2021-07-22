using BusinessApplication.DAL.Contracts.Enteties;
using BusinessApplication.Domain.Contracts.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessApplication.Web.ViewModels
{
	public class EditTaskViewModel
	{
		public int Id { get; set; }

		[Required]
		[Display(Name = "TaskName")]
		public string TaskName { get; set; }

		[Required]
		[DataType(DataType.MultilineText)]
		[Display(Name ="Description")]
		public string Description { get; set; }

		[Required]
		[Display(Name ="Executors")]
		public string Executors { get; set; }

		[Display(Name ="Status")]
		public TaskStatusEnum Status { get; set; }

		[Required]
		[Display(Name = "LaborPlan")]
		public int LaborPlan { get; set; }

		[Display(Name = "LaborFact")]
		public int LaborFact { get; set; }
				
		public IEnumerable<SelectListItem> AvailableStatuses { get; set; }
	}
}
