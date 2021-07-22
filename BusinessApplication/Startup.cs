using BusinessApplication.DAL.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BusinessApplication.DAL.Contracts;
using BusinessApplication.Domain.Contracts;
using BusinessApplication.Domain.Implementations;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using BusinessApplication.Domain.Contracts.Interfaces;
using BusinessApplication.Web.Infrastructure.MIddleware.CultureProvider;
using BusinessApplication.Web.Infrastructure.Middleware.ExceptionHandling;

namespace BusinessApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<MyDbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
					assembly =>
						assembly.MigrationsAssembly("BusinessApplication.DAL.Implementations"));
			});

			#region localization
			services.AddLocalization(options => options.ResourcesPath = "Resources");
			services.AddControllersWithViews()
				.AddDataAnnotationsLocalization()
				.AddViewLocalization();
			services.Configure<RequestLocalizationOptions>(CultureOptionsSetter.SetCultureOptions);
			#endregion

			#region DI

			services.AddScoped<IDbRepository, DbRepository>();
			services.AddScoped<ITaskManager, TaskManager>();
			services.AddTransient<ITreeViewService, TreeViewService>();
			#endregion

			services.AddAutoMapper(typeof(Startup));
		}



		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseMiddleware<ExceptionHandlingMiddleware>();
			}

			app.UseMiddleware<CultureProviderMiddleware>();

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=taskorganizer}/{action=Index}/{id?}");
			});
		}
	}
}
