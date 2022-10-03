using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
[Table("RecuritmentStageDetail")]
public class RecuritmentStageDetail
{
  [Key]
  public int Id { get; set; }
  public int TenantId { get; set; }
  public int? RecruitingStageTypeId { get; set; }
  public int? ApplicantId { get; set; }
  public int? CandidateId { get; set; }
  public string? Comments { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? CreatedOn { get; set; }
  public int? ModifiedBy { get; set; }
  public DateTime? ModifiedOn { get; set; }
  public bool? IsRecruitingStageCompleted { get; set; }
  public int RecordStatus { get; set; } = 1;
}
