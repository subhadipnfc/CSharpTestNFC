using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
[Table("applicantqueue_add_edit_audit")]
public class ApplicantQueueAudit
{
  [Key]
  [Column("id")]
  public int AuditId { get; set; }

  [Column("applicant_id")]
  public int? ApplicantQueueId { get; set; }

  [Column("email")]
  public string? Email { get; set; }

  [Column("div_id")]
  public int? DivisionId { get; set; }

  [Column("source_id")]
  public int? SourceId { get; set; }

  [Column("type_id")]
  public int? TypeId { get; set; }

  [Column("added_from_application_id")]
  public int? AddedFromApplicantionId { get; set; }

  [Column("is_automated")]
  public bool? IsAutomated { get; set; }

  [Column("resume_id")]
  public string? ResumeId { get; set; }

  [Column("jobpostapplicant_id")]
  public int? JobPostApplicantId { get; set; }

  [Column("created_date")]
  public DateTime? CreatedOn { get; set; }

  [Column("created_by")]
  public int? CreatedBy { get; set; }

  [Column("Matching_JopPosting_Applicant")]
  public int? MatchingJopPostingApplicant { get; set; }

}
