using TemPositions.IntelliStaff.Core.ApplicantAggregate;
using TemPositions.IntelliStaff.Core.Response;

namespace TemPositions.IntelliStaff.Core.Interfaces;
public interface IApplicantRepository
{
  public Task<IEnumerable<Core.ApplicantAggregate.Divisions>> GetDivisions();
  public Task<IEnumerable<Core.ApplicantAggregate.RecruitingStagesData>> GetRecruitingStages(int TenantId);
  public Task<IEnumerable<Core.ApplicantAggregate.JobBoardDetails>> GetJobBoardDetails();
  public Task<IEnumerable<Core.ApplicantAggregate.ApplicantFilters>> GetApplicantFiltersByDivision(int TenantId, int DivisionId);
  public Task<List<JobPostingApplicantDetails>> GetApplicantDetailsByApplicantId(RecruitingStageInput obj);
  public Task<string> InsertRecruitingStageByApplicantId(RecruitingStageInput obj);
  public Task<List<Core.ApplicantAggregate.NotesResponse>> GetNotesDetailsByApplicantId(ApplicantNotesRequest objReq);
  public Task<List<Core.ApplicantAggregate.NoteCategoriesData>> GetNoteCategories(int TenantId);
  public Task<List<Core.ApplicantAggregate.NotesResponse>> AddApplicantNotes(ApplicantNotesRequest objReq);
  public Task<IEnumerable<NotesResponse>> GetNoteslList(ApplicantNotesRequest objReq);
  public Task<bool> InsertRecruitingStageByEvent(RecruitingStageInput2 objReq);
  public Task<bool> AddApplicantToWorkList(CandInput objInput);
  public Task<List<Core.ApplicantAggregate.Campaign>> GetCampaignList(int TenantId );
  public Task<int>  CreateCandidateProfile(CandInput objCand);
  public Task<int> SendCampaignToCandidate(SendCampaignRequest objReq);
  public Task<IEnumerable<Core.ApplicantAggregate.JobDetailsResult>> GetApplicantJobDetails(int JobPostId);
  public Task SubmitOrder(CandInput objInput);
  public Task<List<Core.ApplicantAggregate.EmailTemplates>> GetEmailTemplates(int TenantId);
  public Task CreateEmailTemplateAsync(EmailTemplates ObjEmail);
  public Task<int> SendEmailsAsync(CandidateEmailList objEmails);
  public Task<IEnumerable<Core.ApplicantAggregate.JobPostingApplicantDetails>> GetJobPostApplicantAdvanceSerachDetails(ApplicantAdvanceSearchRequest objRequest);
  public Task<IEnumerable<Core.ApplicantAggregate.JobPostingApplicantDetails>> GetJobPostCandidateSerachDetails(int CandidateId);
  public Task<IEnumerable<Core.ApplicantAggregate.ApplicanLogoDetails>> GetApplicantLogo(int ApplicantId);
  public Task<EmailMessageData> GetOnBoardingEmailData(int CandidateId, string PhaseName, string AuthKey);
  public Task<int> CreateNewApplicant(ApplicantInput objApplicant);
}

