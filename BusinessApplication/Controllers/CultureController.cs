using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.Controllers
{
	public class CultureController : Controller
	{
		public IActionResult SetCulture(string culture)
		{
			var chosenCulture = new CultureInfo(culture);

			CultureInfo.DefaultThreadCurrentCulture = chosenCulture;
			CultureInfo.DefaultThreadCurrentUICulture = chosenCulture;

			return LocalRedirect("~/TaskOrganizer/Index");
		}
	}
}
