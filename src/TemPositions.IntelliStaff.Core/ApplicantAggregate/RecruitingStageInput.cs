using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class RecruitingStageInput
{
  public int Id { get; set; }
  public int RecruitingStageTypeId { get; set; }
  public int RecruitingSubStageTypeId { get; set; }
  public int CandidateId { get; set; }
  public int ApplicantId { get; set; }

  public DateTime StageDate { get; set; }

  public string? Comments { get; set; }
  public int EntityId { get; set; }
  public string? EntityType { get; set; }

  public int EntityTypeId { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
  public int UserId { get; set; }

  public bool InsertNotes { get; set; }

  public string? StageName { get; set; }

  public int TenantId { get; set; } 
  public int InterviewDuration { get; set; }
  public bool IsRecruitingStageCompleted { get; set; } = false;
}
