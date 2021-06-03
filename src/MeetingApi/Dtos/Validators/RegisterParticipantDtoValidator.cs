using FluentValidation;
using Persistence.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace MeetingApi.Dtos.Validators
{
    public class RegisterParticipantDtoValidator : AbstractValidator<RegisterParticipantDto>
    {
        private readonly IAsyncRepository<Persistence.EF.Entities.Meeting> _meetingsRepository;

        public RegisterParticipantDtoValidator(IAsyncRepository<Persistence.EF.Entities.Meeting> meetingsRepository)
        {
            _meetingsRepository = meetingsRepository;

            RuleFor(m => m.MeetingId).MustAsync((x, cancellation) => CheckIfMeetingExists(x))
                .WithMessage("Meeting does not exist");
        }

        private async Task<bool> CheckIfMeetingExists(Guid meetingId)
        {
            var meeting = await _meetingsRepository.GetByIdAsync(meetingId);

            if (meeting == null)
            {
                return false;
            }

            return true;
        }
    }
}
