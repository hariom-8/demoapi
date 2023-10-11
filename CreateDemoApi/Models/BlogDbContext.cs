using Microsoft.EntityFrameworkCore;

namespace CreateDemoApi.Models
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        
    }
}
