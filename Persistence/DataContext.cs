using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence
{
	public class DataContext : DbContext
	{
		public DataContext(DbContextOptions options) : base(options) { }

		#region Required
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			new PatientEntityTypeConfiguration().Configure(modelBuilder.Entity<Patient>());

			modelBuilder.Entity<Name>()
				.HasKey(n => n.Id);

			modelBuilder.Entity<Name>()
				.Property(n => n.Family)
				.IsRequired();

			modelBuilder.Entity<Given>()
				.HasKey(g => g.Id);

			modelBuilder.Entity<Given>()
				.HasAlternateKey(g => g.Text);

			new GivenNameEntityTypeConfiguration().Configure(modelBuilder.Entity<GivenName>());
		}
		#endregion

		public DbSet<Patient> Patients { get; set; }

		public DbSet<Name> Names { get; set; }

		public DbSet<Given> Given { get; set; }

		public DbSet<GivenName> GivenName { get; set; }
	}

	public class GivenName
	{
		public Guid NameId { get; set; }
		public Name Name { get; set; }

		public int GivenId { get; set; }
		public Given Given { get; set; }
	}

	public class GivenNameEntityTypeConfiguration : IEntityTypeConfiguration<GivenName>
	{
		public void Configure(EntityTypeBuilder<GivenName> builder)
		{
			builder.HasKey(gn => new { gn.NameId, gn.GivenId });

			builder.HasOne(gn => gn.Name)
				.WithMany(n => n.GivenNames)
				.HasForeignKey(gn => gn.NameId);

			builder.HasOne(gn => gn.Given)
				.WithMany(g => g.NameAssociations)
				.HasForeignKey(gn => gn.GivenId);
		}
	}

	public class PatientEntityTypeConfiguration : IEntityTypeConfiguration<Patient>
	{
		public void Configure(EntityTypeBuilder<Patient> builder)
		{
			builder.HasKey(p => p.Id)
			.HasName("PatientId");

			builder.HasOne(n => n.Name)
				.WithOne()
				.HasForeignKey<Patient>(p => p.NameId);

			builder.Property(p => p.BirthDate)
				.IsRequired();
		}
	}
}
