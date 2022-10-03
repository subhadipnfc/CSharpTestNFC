using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public  class WorkingListSearchInput
{
  public string? ApplicantFirstName { get; set; }
  public string? ApplicantLastName { get; set; }
  public string? ApplicantEmail { get; set; }
  public int RecruitingStage { get; set; }
  public int? WorkingListId { get; set; }
  public int skip { get; set; }
  public int LimitRows { get; set; }

}
