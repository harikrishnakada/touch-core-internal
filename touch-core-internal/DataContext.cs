using Microsoft.EntityFrameworkCore;
using touch_core_internal.Models;

namespace touch_core_internal
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Badge> Badges { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Reward> Rewards { get; set; }

        public DbSet<TimeSheet> TimeSheets { get; set; }
    }
}