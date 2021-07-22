using BusinessApplication.Domain.Contracts.Enums;
using BusinessApplication.Domain.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessApplication.Domain.Contracts.Interfaces
{
	public interface ITaskManager
	{
		IEnumerable<TaskStatusEnum> GetAvailableStatuses(TaskStatusEnum currentStatus);
		Task<TaskItemModel> GetTaskById(int taskId);
		Task CreateTask(TaskItemModel taskModel);
		Task UpdateTask(TaskItemModel taskModel);
		Task DeleteTaskItem(int taskId);
	}
}
