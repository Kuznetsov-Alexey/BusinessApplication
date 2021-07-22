using BusinessApplication.DAL.Contracts.Enteties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessApplication.Web.ViewModels
{
	public class CreateTaskViewModel
	{
		public int? ParentTaskId { get; set; }

		[Required(ErrorMessage ="TaskNameRequired")]
		[Display(Name ="TaskName")]
		public string TaskName { get; set; }

		[Required(ErrorMessage ="DescriptionRequired")]
		[DataType(DataType.MultilineText)]
		[Display(Name ="Description")]
		public string Description { get; set; }

		[Required(ErrorMessage ="ExecutorsRequired")]
		[Display(Name ="Executors")]
		public string Executors { get; set; }	

		[Display(Name ="LaborPlan")]		
		public int LaborPlan { get; set; }		
	}
}
