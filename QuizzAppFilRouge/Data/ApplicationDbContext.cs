using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using QuizzAppFilRouge.Data.Entities;
using System.Reflection.Emit;

namespace QuizzAppFilRouge.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Quizz> Quizzes { get; set; }
        public DbSet<Passage> Passages { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<Response> Responses { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }








        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
        }
    }

    public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u=>u.FirstName).HasMaxLength(75);
            builder.Property(u=>u.LastName).HasMaxLength(75);
            builder.Property(u => u.BirthDate);
            builder.Property(u => u.HandleBy).HasMaxLength(450);


        }
    }
}