using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class ApplicantResumes
{
  [Key]
  public int Id { get; set; }
  public int TenantId { get; set; }
  public int ApplicantId { get; set; }
  public string ResumeFilePath { get; set; }
  public int? ResumeFileType { get; set; }
  public string? ResumeFilename { get; set; }
  public string? ResumeFileExtension { get; set; }
  public string? ResumeFileDescription { get; set; }
  public int RecordStatus { get; set; }
  public DateTime? ApplicationDate { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int? ModifiedBy { get; set; }
  public DateTime? ModifiedOn { get; set; }
}
