using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class JobPostingApplicantDetails
{
  public int Id { get; set; }
  public string? ApplicantName { get; set; }
  public string? ApplicantEmail { get; set; }
  public string? ApplicantJobTitle { get; set; }
  public string? ApplicantCity { get; set; }
  public string? ApplicantState { get; set; }
  public string? ApplicantZip { get; set; }
  public string? ApplicantAddress { get; set; }
  public string? ApplicantPhone { get; set; }
  public string? ApplicantAltPhone { get; set; }
  public int? ApplicantApplicantId { get; set; }
  public int? ApplicantCandidateId { get; set; }
  public int? ApplicantJobPostId { get; set; }
  public int? ApplicantJobCount { get; set; }
  public double ApplicantTotalCount { get; set; }
  public DateTime SubmitDate { get; set; }
  public List<StagesArray>? RecruitingParentStatus { get; set; }
  public List<SubStagesArray>? RecruitingSubStages { get; set; }
  public List<string> WorkingListQueues { get; set; }

  public List<string> CampaignsList { get; set; } 
  public string? ApplicantResumeId { get; set; }

  public string? ApplicantColorCode { get; set; }
  public List<TemPositions.IntelliStaff.Core.ApplicantAggregate.ApplicantJobDetails>? AppJobs { get; set; }

  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public int DivisionId { get; set; }
  public string? DivisionName { get; set; }
  public string? APISmallLogoPath { get; set; }
  public DateTime? ApplicantLatestInterviewDate { get; set; }
  public DateTime? LastLoginDate { get; set; }
  public int IsResumeAvilable { get; set; }

  public string? JobBoardName { get; set; }
  public int CandidateStatusId { get; set; }
  public string? CandidateStatus { get; set; }
  public string? CandidateSubStatus { get; set; }

}
