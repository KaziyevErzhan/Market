using Microsoft.EntityFrameworkCore;

namespace Market.Models
{
    public class BooksContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
