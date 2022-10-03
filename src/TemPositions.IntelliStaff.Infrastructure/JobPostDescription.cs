using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TemPositions.IntelliStaff.Infrastructure
{
    public partial class JobPostDescription
    {
        [Key]
        public int Id { get; set; }
        public int JobPostId { get; set; }
        public string? Responsibility { get; set; }
        public string Description { get; set; } = null!;
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
       
    }
}
