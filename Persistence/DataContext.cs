using Microsoft.EntityFrameworkCore;

namespace Persistence
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options) { }

		#region Required
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Patient>()
				.HasKey(p => p.Id)
				.HasName("PatientId");
			
			modelBuilder.Entity<Patient>()
				.HasOne(n => n.Name)
				.WithOne()
				.HasForeignKey<Patient>(p => p.NameId);

			modelBuilder.Entity<Patient>()
				.Property(p => p.BirthDate)
				.IsRequired();

			modelBuilder.Entity<Name>()
				.HasKey(n => n.Id);

			modelBuilder.Entity<Name>()
				.Property(n => n.Family)
				.IsRequired();

			modelBuilder.Entity<Given>()
				.HasKey(g => g.Id);

			modelBuilder.Entity<Name>()
				.HasMany(e => e.Given)
				.WithMany(v => v.Names)
				.UsingEntity(j => j.ToTable("GivenName"));
		}
		#endregion

		public DbSet<Patient> Patients { get; set; }

		public DbSet<Name> Names { get; set; }
	}
}
