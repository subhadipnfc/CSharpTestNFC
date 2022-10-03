using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class RecruitingSubStageType
{
  [Key]
  public int Id { get; set; }
  public int RecruitingStageTypeId { get; set; }
  public string? SubStageName { get; set; }
  public int Status { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
  public int IsRecruitingStageCompleted { get; set; }
}
