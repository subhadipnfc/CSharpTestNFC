using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class StagesArray
{
  public string? ParentStageName { get; set; }
  public int? RecruitingStageId { get; set; }

  public bool IsRecruitingStageCompleted { get; set; }= false;

}
