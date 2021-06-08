using Microsoft.EntityFrameworkCore;
using Persistence.EF.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.EF.Repositories
{
    public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
    {
        public MeetingRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<Meeting> GetByIdWithParticipantsAsync(Guid id)
        {
            return await _dbContext.Meetings
                .Include(m => m.Participants)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IReadOnlyList<Meeting>> GetAllWithParticipantsAsync()
        {
            return await _dbContext.Meetings.Include(m => m.Participants).ToListAsync();
        }
    }
}