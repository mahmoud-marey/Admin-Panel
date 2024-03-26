using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Sherka.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AdminHome> AdminHome { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<About> About { get; set; }
    }
}