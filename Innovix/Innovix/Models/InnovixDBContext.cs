using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Models
{
    public class InnovixDBContext : DbContext
    {
        public InnovixDBContext(DbContextOptions<InnovixDBContext> options) : base(options)
        {

        }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<SeasonType> SeasonTypes { get; set; }
        public DbSet<MealPlan> MealPlans { get; set; }
    }
}
