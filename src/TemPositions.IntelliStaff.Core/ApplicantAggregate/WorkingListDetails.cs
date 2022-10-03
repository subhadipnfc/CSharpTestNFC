using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class WorkingListDetails
{
  public int ApplicantId { get; set; }
  public int WorkingListId { get; set; }
  public string? ApplicantName { get; set; }
  public string? ApplicantEmail { get; set; }
  public string? RecruitingStage { get; set; }
  public string? CampaignStatus { get; set; }
  public DateTime AddedDate { get; set; }
  
}
