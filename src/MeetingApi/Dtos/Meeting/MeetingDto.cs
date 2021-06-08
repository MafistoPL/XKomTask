using MeetingApi.Dtos.Participant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApi.Dtos.Meeting
{
    public class MeetingDto
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public string Place { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ParticipantDto> Participants { get; set; }
    }
}
