using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingApi.Dtos
{
    public class RegisterParticipantDto
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        [Required]
        [MaxLength(150)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public Guid MeetingId { get; set; }
    }
}
