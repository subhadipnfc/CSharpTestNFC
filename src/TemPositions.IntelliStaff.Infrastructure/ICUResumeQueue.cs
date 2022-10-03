using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
[Table("ICU_Resume_queue")]
public class ICUResumeQueue
{
  [Key]
  public int id { get; set; }
  [Column("resume_id")]
  public string? ResumeId { get; set; }
  [Column("unique_resume_id")]
  public string? UniqueResumeId { get; set; }
  [Column("first_name")]
  public string? FirstName { get; set; }
  [Column("last_name")]
  public string? LastName { get; set; }
  [Column("full_name")]
  public string? FullName { get; set; }
  public string? email { get; set; }
  public string? source { get; set; }
  public string? destination { get; set; }
  [Column("is_process")]
  public int IsProcess { get; set; }
  [Column("processed_date")]
  public DateTime ProcessedDate { get; set; }
  [Column("response_result")]
  public string? ResponseResult { get; set; }
  [Column("response_message")]
  public string? ResponseMessage { get; set; }
  public string? address { get; set; }
  public string? city { get; set; }
  public string? state { get; set; }
  public string? zip { get; set; }
  [Column("queue_file_id")]
  public int QueueFileId { get; set; }
  public string? filename { get; set; }
  [Column("file_extension")]
  public string? FileExtension { get; set; }
  [Column("cand_id")]
  public int CandId { get; set; }
  [Column("jobposting_applicant_id")]
  public int JobpostingApplicantId { get; set; }
  [Column("jobpost_candidate_map_id")]
  public int JobpostCandidateMapId { get; set; }
  [Column("processing_app")]
  public string? ProcessingApp { get; set; }
  [Column("firestore_response_result")]
  public string? FirestoreResponseResult { get; set; }
  [Column("firestore_response_message")]
  public string? FirestoreResponseMessage { get; set; }
  [Column("is_matching_process")]
  public int IsMatchingProcess { get; set; }
  [Column("match_response_results")]
  public string? MatchResponseResults { get; set; }
  [Column("match_response_message")]
  public string? MatchResponseMessage { get; set; }
  [Column("applicant_id")]
  public int ApplicantId { get; set; }
  [Column("created_date")]
  public DateTime CreateDate { get; set; }
  public int? NewApplicantId { get; set; }
}
