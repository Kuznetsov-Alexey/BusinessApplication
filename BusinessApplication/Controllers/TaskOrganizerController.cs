using AutoMapper;
using BusinessApplication.DAL.Contracts.Enteties;
using BusinessApplication.Domain.Contracts;
using BusinessApplication.Domain.Contracts.Interfaces;
using BusinessApplication.Domain.Contracts.Models;	
using BusinessApplication.Web.Infrastructure.Filters;
using BusinessApplication.Web.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.Controllers
{
	[LocalizationFilter]
	public class TaskOrganizerController : Controller
	{		
		private readonly IMapper _mapper;
		private readonly ITaskManager _taskManager;
		private readonly ITreeViewService _treeViewService;

		public TaskOrganizerController(ITaskManager taskManager, ITreeViewService treeViewService, IMapper mapper)
		{
			
			_mapper = mapper;
			_taskManager = taskManager;
			_treeViewService = treeViewService;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var treeList = _treeViewService.GetTreeList();

			TreeListViewModel model = new TreeListViewModel
			{
				Tasks = treeList
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult Add(int? id)
		{
			var viewModel = new CreateTaskViewModel();

			if(id != null)
			{
				viewModel.ParentTaskId = id;
			}

			return PartialView("_AddViewPartial", viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Add(CreateTaskViewModel model)
		{
			if(ModelState.IsValid)
			{
				var mappedModel = _mapper.Map<TaskItemModel>(model);
				mappedModel.CreationDate = DateTime.Now;
				await _taskManager.CreateTask(mappedModel);

				return RedirectToAction(nameof(Index));// TaskInfo(mappedModel.Id).Result; 
			}
			else
			{
				return new ContentResult {Content = "Problems with creation" };
			}
		}

		[HttpGet]
		public async Task<IActionResult> TaskInfo(int id)
		{
			var taskModel = await _taskManager.GetTaskById(id);			

			if(taskModel == null)
			{
				return RedirectToAction("Index");
			}

			var viewModel = _mapper.Map<ShowTaskViewModel>(taskModel);

			return PartialView("_TaskInfoViewPartial", viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> EditTask(int id)
		{
			var taskItem = await _taskManager.GetTaskById(id);
			var availableStatuses = _taskManager.GetAvailableStatuses(taskItem.Status);

			var model = _mapper.Map<EditTaskViewModel>(taskItem);

			model.AvailableStatuses = availableStatuses.Select(s => new SelectListItem { Text = s.ToString() });

			return PartialView("_EditViewPartial", model);
		}

		[HttpPost]
		public async Task<IActionResult> EditTask(EditTaskViewModel model)
		{			
			if(ModelState.IsValid)
			{
				var mappedModel = _mapper.Map<TaskItemModel>(model);
				await _taskManager.UpdateTask(mappedModel);

				ViewData["Message"] = "Note was updated";
				return RedirectToAction(nameof(Index));			
			}
			else
			{
				ViewData["Message"] = "Error with note updating";
				return RedirectToAction(nameof(Index));
			}
		}

		[HttpGet]		
		public async Task<IActionResult> ConfirmDelete(int id)
		{
			var model = await _taskManager.GetTaskById(id);
			var viewModel = _mapper.Map<ConfirmDeleteViewModel>(model);

			return PartialView("_ConfirmDeleteViewPartial", viewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			await _taskManager.DeleteTaskItem(id);
			return RedirectToAction(nameof(Index));
		}
	}
}
