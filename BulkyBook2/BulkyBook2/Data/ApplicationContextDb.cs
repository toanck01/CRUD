
using BulkyBook2.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook2.Data
{
    public class ApplicationContextDb:DbContext
    {
        public ApplicationContextDb(DbContextOptions<ApplicationContextDb> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
    }
}
