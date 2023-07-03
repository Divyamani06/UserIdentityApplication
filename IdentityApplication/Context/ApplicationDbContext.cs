using IdentityApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityApplication.Context
{
    public class ApplicationDbContext:IdentityDbContext
    {
    

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
           

        }

        public DbSet<IdentityModel> IdentityModel { get; set; }
    }
}
