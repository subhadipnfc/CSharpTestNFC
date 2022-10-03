using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
[Table("RecuritmentSubStageDetail")]
public class RecuritmentSubStageDetail
{
  [Key]
  public int Id { get; set; }
  public int RecruitingStageId { get; set; }
  public int RecruitingSubStageId { get; set; }
  public int EntityId { get; set; }
  public int EntityTypeId { get; set; }
  public string? Comments { get; set; }
  public int CreatedBy { get; set; }
  public DateTime? CreatedOn { get; set; }
  public int? ModifiedBy { get; set; }
  public DateTime? ModifiedOn { get; set; }
  public bool? IsRecruitingStageCompleted { get; set; }
  public int RecordStatus { get; set; } = 1;
}
