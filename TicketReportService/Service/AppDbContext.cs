using Microsoft.EntityFrameworkCore;
using TicketReportService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketReportService.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Booking { get; set; }

        public DbSet<Passenger> Passenger { get; set; }

        public DbSet<Airline> Airline { get; set; }

        public DbSet<Schedule> Schedule { get; set; }
    }
}
