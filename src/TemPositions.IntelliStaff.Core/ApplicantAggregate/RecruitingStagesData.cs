using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class RecruitingStagesData
{
  public int RecruitingStageTypeId { get; set; }
  public string? RecruitingStage { get; set; }
  public List<RecruitingSubStageType>? RecruitingSubStageType { get; set; }


}
