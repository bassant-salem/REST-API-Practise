using APIproject.Models;
using Microsoft.EntityFrameworkCore;

namespace APIproject.Data
{
    public class APIprojectContext : DbContext
    {
        public APIprojectContext(DbContextOptions<APIprojectContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "The Metamorphosis",
                    Author = "Franz Kafka",
                    YearPublished = 1960
                },
            new Book
            {
                Id = 2,
                Title = "1984",
                Author = "George Orwell",
                YearPublished = 1949
            },
            new Book
            {
                Id = 3,
                Title = "The Brothers Karamazov",
                Author = "Fyodor Dostoevsky",
                YearPublished = 1880
            },
            new Book
            {
                Id = 4,
                Title = "Being and Nothingness",
                Author = "Jean-Paul Sartre",
                YearPublished = 1943
            },
            new Book
            {
                Id = 5,
                Title = "The Trial",
                Author = "Franz Kafka",
                YearPublished = 1925
            }
                );
        } 
        public DbSet<Book> Books { get; set; } = default!;
    }
}
