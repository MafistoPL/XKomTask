using AutoMapper;
using MeetingApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System.Threading.Tasks;

namespace MeetingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IAsyncRepository<Meeting> _meetingsRepository;
        private readonly IMapper _mapper;

        public MeetingsController(IAsyncRepository<Meeting> meetingsRepository, IMapper mapper)
        {
            _meetingsRepository = meetingsRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddMeeting(MeetingDto meetingDto)
        {
            var savedMeeting = await _meetingsRepository
                .AddAsync(_mapper.Map<Meeting>(meetingDto));
            return Ok(savedMeeting);
        }
    }
}