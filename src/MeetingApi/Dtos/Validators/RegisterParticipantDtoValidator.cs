using FluentValidation;
using MeetingApi.Config;
using Persistence.EF.Repositories;
using System;
using System.Threading.Tasks;

namespace MeetingApi.Dtos.Validators
{
    public class RegisterParticipantDtoValidator : AbstractValidator<RegisterParticipantDto>
    {
        private readonly IMeetingRepository _meetingsRepository;
        private readonly MeetingValidationConfig _meetingValidationConfig;

        public RegisterParticipantDtoValidator(IMeetingRepository meetingsRepository,
            MeetingValidationConfig meetingValidationConfig)
        {
            _meetingsRepository = meetingsRepository;
            _meetingValidationConfig = meetingValidationConfig;

            RuleFor(p => p.MeetingId).MustAsync((x, cancellation) => CheckIfMeetingExistsOrIsFull(x))
                .WithMessage("Meeting does not exist or no place left");
        }

        private async Task<bool> CheckIfMeetingExistsOrIsFull(Guid meetingId)
        {
            var meeting = await _meetingsRepository.GetByIdWithParticipantsAsync(meetingId);

            if (meeting == null)
            {
                return false;
            }
            if (meeting.Participants.Count >= _meetingValidationConfig.MaxParticipantCount)
            {
                return false;
            }

            return true;
        }
    }
}
