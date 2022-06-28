using Microsoft.EntityFrameworkCore;
using SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduleApiService.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Schedule> Schedule { get; set; }

        public DbSet<Airline> Airline { get; set; }

        public DbSet<Meal> Meal { get; set; }

        public DbSet<ScheduledDays> ScheduledDaysType { get; set; }

        public DbSet<Location> Location { get; set; }

        public DbSet<Discount> Discount { get; set; }
    }
}
