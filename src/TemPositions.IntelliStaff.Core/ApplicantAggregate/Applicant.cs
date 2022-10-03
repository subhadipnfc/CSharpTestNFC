using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class Applicant
{
  [Key]
  public int Id { get; set; }
  public int? TenantId { get; set; }
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
  public int? SubSourceID { get; set; }
  public int? DivisionId { get; set; }
  public DateTime? SubmitDateTime { get; set; }
  public bool? HasResumeFile { get; set; } = false;
  public string? ReferenceId { get; set; }
  public int? CandidateId { get; set; }
  public int? RecordStatus { get; set; }
  public int? CreatedBy { get; set; }
  public DateTime? CreatedOn { get; set; }
  public int? ModifiedBy { get; set; } = 0;
  public DateTime? ModifiedOn { get; set; }
  public int? JobPostId { get; set; }
  public int? JobPostReferenceId { get; set; } = 0;
  public int? ApplicantType { get; set; }

  public int? OldApplicantId { get; set; }


}
