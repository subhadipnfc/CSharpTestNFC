using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class SubStagesArray
{
  public int RecruitingSubStageId { get; set; }
  public int? RecruitingStageId { get; set; }
  public string? RecrutingStage { get; set; }
  public bool IsRecruitingStageCompleted { get; set; } 
  public DateTime? CreatedOn { get; set; }
  public int CreatedBy { get; set; }
  public string? Comments { get; set; }
  public string? UserName { get; set; }
}
