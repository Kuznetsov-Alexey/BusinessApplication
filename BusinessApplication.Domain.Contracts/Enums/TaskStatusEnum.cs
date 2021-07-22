using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BusinessApplication.Domain.Contracts.Enums
{
	public enum TaskStatusEnum
	{
		Assigned,			
		InProccess,		
		Stopped,		
		Finished
	}
}
