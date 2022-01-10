using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal.Projects.Worker._1___Domain.Abstractions.Entities;

namespace Personal.Projects.Worker._2___Infra.Data.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMapping());
        }
    }

    public class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("PK_Id");

            builder.Property(e => e.Name)
                .HasColumnName("Name")
                .HasComment("Name of customer");

            builder.Property(e => e.LastName)
                .HasColumnName("LastName")
                .HasComment("Last name of customer");
        }
    }
}