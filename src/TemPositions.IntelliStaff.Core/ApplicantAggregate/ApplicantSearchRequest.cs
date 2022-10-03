using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ApplicantSearchRequest
{
  public string? ApplicantFirstName { get; set; }
  public string? ApplicantLastName { get; set; }
  public string? ApplicantEmail { get; set; }
  public string? ApplicantStatus { get; set; }
  public string? DivisionId { get; set; }
  public int? JobPostId { get; set; }

  public DateTime? JobStartDate { get; set; }
  public DateTime? JobEndDate { get; set; }
  public int skip { get; set; }
  public int LimitRows { get; set; }

  public int NameSort { get; set; }
  public int DateSort { get; set; }
}
