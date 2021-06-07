using AutoMapper;
using MeetingApi.Dtos;
using MeetingApi.Dtos.Participant;
using MeetingApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System.Threading.Tasks;

namespace MeetingApi.Services
{
    public interface IParticipantService
    {
        Task<ParticipantDto> RegisterParticipant(RegisterParticipantDto participantDto);
    }

    public class ParticipantService : IParticipantService
    {
        private readonly IAsyncRepository<Participant> _participantRepository;
        private readonly IMapper _mapper;

        public ParticipantService(IAsyncRepository<Participant> participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        public async Task<ParticipantDto> RegisterParticipant(RegisterParticipantDto participantDto)
        {
            try
            {
                var registeredParticipant = await _participantRepository
                .AddAsync(_mapper.Map<Participant>(participantDto));

                return _mapper.Map<ParticipantDto>(registeredParticipant);
            }
            catch(DbUpdateException)
            {
                throw new BadRequestException("Provided email is already assigned to given meeting");
            }
        }
    }
}
