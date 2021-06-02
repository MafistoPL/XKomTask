using Microsoft.EntityFrameworkCore;
using Persistence.EF.Entities;

namespace Persistence.EF
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        { }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
