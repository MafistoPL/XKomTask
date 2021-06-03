using Persistence.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EF.Repositories
{
    public class ParticipantRepository : BaseRepository<Participant>, IParticipantRepositiory
    {
        public ParticipantRepository(ApplicationContext dbContext) : base(dbContext)
        {
        }
    }
}
