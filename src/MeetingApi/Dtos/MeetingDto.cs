using System;
using System.ComponentModel.DataAnnotations;

namespace MeetingApi.Dtos
{
    public class MeetingDto
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Place { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}