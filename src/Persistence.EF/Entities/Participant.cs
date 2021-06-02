using Persistence.EF.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Persistence.EF.Entities
{
    public class Participant : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Email { get; set; }
       
        [Required]
        public Guid MeetingId { get; set; }

        public virtual Meeting Meeting { get; set; }
    }
}
