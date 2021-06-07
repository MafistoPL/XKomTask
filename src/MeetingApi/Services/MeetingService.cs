using MeetingApi.Exceptions;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace MeetingApi.Services
{
    public interface IMeetingService
    {
        Task RemoveMeetingAsync(Guid meetingId);
    }

    public class MeetingService : IMeetingService
    {
        private readonly IAsyncRepository<Meeting> _meetingsRepository;

        public MeetingService(IAsyncRepository<Meeting> meetingsRepository)
        {
            _meetingsRepository = meetingsRepository;
        }

        public async Task RemoveMeetingAsync(Guid meetingId)
        {
            var meetingToDelete = await _meetingsRepository.GetByIdAsync(meetingId);
            if (meetingToDelete is null)
            {
                throw new NotFoundException($"Meeting with given id='{meetingId}' does not exist");
            }
            await _meetingsRepository.DeleteAsync(meetingToDelete);
        }

    }
}
