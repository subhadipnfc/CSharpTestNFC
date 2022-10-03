using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class SendCampaignRequest
{
  public int DivID { get; set; }
 public string? FirstName { get; set; }
  public int CandidateId { get; set; }
  public string? LastName { get; set; }
  public string? EMail { get; set; }
  public string? State { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public int CampaignId { get; set; }
  public int ApplicantId { get; set; }
  public int TenantId { get; set; }
  public string? Source { get; set; } = "TPA";
  public int UserId { get; set; }
  public DateTime? CreatedOn { get; set; }
  public string? HomePhone { get; set; }
  public int EntityTypeId { get; set; }
  public int EntityId { get; set; }

  public string? Entity { get; set; }
  public string? NotesText { get; set; }
  public RecruitingStageInput2 objStage { get; set; }

}
