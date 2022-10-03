using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ApplicantInput
{
  public int TenantId { get; set; }
  public string? FirstName { get; set; }
  public string? MiddleName { get; set; }
  public string? LastName { get; set; }
  public string? Email { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? ZipCode { get; set; }
  public string? CellPhoneNumber { get; set; }
  public string? HomePhoneNumber { get; set; }
  public int SubSourceID { get; set; }
  public int DivisionId { get; set; }
  public DateTime SubmitDateTime { get; set; }
  public bool HasResumeFile { get; set; }
  public string ReferenceId { get; set; }
  public int CandidateId { get; set; }
  public int RecordStatus { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int? ModifiedBy { get; set; }
  public DateTime? ModifiedOn { get; set; }
  public int? JobPostId { get; set; }
  public string? JobPostReferenceId { get; set; }
  public int? ApplicantType { get; set; }
  public int SourceId { get; set;}
  public string Activity { get; set; }
  public int CategoryType { get; set; }
  public int CategoryId { get; set; }
  public string? Category { get; set; }
  public byte IsAutomated { get; set; }
  public string ResumeFilePath { get; set; }
  public int ResumeFileType { get; set; }
  public string ResumeFilename { get; set; }
  public string ResumeFileExtension { get; set; }
  public string ResumeFileDescription { get; set; }
  public string ResumeText { get; set; }
  public string? ResumeFile { get; set; }
}
