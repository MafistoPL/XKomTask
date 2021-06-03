using AutoMapper;
using MeetingApi.Dtos;
using MeetingApi.Dtos.Meeting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistence.EF.Entities;
using Persistence.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IAsyncRepository<Meeting> _meetingsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MeetingsController> _logger;

        public MeetingsController(IAsyncRepository<Meeting> meetingsRepository, IMapper mapper,
            ILogger<MeetingsController> logger)
        {
            _meetingsRepository = meetingsRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Add new meeting
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> AddMeeting(CreateMeetingDto createMeetingDto)
        {
            var savedMeeting = await _meetingsRepository
                .AddAsync(_mapper.Map<Meeting>(createMeetingDto));

            return Ok(_mapper.Map<MeetingDto>(savedMeeting));
        }

        /// <summary>
        /// Delete existing meeting
        /// </summary>
        [HttpDelete("{meetingId}")]
        public async Task<ActionResult> RemoveMeeting(Guid meetingId)
        {
            _logger.LogWarning($"Meeting with id: {meetingId} DELETE action invoked");

            var deletedMeeting = new Meeting { Id = meetingId };
            await _meetingsRepository.DeleteAsync(deletedMeeting);

            return NoContent();
        }

        /// <summary>
        /// Fetch all meetings
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetAllMeetings()
        {
            var allMeetings = await _meetingsRepository.GetAllAsync();

            return Ok(_mapper.Map<List<MeetingDto>>(allMeetings));
        }
    }
}