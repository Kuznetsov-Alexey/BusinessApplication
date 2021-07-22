using BusinessApplication.DAL.Contracts.Enteties;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApplication.DAL.Implementations
{
	public class MyDbContext : DbContext
	{
		public DbSet<TaskItemEntity> Tasks { get; set; }

		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<TaskItemEntity>().HasOne(e1 => e1.ParentTask).WithMany(e2 => e2.ChildTasks);
		}
	}
}
