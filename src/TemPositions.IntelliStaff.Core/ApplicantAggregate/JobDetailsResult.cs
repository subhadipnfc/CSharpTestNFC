using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;

public class JobDetailsResult
{
  public int? OrderId { get; set; }
  public string? JobTitle { get; set; }
  public string? JobLocation { get; set; }
  public string? Division { get; set; }
  public string? ContactName { get; set; }

  public int? ApplicantId { get; set; }

  public int? CandidateId { get; set; }

  public string? EnteredBy { get; set; }
  public string? JobCategory { get; set; }
  public string? JobIndustry { get; set; }
  public string? Status { get; set; }

  public string? JObName { get; set; }
  public string? JObSubCategory { get; set; }
  public string? Address { get; set; }
  public string? Email { get; set; }

  public string? Phone { get; set; }
  public string? SkillSet { get; set; }
  public string? EducationLevel { get; set; }
  public string? JObLevel { get; set; }
  public string? Description { get; set; }
  public string? Responsibilities { get; set; }

}
