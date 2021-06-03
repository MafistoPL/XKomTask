using AutoMapper;
using MeetingApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System;
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

        /// <summary>
        /// Add new meeting
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddMeeting(MeetingDto meetingDto)
        {
            var savedMeeting = await _meetingsRepository
                .AddAsync(_mapper.Map<Meeting>(meetingDto));
            return Ok(savedMeeting);
        }

        /// <summary>
        /// Delete existing meeting
        /// </summary>
        [HttpDelete("{meetingId}")]
        public async Task<ActionResult> RemoveMeeting(Guid meetingId)
        {
            var deletedMeeting = new Meeting { Id = meetingId };
            await _meetingsRepository.DeleteAsync(deletedMeeting);
            return NoContent();
        }
    }
}