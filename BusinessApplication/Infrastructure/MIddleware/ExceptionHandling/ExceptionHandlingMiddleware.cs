using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessApplication.Web.Infrastructure.Middleware.ExceptionHandling
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionHandlingMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, IWebHostEnvironment webHostEnvironment)
		{
			try
			{
				await _next(context);
			}
			catch(Exception ex)
			{
				Log(context, ex, webHostEnvironment);
				throw;
			}
		}

		private static void Log(HttpContext context, Exception exception, IWebHostEnvironment webHostEnvironment)
		{
			var savePath = webHostEnvironment.WebRootPath;
			var now = DateTime.UtcNow;
			var fileName = $"{now:yyyy_MM_dd}.log";
			var filePath = Path.Combine(savePath, "logs", fileName);
						
			new FileInfo(filePath).Directory.Create();

			using var writer = File.CreateText(filePath);
			writer.WriteLine($"{now:HH:mm:ss} {context.Request.Path}");
			writer.WriteLine(exception.Message);
		}
	}
}
