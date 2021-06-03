using AutoMapper;
using MeetingApi.Dtos;
using MeetingApi.Dtos.Participant;
using Microsoft.AspNetCore.Mvc;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System.Threading.Tasks;

namespace MeetingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IAsyncRepository<Participant> _participantRepository;
        private readonly IMapper _mapper;

        public ParticipantController(IAsyncRepository<Participant> participantRepository, IMapper mapper)
        {
            _participantRepository = participantRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Register participant to meeting
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> RegisterParticipant(RegisterParticipantDto participantDto)
        {
            var registeredParticipant = await _participantRepository
                .AddAsync(_mapper.Map<Participant>(participantDto));

            return Ok(_mapper.Map<ParticipantDto>(registeredParticipant));
        }
    }
}
