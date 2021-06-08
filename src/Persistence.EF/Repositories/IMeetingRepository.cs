using Persistence.EF.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.EF.Repositories
{
    public interface IMeetingRepository : IAsyncRepository<Meeting>
    {
        Task<Meeting> GetByIdWithParticipantsAsync(Guid id);
        Task<IReadOnlyList<Meeting>> GetAllWithParticipantsAsync();
    }
}