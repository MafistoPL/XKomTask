using Persistence.EF.Entities;

namespace Persistence.EF.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepository
    {
        public ParticipantRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
