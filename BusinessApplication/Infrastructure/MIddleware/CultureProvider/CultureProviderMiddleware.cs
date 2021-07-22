using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.Infrastructure.MIddleware.CultureProvider
{
	public class CultureProviderMiddleware
	{
        private readonly RequestDelegate _next;

        public CultureProviderMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var lang = context.Request.Query["culture"].ToString();
            if (!string.IsNullOrEmpty(lang))
            {
                try
                {
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(lang);
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(lang);
                }
                catch (CultureNotFoundException) { }
            }

            await _next.Invoke(context);
        }
    }
}
