using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ResumeDetails
{
  public int ResumeID { get; set; }
  public string? ResumeFile { get; set; }
  public string? ResumeFileExtension { get; set; }
  public int? IsProcess { get; set; }
}
