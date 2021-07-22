using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.Infrastructure.MIddleware.CultureProvider
{
	public static class CultureOptionsSetter
	{
		public static void SetCultureOptions(RequestLocalizationOptions options)
		{
			var supportedCultures = new[]
			{
				new CultureInfo("en"),
				new CultureInfo("ru")

			};
			options.DefaultRequestCulture = new RequestCulture("ru");
			options.SupportedCultures = supportedCultures;
			options.SupportedUICultures = supportedCultures;
		}
	}
}
