using Persistence.EF.Entities;

namespace Persistence.EF.Repositories
{
    public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}