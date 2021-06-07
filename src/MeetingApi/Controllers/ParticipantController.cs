using MeetingApi.Dtos;
using MeetingApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeetingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        /// <summary>
        /// Register participant to meeting
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> RegisterParticipant(RegisterParticipantDto participantDto)
        {   
            return Ok(await _participantService.RegisterParticipant(participantDto));
        }
    }
}
