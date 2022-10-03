using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ApplicantAdvanceSearchRequest
{
public string? ApplicantFirstName { get; set; }
public string? ApplicantLastName { get; set; }
public string? ApplicantEmail { get; set; }
public string? ApplicantStatus { get; set; }
public int? DivisionId { get; set; }
public int? JobBoardId { get; set; }
public int? JobPostId { get; set; }
public DateTime? JobStartDate { get; set; }
public DateTime? JobEndDate { get; set; }
public int skip { get; set; }
public int LimitRows { get; set; }
public int NameSort { get; set; }
public int DateSort { get; set; }
public int WorkingList { get; set; }
public int CampaignStatus { get; set; }
public string JobPost { get; set; }
public string Recruiter { get; set; }
public int ApplicantType { get; set; }

public bool isURL { get; set; } = true;
 public bool isToggle { get; set; }

  public int UserId { get; set; }

}
