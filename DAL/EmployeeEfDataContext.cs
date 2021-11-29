using System.Data.Entity;

namespace DAL
{
	// ORM - object relation mapper
	public class EmployeeEfDataContext : DbContext
	{
		private static string ConnectionString = "Server=LAPTOP-3MU9SNU2;Database=PERSONALTRACKING;Trusted_Connection=True;";

		public DbSet<DEPARTMENT> DEPARTMENTs { get; set; }
		public DbSet<EMPOLYEE> EMPOLYEEs { get; set; }
		public DbSet<POSITION> POSITIONs { get; set; }
		public DbSet<PERMISSION> PERMISSIONs { get; set; }
		public DbSet<MONTH> MONTHs { get; set; }
		public DbSet<PERMISSIONSTATE> PERMISSIONSTATEs { get; set; }
		public DbSet<SALARY> SALARies { get; set; }
		public DbSet<TASK> TASKs { get; set; }
		public DbSet<TASKSTATE> TASKSTATEs { get; set; }


        public EmployeeEfDataContext() : base(ConnectionString)
		{
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<DEPARTMENT>().ToTable("DEPARTMENT");

			modelBuilder.Entity<EMPOLYEE>().ToTable("EMPOLYEE");

			modelBuilder.Entity<POSITION>().ToTable("POSITION");

			modelBuilder.Entity<PERMISSION>().ToTable("PERMISSION");

			modelBuilder.Entity<MONTH>().ToTable("MONTH");

			modelBuilder.Entity<PERMISSIONSTATE>().ToTable("PERMISSIONSTATE");

			modelBuilder.Entity<SALARY>().ToTable("SALARY");

			modelBuilder.Entity<TASK>().ToTable("TASK");

			modelBuilder.Entity<TASKSTATE>().ToTable("TASKSTATE");

		}
	}
}