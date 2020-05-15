using System;
using ZooDataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace ZooDataBase
{
	public class ZooDatabase : DbContext
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured == false)
			{
				optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-8MB9FIAG\SQLEXPRESS;Initial Catalog=ZooCalculationDatabaseImplements;Integrated Security=True;MultipleActiveResultSets=True;");
			}
			base.OnConfiguring(optionsBuilder);
		}
		public virtual DbSet<Route> Routes { set; get; }
		public virtual DbSet<Excursion> Excursions { set; get; }
		public virtual DbSet<RouteForExcursion> RouteForExcursions { set; get; }
		public virtual DbSet<Client> Clients { set; get; }
		public virtual DbSet<Order> Orders { set; get; }
	}
}
