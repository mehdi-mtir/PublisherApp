using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublisherDomain;
using System.Diagnostics;

namespace PublisherData
{
    public class PubContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Cover> Covers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB;Initial Catalog=PublisherDB;")
                .LogTo(message => Debug.WriteLine(message), new[] { DbLoggerCategory.Database.Command.Name}, LogLevel.Information )
                .EnableSensitiveDataLogging();
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, FirstName = "Jeff", LastName = "Olsen" });

            //modelBuilder.Entity<Author>().HasMany<Book>().WithOne().HasForeignKey(b => b.AuthorFK);
            //modelBuilder.Entity<Author>().HasMany<Book>().WithOne();
        }
         /*   //modelBuilder.Entity<Book>()
                   // .Property(b => b.Title)
                   // .HasColumnName("MainTitle");


            var authors = new Author[]
            {
                new Author { AuthorId = 2, FirstName = "Charles", LastName="Duhigg"},
                new Author { AuthorId = 3, FirstName = "Victor", LastName="Hugo"},
                new Author { AuthorId = 4, FirstName = "Emile", LastName="Zola"},
                new Author { AuthorId = 5, FirstName = "Pablo", LastName="Coelho"},
            };
            modelBuilder.Entity<Author>().HasData(authors);

            var books = new Book[]
            {
                new Book{ BookId = 1, AuthorId = 1, Title = "The slight Edge", PublishDate = new DateOnly(1990, 1, 1), BasePrice = 17.50m },
                new Book{ BookId = 2, AuthorId = 5, Title = "L'alchimiste", PublishDate = new DateOnly(1988, 1, 1), BasePrice = 22.50m },
                new Book{ BookId = 3, AuthorId = 5, Title = "Onze minutes", PublishDate = new DateOnly(2003, 1, 1), BasePrice = 19.50m }
            };
            modelBuilder.Entity<Book>().HasData(books);
        }*/

    }
    
}
