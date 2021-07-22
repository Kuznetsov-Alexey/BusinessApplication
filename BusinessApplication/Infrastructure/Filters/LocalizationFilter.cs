using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.Infrastructure.Filters
{
	public class LocalizationFilter : Attribute, IAsyncResourceFilter
	{
		public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
		{
			if(CultureInfo.DefaultThreadCurrentCulture != null && CultureInfo.DefaultThreadCurrentUICulture != null)
			{
				CultureInfo.CurrentCulture = CultureInfo.DefaultThreadCurrentCulture;
				CultureInfo.CurrentUICulture = CultureInfo.DefaultThreadCurrentUICulture;
			}

			await next();
		}
	}
}
