using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.ViewModels
{
	public class ConfirmDeleteViewModel
	{
		public int Id { get; set; }

		[Display(Name = "TaskName")]
		public string TaskName { get; set; }
	}
}
