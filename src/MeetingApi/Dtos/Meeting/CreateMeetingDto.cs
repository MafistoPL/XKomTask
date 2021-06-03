using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingApi.Dtos
{
    public class CreateMeetingDto
    {
        [Required]
        [MaxLength(150)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(100)]
        public string Place { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}