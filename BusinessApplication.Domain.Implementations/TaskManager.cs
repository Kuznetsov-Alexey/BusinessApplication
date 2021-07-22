using AutoMapper;
using BusinessApplication.DAL.Contracts;
using BusinessApplication.DAL.Contracts.Enteties;
using BusinessApplication.Domain.Contracts;
using BusinessApplication.Domain.Contracts.Enums;
using BusinessApplication.Domain.Contracts.Interfaces;
using BusinessApplication.Domain.Contracts.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Domain.Implementations
{
	public class TaskManager : ITaskManager
	{
		private readonly IDbRepository _repository;
		private readonly IMapper _mapper;

		public TaskManager(IDbRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task CreateTask(TaskItemModel taskModel)
		{
			TaskItemEntity entity = _mapper.Map<TaskItemEntity>(taskModel);
			await _repository.AddElement<TaskItemEntity>(entity);
			taskModel.Id = entity.Id;
			await _repository.SaveChangesAsync();			
		}

		public async Task DeleteTaskItem(int taskId)
		{
			var task = await _repository.GetAllElements<TaskItemEntity>().Include(t => t.ChildTasks).FirstOrDefaultAsync(t => t.Id == taskId);

			if (task == null)
			{
				throw new DbUpdateException();
			}

			if (task.ChildTasks != null)
			{
				int tasksAmount = task.ChildTasks.Count;
				for(int i = 0; i < tasksAmount; i++)
				{
					await RecursionDelete(task.ChildTasks[0].Id);
				}
			}

			await _repository.DeleteById<TaskItemEntity>(taskId);
			await _repository.SaveChangesAsync();
		}

		private async Task RecursionDelete(int taskId)
		{
			var task = await _repository.GetAllElements<TaskItemEntity>().Include(t => t.ChildTasks).FirstOrDefaultAsync(t => t.Id == taskId);

			if(task == null)
			{
				return;
			}

			if (task.ChildTasks != null)
			{
				int tasksAmount = task.ChildTasks.Count;
				for (int i = 0; i < tasksAmount; i++)
				{
					await RecursionDelete(task.ChildTasks[0].Id);
				}
			}
			await _repository.DeleteById<TaskItemEntity>(taskId);
		}

		public async Task<TaskItemModel> GetTaskById(int taskId)
		{
			var entity = await _repository.GetAllElements<TaskItemEntity>().Include(t => t.ChildTasks).FirstOrDefaultAsync(t => t.Id == taskId);// _repository.GetElementById<TaskItemEntity>(taskId);

			if (entity == null)
			{
				throw new Exception("Can't find object by id");
			}

			var model = _mapper.Map<TaskItemModel>(entity);

			CountSubtasksLabor(model, model);

			return model;
		}

		private void CountSubtasksLabor(TaskItemModel subTask, TaskItemModel initialTask)
		{
			if(subTask == initialTask)
			{
				initialTask.TotalLaborFact = 0;
				initialTask.TotalLaborPlan = 0;
			}

			var subTasksLocal = GetSubTasks(subTask.Id);

			foreach(var task in subTasksLocal)
			{
				CountSubtasksLabor(task, initialTask);
			}

			initialTask.TotalLaborPlan += subTask.LaborPlan;
			initialTask.TotalLaborFact += subTask.LaborFact;
		}

		private IEnumerable<TaskItemModel> GetSubTasks(int taskId)
		{
			var subTasks = _repository.GetAllElements<TaskItemEntity>().Where(t => t.ParentTaskId == taskId).AsEnumerable();
			var mappedTasks = subTasks.Select(t => _mapper.Map<TaskItemModel>(t));

			return mappedTasks;
		}

		public IEnumerable<TaskStatusEnum> GetAvailableStatuses(TaskStatusEnum currentStatus)
		{
			List<TaskStatusEnum> statuses = new List<TaskStatusEnum>();

			if (currentStatus == TaskStatusEnum.Assigned)
			{
				statuses.Add(TaskStatusEnum.Assigned);
				statuses.Add(TaskStatusEnum.InProccess);
			}
			else if (currentStatus == TaskStatusEnum.InProccess)
			{
				statuses.Add(TaskStatusEnum.InProccess);
				statuses.Add(TaskStatusEnum.Stopped);
				statuses.Add(TaskStatusEnum.Finished);
			}
			else if (currentStatus == TaskStatusEnum.Stopped)
			{
				statuses.Add(TaskStatusEnum.InProccess);
				statuses.Add(TaskStatusEnum.Stopped);
			}
			else if (currentStatus == TaskStatusEnum.Finished)
			{
				statuses.Add(TaskStatusEnum.Finished);
			}

			return statuses;
		}

		public async Task UpdateTask(TaskItemModel taskModel)
		{
			var currentTaskEntity = await _repository.GetElementById<TaskItemEntity>(taskModel.Id);

			if(currentTaskEntity == null)
			{
				throw new Exception("Can't find object by id");
			}

			currentTaskEntity.TaskName = taskModel.TaskName;
			currentTaskEntity.Description = taskModel.Description;
			currentTaskEntity.Executors = taskModel.Executors;
			currentTaskEntity.LaborFact = taskModel.LaborFact;
			currentTaskEntity.LaborPlan = taskModel.LaborPlan;
						
			if(taskModel.Status == TaskStatusEnum.Finished)
			{
				if (await AreAllTasksReadyToFinish(taskModel.Id))
				{					
					await SetFinishStatusToSubTasks(taskModel.Id);
					currentTaskEntity.Status = (BusinessApplication.DAL.Contracts.Enums.TaskItemStatus)taskModel.Status;
					currentTaskEntity.FinishDate = DateTime.Now;
				}
			}
			else
			{
				currentTaskEntity.Status = (BusinessApplication.DAL.Contracts.Enums.TaskItemStatus)taskModel.Status;
			}

			await _repository.Update<TaskItemEntity>(currentTaskEntity);
			await _repository.SaveChangesAsync();
		}

		private async Task<bool> AreAllTasksReadyToFinish(int id)
		{
			var taskEntity = await _repository.GetAllElements<TaskItemEntity>().Include(t => t.ChildTasks).FirstOrDefaultAsync(t => t.Id == id);

			foreach (var subtask in taskEntity.ChildTasks)
			{
				if(!await AreAllTasksReadyToFinish(subtask.Id))
				{
					return false;
				}
			}			

			if(taskEntity.Status == BusinessApplication.DAL.Contracts.Enums.TaskItemStatus.InProccess || 
				taskEntity.Status == BusinessApplication.DAL.Contracts.Enums.TaskItemStatus.Finished)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private async Task SetFinishStatusToSubTasks(int id)
		{
			var taskEntity = await _repository.GetAllElements<TaskItemEntity>().Include(t => t.ChildTasks).FirstOrDefaultAsync(t => t.Id == id);

			foreach (var subtask in taskEntity.ChildTasks)
			{
				await SetFinishStatusToSubTasks(subtask.Id);
			}			
			if(taskEntity.Status != BusinessApplication.DAL.Contracts.Enums.TaskItemStatus.Finished)
			{
				taskEntity.Status = BusinessApplication.DAL.Contracts.Enums.TaskItemStatus.Finished;
				taskEntity.FinishDate = DateTime.Now;
			}			
		}
	}
}
