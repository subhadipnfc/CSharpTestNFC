using System;
using System.Collections.Generic;

namespace TemPositions.IntelliStaff.Infrastructure
{
    public partial class SkillSet
    {
        public SkillSet()
        {
            JobPostSkills = new HashSet<JobPostSkill>();
        }

        public int Id { get; set; }
        public int TenantId { get; set; }
        public string SkillName { get; set; } = null!;
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<JobPostSkill> JobPostSkills { get; set; }
    }
}
