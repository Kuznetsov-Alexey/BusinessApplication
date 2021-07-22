using AutoMapper;
using BusinessApplication.DAL.Contracts.Enteties;
using BusinessApplication.Domain.Contracts.Models;
using BusinessApplication.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.MapperProfiles
{
	public class LeadProfile : Profile
	{
		public LeadProfile()
		{
			CreateMap<TaskItemEntity, TaskItemModel>();
			CreateMap<TaskItemModel, TaskItemEntity>();

			CreateMap<TaskItemModel, CreateTaskViewModel>();
			CreateMap<CreateTaskViewModel, TaskItemModel>();

			CreateMap<TaskItemModel, EditTaskViewModel>();
			CreateMap<TaskItemModel, ShowTaskViewModel>();
			CreateMap<TaskItemModel, ConfirmDeleteViewModel>();

			CreateMap<EditTaskViewModel, TaskItemModel>();
		}
	}
}
