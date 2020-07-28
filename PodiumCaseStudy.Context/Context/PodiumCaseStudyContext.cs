using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PodiumCaseStudy.Context
{
    public class PodiumCaseStudyContext : DbContext
    {
        public PodiumCaseStudyContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<MortgageProduct> MortgageProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("PodiumCaseStudy");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<User>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User>()
                .Property(x => x.Firstname)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(320);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(x => x.DateOfBirth)
                .IsRequired();

            #endregion

            #region Mortgage

            modelBuilder.Entity<MortgageProduct>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<MortgageProduct>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MortgageProduct>()
                .Property(x => x.Lender)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<MortgageProduct>()
                .Property(x => x.InterestRate)
                .IsRequired();

            modelBuilder.Entity<MortgageProduct>()
                .Property(x => x.MortgageType)
                .IsRequired();

            modelBuilder.Entity<MortgageProduct>()
                .Property(x => x.LoanToValue)
                .IsRequired();

            #endregion
        }

        public void SeedContext()
        {
            if (!MortgageProducts.Any())
            {
                MortgageProducts.AddRange(
                    new MortgageProduct
                    {
                        Lender = "Bank A",
                        InterestRate = 2,
                        MortgageType = MortgageType.Variable,
                        LoanToValue = 60
                    },
                    new MortgageProduct
                    {
                        Lender = "Bank B",
                        InterestRate = 3,
                        MortgageType = MortgageType.Fixed,
                        LoanToValue = 60
                    },
                    new MortgageProduct
                    {
                        Lender = "Bank C",
                        InterestRate = 4,
                        MortgageType = MortgageType.Variable,
                        LoanToValue = 90
                    });

                SaveChanges();
            }
        }
    }
}
