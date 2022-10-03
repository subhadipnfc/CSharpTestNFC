using Ardalis.Specification.EntityFrameworkCore;
using TemPositions.IntelliStaff.SharedKernel.Interfaces;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;
using Microsoft.EntityFrameworkCore;
using TemPositions.IntelliStaff.Core.Interfaces;
using System.Net;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore.Storage;
using TemPositions.IntelliStaff.Core.Response;
using System.Data;
using TemPositions.IntelliStaff.Core.ApplicantAggregate.Request;
using System.Net.Http.Headers;
using System.Globalization;

namespace TemPositions.IntelliStaff.Infrastructure.Data;
public class ApplicantRepository : IAggregateRoot, IApplicantRepository
{
  private readonly IntelliStaffContext intelliStaffContext;
  private IConfiguration Configuration;
  private readonly EmailConfiguration _emailConfig;
  private readonly Tempositions.Intellistaff.Shared.Logging.Core.ILogger _logger;
  private readonly HttpClient _httpClient;
  public ApplicantRepository(IntelliStaffContext context, IConfiguration _configuration,
    Tempositions.Intellistaff.Shared.Logging.Core.ILogger logger, EmailConfiguration _emailConfig,
    HttpClient httpClient)
  {
    this.intelliStaffContext = context;
    Configuration = _configuration;
    this._emailConfig = _emailConfig;
    _logger = logger;
    _httpClient = httpClient;
  }
  public async Task<IEnumerable<Core.ApplicantAggregate.Divisions>> GetDivisions()
  {
    try
    {
      if (intelliStaffContext != null)
      {
        return await (from d in intelliStaffContext.Divisions orderby d.Division ascending
                      select new Core.ApplicantAggregate.Divisions
                      {
                        Div_ID = d.Div_ID,
                        Division = d.Division,
                        TenantId = d.TenantId,
                      }).ToListAsync();
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex.Message.ToString());
      return null;
    }

  }
  public async Task<IEnumerable<Core.ApplicantAggregate.JobBoardDetails>> GetJobBoardDetails()
  {
    try
    {
      if (intelliStaffContext != null)
      {
        return await (from d in intelliStaffContext.JobBoardDetails orderby d.BoardName ascending
                      select new Core.ApplicantAggregate.JobBoardDetails
                      {
                        JobBoardId = d.JobBoardId,
                        BoardName = d.BoardName,
                        TenantId = d.TenantId,
                      }).ToListAsync();
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }

  }
  public async Task<IEnumerable<Core.ApplicantAggregate.RecruitingStagesData>> GetRecruitingStages(int TenantId)
  {
    try
    {
      if (intelliStaffContext != null)
      {
        return await (from a in intelliStaffContext.RecruitingSubStageType.AsNoTracking()
                      from d in intelliStaffContext.RecruitingStageType.AsNoTracking()
                        .Where(r => r.RecruitingStageTypeId == a.RecruitingStageTypeId).DefaultIfEmpty()
                        orderby d.DisplayOrder ascending
                      select new Core.ApplicantAggregate.RecruitingStagesData
                      {
                        RecruitingStageTypeId = d.RecruitingStageTypeId,
                        RecruitingStage = d.StageName,
                        RecruitingSubStageType = (from a1 in intelliStaffContext.RecruitingSubStageType
                                                  where a1.RecruitingStageTypeId == d.RecruitingStageTypeId
                                                  select new Core.ApplicantAggregate.RecruitingSubStageType
                                                  {
                                                    Id = a1.Id,
                                                    RecruitingStageTypeId = a1.RecruitingStageTypeId,
                                                    SubStageName = a1.SubStageName,
                                                    IsRecruitingStageCompleted = a1.IsRecruitingStageCompleted,
                                                    CreatedBy = a1.CreatedBy,
                                                    CreatedOn = a1.CreatedOn,
                                                    Status = a1.Status,
                                                  }

                                     ).ToList()

                      }
                      ).ToListAsync();
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }

  }
  public async Task<List<JobPostingApplicantDetails>> GetApplicantDetailsByApplicantId(RecruitingStageInput objRequest)
  {
    using (var transaction = intelliStaffContext.Database.BeginTransaction())
    {
      try
      {
        if (objRequest.TenantId == 0)
          objRequest.TenantId = 1;
        if (intelliStaffContext != null)
        {
          var temp = (from a in intelliStaffContext.RecuritmentStageDetail.AsNoTracking()
                      where a.RecruitingStageTypeId == objRequest.RecruitingStageTypeId && a.ApplicantId == objRequest.ApplicantId
                      select a).Count();
          if (temp == 0)
          {
            try
            {
              int StageId = 0;
              var stageDetails = new RecuritmentStageDetail
              {
                RecruitingStageTypeId = objRequest.RecruitingStageTypeId,
                ApplicantId = objRequest.ApplicantId,
                CandidateId = objRequest.CandidateId,
                Comments = objRequest.Comments,
                CreatedBy = objRequest.UserId,
                IsRecruitingStageCompleted = true,
                TenantId=objRequest.TenantId,
                RecordStatus=1,
                CreatedOn = Convert.ToDateTime(System.DateTime.Now),
              };
              intelliStaffContext.RecuritmentStageDetail.Add(stageDetails);
              await intelliStaffContext.SaveChangesAsync();
              StageId = stageDetails.Id;
              var stageSubDetails = new RecuritmentSubStageDetail
              {
                RecruitingStageId = StageId,
                EntityTypeId = objRequest.EntityTypeId,
                EntityId = objRequest.EntityId,
                RecruitingSubStageId = objRequest.RecruitingSubStageTypeId,
                Comments = objRequest.Comments,
                CreatedBy = objRequest.UserId,
                IsRecruitingStageCompleted = true,
                RecordStatus = 1,
                CreatedOn = Convert.ToDateTime(System.DateTime.Now),
              };
              intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
              await intelliStaffContext.SaveChangesAsync();
              
            }

            catch (Exception ex)
            {
              transaction.Rollback();
              return null;
            }
          }
          else
          {
            var objStage = (from a in intelliStaffContext.RecuritmentStageDetail.AsNoTracking()
                            where a.ApplicantId == objRequest.ApplicantId && a.RecruitingStageTypeId == objRequest.RecruitingStageTypeId
                            select a).Take(1);

            int SID = 0;
            if (objStage != null)
            {
              foreach (var stageObj in objStage)
              {
                SID = stageObj.Id;
              }
              var stageSubDetails = new RecuritmentSubStageDetail
              {
                RecruitingStageId = SID,
                EntityTypeId = objRequest.EntityTypeId,
                EntityId = objRequest.EntityId,
                RecruitingSubStageId = objRequest.RecruitingSubStageTypeId,
                Comments = objRequest.Comments,
                CreatedBy = objRequest.UserId,
                IsRecruitingStageCompleted = true,
                CreatedOn = Convert.ToDateTime(System.DateTime.Now),
                RecordStatus = 1,
              };
              intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
              await intelliStaffContext.SaveChangesAsync();
            }
          }
          var obj = await (from a in intelliStaffContext.Applicant.AsNoTracking()
                           from k in intelliStaffContext.JobPostDetails.AsNoTracking()
                         .Where(k1 => k1.JobPostId == a.JobPostId).DefaultIfEmpty()
                     from d in intelliStaffContext.Divisions.AsNoTracking()
                          .Where(d => d.Div_ID == a.DivisionId)
                    from logo in intelliStaffContext.DivisionTheme.AsNoTracking()
                          .Where(DT => DT.DivisionID == a.DivisionId).DefaultIfEmpty()
                           from ass in intelliStaffContext.ApplicantSubSource
                          .Where(ass => ass.Id == a.SubSourceID)
                     from j in intelliStaffContext.JobBoardDetails.AsNoTracking()
                         .Where(j => j.JobBoardId == ass.ApplicantSourceId).DefaultIfEmpty()
                     where a.Id == objRequest.ApplicantId
                     orderby a.SubmitDateTime descending
                     select new JobPostingApplicantDetails
                     {
                       Id = a.Id,
                       FirstName = a.FirstName,
                       LastName = a.LastName,
                       DivisionId = a.DivisionId.Value,
                       DivisionName = d.Division,
                       ApplicantName = a.FirstName + " " + a.LastName,
                       ApplicantEmail = a.Email,
                       ApplicantJobTitle = k.JobTitle,
                       ApplicantCity = a.City,
                       ApplicantState = a.State,
                       ApplicantZip = a.ZipCode,
                       ApplicantAddress = (a.Address ?? $"{a.City}-{a.State}-{a.ZipCode}").Trim('-'),
                       ApplicantPhone = a.CellPhoneNumber,
                       ApplicantAltPhone = a.HomePhoneNumber,
                       ApplicantApplicantId = a.Id,
                       ApplicantCandidateId = a.CandidateId,
                       ApplicantJobPostId = a.JobPostId,
                       SubmitDate = a.SubmitDateTime.Value,
                       APISmallLogoPath= logo.APISmallLogoPath,
                       ApplicantResumeId = (from res in intelliStaffContext.ICUResumeQueue
                                            .Where(o => o.NewApplicantId == a.Id && a.Id != 0) // TODO: && o.ResponseResult == "Success" Processing and Download logic should be separated
                                            select res.UniqueResumeId).FirstOrDefault(),
                       AppJobs = (from a1 in intelliStaffContext.Applicant.AsNoTracking()
                                  from ass in intelliStaffContext.ApplicantSubSource.AsNoTracking()
                                      .Where(ass => ass.Id == a1.SubSourceID)
                                  from j in intelliStaffContext.JobBoardDetails.AsNoTracking()
                                      .Where(j => j.JobBoardId == ass.ApplicantSourceId).DefaultIfEmpty()
                                  from k in intelliStaffContext.JobPostDetails.AsNoTracking()
                                    .Where(k1 => k1.JobPostId == a1.JobPostId).DefaultIfEmpty()
                                  where a1.Email == a.Email
                                  select new Core.ApplicantAggregate.ApplicantJobDetails
                                  {
                                    JobTitle = k.JobTitle,
                                    JobDate = a1.SubmitDateTime.ToString()

                                  }).ToList(),

                       ApplicantJobCount = (from a1 in intelliStaffContext.Applicant where a1.Email == a.Email select a).Count(),
                       RecruitingParentStatus = (from a1 in intelliStaffContext.RecuritmentStageDetail.AsNoTracking()
                                                 from j1 in intelliStaffContext.RecruitingStageType.AsNoTracking()
                                                  .Where(j => j.RecruitingStageTypeId == a1.RecruitingStageTypeId)
                                                 where a1.ApplicantId == a.Id
                                                 select new Core.ApplicantAggregate.StagesArray
                                                 {
                                                   RecruitingStageId = a1.RecruitingStageTypeId,
                                                   ParentStageName = j1.StageName,
                                                   IsRecruitingStageCompleted = a1.IsRecruitingStageCompleted.Value,
                                                 }
                                       ).Distinct().ToList(),
                       RecruitingSubStages = (from a1 in intelliStaffContext.RecuritmentStageDetail.AsNoTracking()
                                              from b in intelliStaffContext.RecruitingSubStageDetail.AsNoTracking()
                                              .Where(o => o.RecruitingStageId == a1.Id)
                                              from j1 in intelliStaffContext.RecruitingSubStageType.AsNoTracking()
                                               .Where(j => j.Id == b.RecruitingSubStageId)
                                              from j2 in intelliStaffContext.User_Names.AsNoTracking()
                                              .Where(o => o.UserID == a1.CreatedBy)
                                              where a1.ApplicantId == a.Id
                                              orderby b.Id descending
                                              select new SubStagesArray
                                              {
                                                RecruitingStageId = a1.RecruitingStageTypeId,
                                                RecrutingStage = j1.SubStageName,
                                                RecruitingSubStageId = b.RecruitingSubStageId,
                                                IsRecruitingStageCompleted = a1.IsRecruitingStageCompleted.Value,
                                                CreatedOn = a1.CreatedOn,
                                                UserName = j2.Name,
                                                Comments=a1.Comments,
                                              }
                                     ).Distinct().ToList(),
                       IsResumeAvilable = (from icq in intelliStaffContext.ICUResumeQueue.AsNoTracking()
                                           from b in intelliStaffContext.IcuResumeQueueFile.AsNoTracking()
                                           
                                           .Where(ass => ass.queue_file_id == icq.id && icq.IsProcess == 1 && icq.ApplicantId == a.OldApplicantId && a.OldApplicantId != 0)
                                           select icq.IsProcess).FirstOrDefault(),

                       WorkingListQueues = (from w1 in intelliStaffContext.workinglistitem.AsNoTracking()
                                            from w2 in intelliStaffContext.WorkingListNew.AsNoTracking()
                                             .Where(j1 => j1.WorkingListId == w1.WorkingListId)
                                            where w1.CandidateID == a.CandidateId
                                            orderby w1.CreatedOn descending
                                            select w2.WorkingListName
                                            ).Distinct().ToList(),
                       CampaignsList = (from w1 in intelliStaffContext.Campaign_Invitations.AsNoTracking()
                                        from w2 in intelliStaffContext.Campaign.AsNoTracking()
                                             .Where(j1 => j1.Id == w1.CampaignId)
                                        where w1.ApplicantId == a.OldApplicantId
                                        orderby w1.CreatedDate descending
                                        select w2.Name
                                            ).Distinct().ToList(),
                       ApplicantTotalCount = 0,
                       ApplicantLatestInterviewDate = (from order in intelliStaffContext.Interview.AsNoTracking() where objRequest.CandidateId != 0 && order.EntityId == objRequest.CandidateId
                                                       orderby order.Id descending
                                                       select order.InterviewDate).FirstOrDefault(),
                       LastLoginDate = (from y in intelliStaffContext.UserLoginInfo.AsEnumerable() where (y.UserId == objRequest.UserId) orderby y.Id descending select y.LoginTime).SingleOrDefault(),

                       ApplicantColorCode = String.IsNullOrEmpty(a.SubmitDateTime.Value.ToString()) ? "red" : ((DateTime.Now - a.SubmitDateTime.Value).Days == 0 ? "green" : (DateTime.Now - a.SubmitDateTime.Value).Days > 7 ? "red" : "orange"),
                       JobBoardName = ass.SubSourceName,
                       CandidateStatusId = (from cg in intelliStaffContext.CanProfGen.AsNoTracking()
                                         .Where(o => o.CandID == a.CandidateId && a.CandidateId != 0).DefaultIfEmpty()
                                            from cs in intelliStaffContext.CandidateStatus.AsNoTracking()
                                            .Where(cs => cs.Id == cg.StatusType && cg.StatusType != 0).DefaultIfEmpty()
                                            select cs.Id).FirstOrDefault(),
                       CandidateStatus = (from cg in intelliStaffContext.CanProfGen.AsNoTracking()
                                         .Where(o => o.CandID == a.CandidateId && a.CandidateId != 0).DefaultIfEmpty()
                                         from cs in intelliStaffContext.CandidateStatus.AsNoTracking()
                                         .Where(cs => cs.Id == cg.StatusType && cg.StatusType!= 0).DefaultIfEmpty()
                                         select cs.Name).FirstOrDefault(),
                      CandidateSubStatus= (from cg in intelliStaffContext.CanProfGen.AsNoTracking()
                                           .Where(o=>o.CandID == a.CandidateId && a.CandidateId != 0).DefaultIfEmpty()
                                           from css in intelliStaffContext.CandidateSubStatus.AsNoTracking()
                                           .Where(css => css.Id == cg.SubStatusType && cg.SubStatusType!= 0).DefaultIfEmpty()
                                           select css.Name).FirstOrDefault()
                     }).Take(1).ToListAsync();


          transaction.Commit();
          if(obj != null && obj.Count > 0)
          {
            obj[0].AppJobs = obj[0].AppJobs.Distinct().ToList();
          }
          return obj;
        }

        return null;
      }
      catch (Exception ex)
      {
        transaction.RollbackAsync();
        _logger.Log(ex);
        return null;
      }
    }
  }
  public async  Task<bool> InsertRecruitingStageByApplicantId(RecruitingStageInput objRequest)
  {
    bool isSuccess = false;
    using (var transaction = intelliStaffContext.Database.BeginTransaction())
    {
      try
      {

        if (intelliStaffContext != null)
        {
          var temp = (from a in intelliStaffContext.RecuritmentStageDetail
                      where a.RecruitingStageTypeId == objRequest.RecruitingStageTypeId && a.ApplicantId == objRequest.ApplicantId
                      select a).Count();
          if (temp == 0)
          {
            try
            {
              var stageDetails = new RecuritmentStageDetail
              {
                RecruitingStageTypeId = objRequest.RecruitingStageTypeId,
                TenantId = objRequest.TenantId,
                ApplicantId = objRequest.ApplicantId,
                CandidateId = objRequest.CandidateId,
                Comments = objRequest.Comments,
                CreatedBy = objRequest.UserId,
                IsRecruitingStageCompleted = true,
                CreatedOn = Convert.ToDateTime(System.DateTime.Now),
              };
              intelliStaffContext.RecuritmentStageDetail.Add(stageDetails);
              await intelliStaffContext.SaveChangesAsync();
              var stageSubDetails = new RecuritmentSubStageDetail
              {
                RecruitingStageId = objRequest.RecruitingStageTypeId,
                EntityTypeId = objRequest.EntityTypeId,
                EntityId = objRequest.EntityId,
                RecruitingSubStageId = objRequest.RecruitingSubStageTypeId,
                Comments = objRequest.Comments,
                CreatedBy = objRequest.UserId,
                IsRecruitingStageCompleted = true,
                RecordStatus = 1,
                CreatedOn = Convert.ToDateTime(System.DateTime.Now),
              };
              intelliStaffContext.RecuritmentStageDetail.Add(stageDetails);
              await intelliStaffContext.SaveChangesAsync();
              isSuccess = true;
              transaction.Commit();
            }

            catch (Exception ex)
            {
              transaction.Rollback();
              return isSuccess;
            }

          }
          else
          {
            var objRecruitingStage = (from p in intelliStaffContext.RecuritmentStageDetail where p.ApplicantId == objRequest.ApplicantId select p).Take(1);
            foreach (var stage in objRecruitingStage)
            {
              stage.IsRecruitingStageCompleted = objRequest.IsRecruitingStageCompleted;
              intelliStaffContext.RecuritmentStageDetail.Update(stage);
            }
            await intelliStaffContext.SaveChangesAsync();

          }
          transaction.Commit();
          return isSuccess;
        }

        return isSuccess;
      }
      catch (Exception ex)
      {
        isSuccess = false;
        _logger.Log(ex);
        return isSuccess;
      }
    }
  }
  public async Task<bool> InsertRecruitingStageByEvent(RecruitingStageInput2 objRequest)
  {
    bool isSuccess = false;
    
    using (var transaction = intelliStaffContext.Database.BeginTransaction())
    {
      try
      {
        CandInput objInput = objRequest.CandidateObj;
        Applicant objApplicant = intelliStaffContext.Applicant.Single(p => p.Id == objRequest.ApplicantId);

        int CandidateId = 0;
        if (objRequest != null && (Convert.ToInt32(objApplicant.CandidateId) == 0 || Convert.ToInt32(objApplicant.CandidateId) == 1))
        {
          CandidateId = await CreateCandidate(objInput, CandidateId, transaction);
        }


        if (intelliStaffContext != null)
        {
          if (objRequest.StageName?.ToUpper().Trim() == "QUALIFICATION")
          {
            var qualif = new Qualification
            {
              Comments = objRequest.Comments,
              CreatedBy = objRequest.UserId,
              CreatedOn = DateTime.Now,
              EntityId = objRequest.EntityId,
              EntityType = objRequest.EntityType,
              Status = 1,
            };
            intelliStaffContext.Qualification.Add(qualif);
            await intelliStaffContext.SaveChangesAsync();

          }
          else if (objRequest.StageName?.ToUpper().Trim() == "PROFILE REVIEW")
          {
            var app_Notes = new AppNotes
            {
              Datein = DateTime.Now,
              Appid = objRequest.ApplicantId,
              Activ = objRequest.Comments,
              Userid = objRequest.UserId,
              Category = null,
              CategoryType = 0,
            };
            intelliStaffContext.AppNotes.Add(app_Notes);
            await intelliStaffContext.SaveChangesAsync();
            var appNotes = new ApplicantNotes
            {
              CreatedOn = DateTime.Now,
              ApplicantId = objRequest.ApplicantId,
              CreatedBy = objRequest.UserId,
              TenantId = objRequest.TenantId,
              IsAutomated = 0,
              RecordStatus = 1,
              Activity = objRequest.Comments
            };
            intelliStaffContext.ApplicantNotes.Add(appNotes);
            await intelliStaffContext.SaveChangesAsync();
            var profileReview = new ProfileReview
            {
              ReviewDate = DateTime.Now,
              CreatedBy = objRequest.UserId,
              CreatedOn = DateTime.Now,
              Comments = objRequest.Comments,
              EntityId = objRequest.EntityId,
              EntityType = objRequest.EntityTypeId,
              Status = 1,
            };
            intelliStaffContext.ProfileReview.Add(profileReview);
            await intelliStaffContext.SaveChangesAsync();
          }
          else if (objRequest.StageName?.ToUpper().Trim() == "CONTACTED")
          {
            if (objRequest.CandidateObj != null && (Convert.ToInt32(CandidateId) == 0 || Convert.ToInt32(CandidateId) == 1))
            {
              CandidateId= await CreateCandidate(objInput, CandidateId, transaction);
              objRequest.CandidateId = CandidateId;
              objRequest.EntityTypeId = 2;
              objRequest.EntityId = CandidateId;
            }
            else
            {
              objRequest.EntityTypeId = 2;
              objRequest.EntityId = CandidateId;
            }
            var profileContacted = new ProfileContacted
            {
              ContactedDate = DateTime.Now,
              CreatedBy = objRequest.UserId,
              CreatedOn = DateTime.Now,
              Comments = objRequest.Comments,
              EntityId = objRequest.EntityId,
              EntityType = objRequest.EntityTypeId,
              Status = 1,
              ContactType = objRequest.EntityTypeId,
            };
            intelliStaffContext.ProfileContacted.Add(profileContacted);
            await intelliStaffContext.SaveChangesAsync();
          }
          else if (objRequest.StageName?.ToUpper().Trim() == "INTERVIEW")
          {
            if (objRequest.CandidateObj != null && (Convert.ToInt32(CandidateId) == 0 || Convert.ToInt32(CandidateId) == 1))
            {
              CandidateId= await CreateCandidate(objInput, CandidateId, transaction);
              objRequest.CandidateId = CandidateId;
              objRequest.EntityTypeId = 2;
              objRequest.EntityId = CandidateId;
            }
            var interview = new Interview
            {
              InterviewDate = objRequest.StageDate,
              CreatedBy = objRequest.UserId,
              CreatedOn = DateTime.Now,
              Comments = objInput.NotesText,
              EntityId = objRequest.EntityId,
              EntityType = objRequest.EntityTypeId,
              Duration = objRequest.InterviewDuration,
              Status = 1,
              TenantId = objRequest.TenantId,

            };
            intelliStaffContext.Interview.Add(interview);
            await intelliStaffContext.SaveChangesAsync();
            objRequest.EntityId = CandidateId;
          }
          else if (objRequest.StageName?.ToUpper().Trim() == "OnSpot Interview".ToUpper())
          {
            if (objRequest.CandidateObj != null && Convert.ToInt32(CandidateId) == 0)
              CandidateId= await CreateCandidate(objInput, CandidateId, transaction);
            objRequest.EntityTypeId = 2;
            objRequest.EntityId = CandidateId;
          }
          else if (objRequest.StageName?.ToUpper().Trim() == "Onboarding Call".ToUpper())
          {
            if (objRequest.CandidateObj != null && Convert.ToInt32(CandidateId) == 0)
              CandidateId = await CreateCandidate(objInput, CandidateId, transaction);
            objRequest.EntityTypeId = 2;
            objRequest.EntityId = CandidateId;
          }
          await UpdateOldApplicant(objInput, objApplicant, CandidateId);

          var temp = (from a in intelliStaffContext.RecuritmentStageDetail
                      where a.RecruitingStageTypeId == objRequest.RecruitingStageTypeId && a.ApplicantId == objRequest.ApplicantId && a.CandidateId == (Convert.ToInt32(objRequest.CandidateId) > 0 ? objRequest.CandidateId : a.CandidateId)
                      select a).Count();

          var subStageId = (from s in intelliStaffContext.RecruitingSubStageType
                            where s.Id == objRequest.RecruitingSubStageTypeId
                            select (s.IsRecruitingStageCompleted == 0 ? false : true)).Single();
          if (temp == 0)
          {
            int SID = 0;
            var stageDetails = new RecuritmentStageDetail
            {
              RecruitingStageTypeId = objRequest.RecruitingStageTypeId,
              ApplicantId = objRequest.ApplicantId,
              TenantId = objRequest.TenantId,
              CandidateId = objRequest.CandidateId,
               Comments = objRequest.Comments,
              CreatedBy = objRequest.UserId,
              IsRecruitingStageCompleted = subStageId,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecuritmentStageDetail.Add(stageDetails);
            await intelliStaffContext.SaveChangesAsync();
            SID = stageDetails.Id;
            var stageSubDetails = new RecuritmentSubStageDetail
            {
              RecruitingStageId = SID,
              EntityTypeId = objRequest.EntityTypeId,
              EntityId = objRequest.EntityId,
              RecruitingSubStageId = objRequest.RecruitingSubStageTypeId,
              Comments = objRequest.Comments,
              CreatedBy = objRequest.UserId,
              RecordStatus = 1,
              IsRecruitingStageCompleted = subStageId,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
            await intelliStaffContext.SaveChangesAsync();
          }
          else
          {
            var objStage = (from a in intelliStaffContext.RecuritmentStageDetail
                            where a.RecruitingStageTypeId == objRequest.RecruitingStageTypeId && a.ApplicantId == objRequest.ApplicantId
                            select a).Take(1);

            int SID = 0;
            foreach (var stageObj in objStage)
            {
              stageObj.IsRecruitingStageCompleted = subStageId;
              SID = stageObj.Id;
              intelliStaffContext.RecuritmentStageDetail.Update(stageObj);
            }
            var stageSubDetails = new RecuritmentSubStageDetail
            {
              RecruitingStageId = SID,
              EntityTypeId = objRequest.EntityTypeId,
              EntityId = objRequest.EntityId,
              RecruitingSubStageId = objRequest.RecruitingSubStageTypeId,
              Comments = objRequest.Comments,
              CreatedBy = objRequest.UserId,
              RecordStatus = 1,
              IsRecruitingStageCompleted = subStageId,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
            await intelliStaffContext.SaveChangesAsync();
          }
        }
        if (objRequest.InsertNotes)
        {
          if (CandidateId == 0)
          {
            objRequest.EntityId = objRequest.ApplicantId;
            objRequest.EntityTypeId = 1;
          }
          else
          {
            objRequest.EntityId = CandidateId;
            objRequest.EntityTypeId = 2;
          }

          AddNoteByStage(objRequest);
        }

        transaction.Commit();
        isSuccess = true;
        return isSuccess;
      }
      catch (Exception ex)
      {
        isSuccess = false;
        transaction.Rollback();
        _logger.Log(ex);
        return isSuccess;
      }
    }
  }

  private async Task UpdateOldApplicant (CandInput objInput, Applicant objApplicant, int CandidateId)
  {
    if (objApplicant.OldApplicantId == 0)
    {
      var appQueue = new ApplicantQueue
      {

        Div_ID = objApplicant.DivisionId,
        First_Name = objInput.FirstName,
        Last_Name = objInput.LastName,
        E_Mail = objInput.Email,
        Address = objInput.Address,
        City = objInput.City,
        State = objInput.STATE,
        CreatedOn = DateTime.Now,
        CFHC = false,
        NewApplicantId = objInput.ApplicantId,
        TenantId = objInput.TenantId,
        PhaseComplete = "",
        agent_i9_verification_req = false,
        cand_id = CandidateId,
      };
      intelliStaffContext.ApplicantQueue.Add(appQueue);
      await intelliStaffContext.SaveChangesAsync();

      objApplicant.OldApplicantId = appQueue.Applicant_ID;
      intelliStaffContext.Applicant.Update(objApplicant);
      await intelliStaffContext.SaveChangesAsync();

    }
    else
    {
      try
      {
        if (objApplicant.OldApplicantId > 0)
        {
          var objAppQueue = (from p in intelliStaffContext.ApplicantQueue.AsNoTracking().Where(o => o.Applicant_ID == objApplicant.OldApplicantId) select p);
          foreach (var app in objAppQueue)
          {
            app.cand_id = CandidateId;
            intelliStaffContext.ApplicantQueue.Update(app);
          }
          await intelliStaffContext.SaveChangesAsync();
        }
      }
      catch (Exception ex)
      {
        _logger.Log(ex);
      }
    }
  }
  private async Task<int> CreateCandidate(CandInput objInput, int cid, IDbContextTransaction candTrans)
  {
    try
    {
      if (objInput.Candid == 0 || objInput.Candid == 1)
      {
        CanProfGen ExistingCandidate = await intelliStaffContext.CanProfGen.Where(x => x.EMail != null && x.EMail == objInput.Email).FirstOrDefaultAsync();
        if (ExistingCandidate != null)
        {
          cid = ExistingCandidate.CandID;
        }
        else
        {
          cid = intelliStaffContext.CanProfGen.Max(p => p.CandID) + 1;
          var obj = new CanProfGen
          {

            Divid = objInput.DivId,
            Address = objInput.Address,
            City = objInput.City,
            EMail = objInput.Email,
            Dateenter = DateTime.Now,
            HomePhone = String.IsNullOrEmpty(objInput.HomePhone) ? "" : (objInput.HomePhone.Length > 12) ? objInput.HomePhone.Substring(0, 13) : objInput.HomePhone,
            FirstName = objInput.FirstName,
            LastName = objInput.LastName,
            State = objInput.STATE,
            TenantId = objInput.TenantId,
            office = objInput.Office,
            CreatedBy = objInput.UserId,
            CreatedOn = DateTime.Now,
            SSN = objInput.ssn,
            CandID = cid

          };
          objInput.Candid = cid;

          intelliStaffContext.CanProfGen.Add(obj);
          await intelliStaffContext.SaveChangesAsync();
        }
      }
      else
        cid = objInput.Candid;

      //need to update candidate id in applicant table

      await MapApplicantWithCandidateId(objInput.Email, objInput.ApplicantId, cid);

      return cid;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return cid;
    }
    
  }

  private async Task MapApplicantWithCandidateId(string Email, int ApplicantId, int CandidateId)
  {
    var objApplicant = await (from p in intelliStaffContext.Applicant where p.Email == Email select p).ToListAsync();
    foreach (var app in objApplicant)
    {

      app.CandidateId = CandidateId;
      intelliStaffContext.Applicant.Update(app);

    }
    var appQueueCount = (from a in intelliStaffContext.ApplicantQueue.AsNoTracking()
                         where a.NewApplicantId == ApplicantId
                         select a).Count();
    try
    {

      if (appQueueCount > 0)
      {
        ApplicantQueue objAppQueue = intelliStaffContext.ApplicantQueue.AsNoTracking().Single(p => p.NewApplicantId == ApplicantId);
        if (objAppQueue != null)
        {
          objAppQueue.cand_id = CandidateId;
          intelliStaffContext.ApplicantQueue.Update(objAppQueue);
        }

      }

      await intelliStaffContext.SaveChangesAsync();

    }
    catch (Exception ex)
    {
      _logger.Log(ex);
    }
  }

  public async Task<bool> AddApplicantToWorkList(CandInput objInput)
  {
    try
    {
      Applicant objApplicant = intelliStaffContext.Applicant.Single(p => p.Id == objInput.ApplicantId);
      int cid = 0;
      int StageId = 0;
      var subStageId = false;
      if (objApplicant != null && objApplicant.CandidateId != null)
        cid = objApplicant.CandidateId.Value;
      using (var transaction = intelliStaffContext.Database.BeginTransaction())
      {
        if (intelliStaffContext != null)
        {
          if (cid == 0)
          {
            cid = intelliStaffContext.CanProfGen.Max(p => p.CandID) + 1;
            var obj = new CanProfGen
            {

              Divid = objApplicant.DivisionId.Value,
              Address = objInput.Address,
              City = objInput.City,
              EMail = objInput.Email,
              Dateenter = DateTime.Now,
              HomePhone = objInput.HomePhone,
              FirstName = objInput.FirstName,
              LastName = objInput.LastName,
              State = objInput.STATE,
              TenantId = objInput.TenantId,
              office = objInput.Office > 0 ? (byte)1 : (byte)0,
              CreatedBy = objInput.UserId,
              CreatedOn = DateTime.Now,
              SSN = objInput.ssn,
              CandID = cid

            };
            intelliStaffContext.CanProfGen.Add(obj);
            await intelliStaffContext.SaveChangesAsync();

            //need to update candidate id in applicant table
            var objApplicant2 = (from p in intelliStaffContext.Applicant where p.Email == objInput.Email select p);
            foreach (var app in objApplicant2)
            {
              app.CandidateId = cid;
              objInput.Candid = cid;
              intelliStaffContext.Applicant.Update(app);
            }

            // executes the appropriate commands to implement the changes to the database  
            await intelliStaffContext.SaveChangesAsync();

          }
          else
            cid = objInput.Candid;

          await UpdateOldApplicant(objInput, objApplicant, cid);
          var temp = (from a in intelliStaffContext.RecuritmentStageDetail
                      where a.RecruitingStageTypeId == objInput.RecruitingStageTypeId && a.ApplicantId == objInput.ApplicantId && a.CandidateId == (Convert.ToInt32(objInput.Candid) > 0 ? objInput.Candid : a.CandidateId)
                      select a).Count();

          subStageId = (from s in intelliStaffContext.RecruitingSubStageType
                            where s.Id == objInput.RecruitingSubStageTypeId
                            select (s.IsRecruitingStageCompleted == 0 ? false : true)).Single();
          if (temp == 0)
          {
            int SID = 0;
            var stageDetails = new RecuritmentStageDetail
            {
              RecruitingStageTypeId = objInput.RecruitingStageTypeId,
              ApplicantId = objInput.ApplicantId,
              CandidateId = objInput.Candid,
              TenantId = objInput.TenantId,
              Comments = objInput.NotesText,
              CreatedBy = objInput.UserId,
              IsRecruitingStageCompleted = subStageId,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecuritmentStageDetail.Add(stageDetails);
            await intelliStaffContext.SaveChangesAsync();
            SID = stageDetails.Id;
            var stageSubDetails = new RecuritmentSubStageDetail
            {
              RecruitingStageId = SID,
              EntityTypeId = objInput.EntityTypeId,
              EntityId = objInput.EntityId,
              RecruitingSubStageId = objInput.RecruitingSubStageTypeId,
              Comments = objInput.NotesText,
              CreatedBy = objInput.UserId,
              RecordStatus = 1,
              IsRecruitingStageCompleted = subStageId,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
            await intelliStaffContext.SaveChangesAsync();
          }
          else
          {
            var objStage = (from a in intelliStaffContext.RecuritmentStageDetail
                            where a.RecruitingStageTypeId == objInput.RecruitingStageTypeId && a.ApplicantId == objInput.ApplicantId
                            select a).Take(1);

            int SID = 0;
            foreach (var stageObj in objStage)
            {
              stageObj.IsRecruitingStageCompleted = subStageId;
              SID = stageObj.Id;
              intelliStaffContext.RecuritmentStageDetail.Update(stageObj);
            }
            await intelliStaffContext.SaveChangesAsync();
            //update the recruitingstage table
            var stageSubDetails = new RecuritmentSubStageDetail
            {
              RecruitingStageId = SID,
              EntityTypeId = objInput.EntityTypeId,
              EntityId = objInput.EntityId,
              RecruitingSubStageId = objInput.RecruitingSubStageTypeId,
              Comments = objInput.NotesText,
              CreatedBy = objInput.UserId,
              IsRecruitingStageCompleted = true,
              RecordStatus = 1,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
            await intelliStaffContext.SaveChangesAsync();
          }

          var intCount = (from a in intelliStaffContext.workinglistitem
                          where a.CandidateID == cid
                          select a).Count();
          if (intCount == 0 && cid > 0)
          {
            WorkingListItem objItem = new WorkingListItem();
            objItem.WorkingListId = objInput.WorkingListId;
            objItem.ItemId = cid;
            objItem.SourceId = objInput.SourceId;
            objItem.EntityTypeId = objInput.EntityTypeId;
            objItem.CandidateID = cid;
            objItem.CreatedOn = DateTime.Now;
            objItem.CreatedBy = objInput.UserId;

            intelliStaffContext.workinglistitem.Add(objItem);
            await intelliStaffContext.SaveChangesAsync();
          }

            bool res = false;
          if (!String.IsNullOrEmpty(objInput.NotesText))
          {
            if (cid == 0)
            {
              objInput.EntityId = objInput.ApplicantId;
              objInput.EntityTypeId = 1;
            }
            else
            {
              objInput.EntityId = cid;
              objInput.EntityTypeId = 2;
            }

            res = await AddNote(objInput);
          }
        }
        transaction.Commit();
        return true;
      }
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  private void AddNoteByStage(RecruitingStageInput2 objRequest)
  {
    ApplicantNotesRequest objReq = new ApplicantNotesRequest();
    objReq.NotesText = objRequest.Comments;
    objReq.CreatedOn = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
    objReq.CreatedBy = objRequest.UserId;
    objReq.EntityTypeId = objRequest.EntityTypeId;
    objReq.EntityId = objRequest.EntityId;
    objReq.TenantId = objRequest.TenantId;
    objReq.RecruitingStageName = objRequest.StageName;
    objReq.CategoryTypeId = 0;
    List<NotesResponse> obj1 = new List<NotesResponse>();
    string result = "";

    var objJson = new
    {
      NotesId = objReq.NotesId,
      ApplicantId = objRequest.ApplicantId,
      NotesText = objReq.NotesText,
      CreatedBy = objReq.CreatedBy,
      CreatedOn = objReq.CreatedOn,
      ModifiedOn = objReq.ModifiedOn,
      ModifiedBy = objReq.ModifiedBy,
      TenantId = objReq.TenantId,
      EntityTypeId = objReq.EntityTypeId,
      EntityId = objReq.EntityId,
      CategoryTypeId = objReq.CategoryTypeId,
      RecruitingStageName = objReq.RecruitingStageName
    };

    var body = JsonConvert.SerializeObject(objJson);

      using (WebClient client = new WebClient())
      {
        var URL = this.Configuration.GetSection("Api")["NotesServiceAPI"];
        client.BaseAddress = URL + "AddEditNotesDetails/";
        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        client.Headers[HttpRequestHeader.Accept] = "application/json";
        client.Encoding = System.Text.Encoding.UTF8;
        result = client.UploadString(client.BaseAddress, "POST", body);
        var response = JsonConvert.DeserializeObject(result);
        if (response != null)
        {
          obj1 = JsonConvert.DeserializeObject<List<NotesResponse>>(response.ToString());
        }
      }    
  }
  private Task<bool> AddNote(CandInput objInput)
  {
    bool isSuccess = false;   
      try
      {
        ApplicantNotesRequest objReq = new ApplicantNotesRequest();
        objReq.NotesText = objInput.NotesText;
        objReq.CreatedOn = DateTime.Now.ToString("yyyy-MM-dd").ToString();
        objReq.CreatedBy = objInput.UserId;
        objReq.EntityTypeId = objInput.EntityTypeId;
        objReq.EntityId = objInput.EntityId;
        objReq.TenantId = objInput.TenantId;
        objReq.RecruitingStageName = objInput.RecruitingStageName;
        objReq.CategoryTypeId = 0;
        //logic to insert notes
        List<NotesResponse> obj1 = new List<NotesResponse>();
        string result = "";
        StringBuilder objJson = new StringBuilder();
        objJson.Append("{");
        objJson.Append("\"NotesId\": \"" + objReq.NotesId + "\",");
        objJson.Append("\"ApplicantId\": \"" + objInput.ApplicantId + "\",");
        objJson.Append("\"NotesText\": \"" + objReq.NotesText + "\",");
        objJson.Append("\"CreatedBy\": \"" + objReq.CreatedBy + "\",");
        objJson.Append("\"CreatedOn\": \"" + objReq.CreatedOn + "\",");
        objJson.Append("\"ModifiedOn\": \"" + objReq.ModifiedOn + "\",");
        objJson.Append("\"ModifiedBy\": \"" + objReq.ModifiedBy + "\",");
        objJson.Append("\"TenantId\": \"" + objReq.TenantId + "\",");
        objJson.Append("\"EntityTypeId\": \"" + objReq.EntityTypeId + "\",");
        objJson.Append("\"EntityId\": \"" + objReq.EntityId + "\",");
        objJson.Append("\"CategoryTypeId\": \"" + objReq.CategoryTypeId + "\",");
        objJson.Append("\"RecruitingStageName\": \"" + objReq.RecruitingStageName + "\"");
        objJson.Append("}");

        var body = JsonConvert.SerializeObject(objJson);

        using (WebClient client = new WebClient())
        {
          var URL = this.Configuration.GetSection("Api")["NotesServiceAPI"];
          client.BaseAddress = URL + "AddEditNotesDetails/";
          client.Headers[HttpRequestHeader.ContentType] = "application/json";
          client.Headers[HttpRequestHeader.Accept] = "application/json";
          client.Encoding = System.Text.Encoding.UTF8;
          result = client.UploadString(client.BaseAddress, "POST", objJson.ToString());
          var response = JsonConvert.DeserializeObject(result);
          if (response != null)
          {
            obj1 = JsonConvert.DeserializeObject<List<NotesResponse>>(response.ToString());
          }
        }

        return Task.FromResult(isSuccess);
      }
      catch (Exception ex)
      {
        _logger.Log(ex);
        return Task.FromResult(isSuccess);
      }

      return Task.FromResult(isSuccess);    
  }
  public async Task<IEnumerable<Core.ApplicantAggregate.ApplicantFilters>> GetApplicantFiltersByDivision(int TenantId, int DivisionId)
  {
    try
    {
      var objFilters = new List<Core.ApplicantAggregate.ApplicantFilters>();
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 1,
        LabelName = "FirstName",
      });
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 2,
        LabelName = "LastName",
      });
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 3,
        LabelName = "Email",
      });
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 4,
        LabelName = "DivisionId",
      });
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 5,
        LabelName = "Status",
      });
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 6,
        LabelName = "JobBoard",
      });
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 7,
        LabelName = "FromDate",
      });
      objFilters.Add(new Core.ApplicantAggregate.ApplicantFilters
      {
        DivisionId = DivisionId,
        IsDefaultSearchFilter = true,
        PriorityOrder = 7,
        LabelName = "ToDate",
      });
      return await Task.FromResult(objFilters);
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }
  }
  public async Task<List<Core.ApplicantAggregate.NotesResponse>> GetNotesDetailsByApplicantId(ApplicantNotesRequest objReq)
  {

    List<Core.ApplicantAggregate.NotesResponse> objResp = new List<NotesResponse>();
    try
    {
      string result = "";

      StringBuilder objJson = new StringBuilder();
      objJson.Append("{");
      objJson.Append("\"TenantId\": \"" + objReq.TenantId + "\",");
      objJson.Append("\"EntityId\": \"" + objReq.EntityId + "\",");
      objJson.Append("\"EntityTypeId\": \"" + objReq.EntityTypeId + "\",");
      objJson.Append("\"ApplicantId\": \"" + objReq.ApplicantId + "\",");
      objJson.Append("\"CreatedBy\": \"" + objReq.CreatedBy + "\"");
      objJson.Append("}");

      var body = JsonConvert.SerializeObject(objJson);

      using (WebClient client = new WebClient())
      {
        var URL = this.Configuration.GetSection("Api")["NotesServiceAPI"];
        client.BaseAddress = URL + "GetNoteslList/";
        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        client.Headers[HttpRequestHeader.Accept] = "application/json";
        client.Encoding = System.Text.Encoding.UTF8;
        result = client.UploadString(client.BaseAddress, "POST", objJson.ToString());
        var response = JsonConvert.DeserializeObject(result);
        if (response != null)
        {
          objResp = JsonConvert.DeserializeObject<List<NotesResponse>>(response.ToString());
        }

      }
      if (objResp.Count() > 0)
        return objResp.ToList();
      else
        return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }

  }
  public async Task<List<Core.ApplicantAggregate.NoteCategoriesData>> GetNoteCategories(int TenantId)
  {
    try
    {
      if (intelliStaffContext != null)
      {
        return await (from a in intelliStaffContext.NoteCategoriesMaster.AsNoTracking()
                      select new Core.ApplicantAggregate.NoteCategoriesData
                      {
                        master_category_id = a.MasterCategoryId,
                        master_category_type = a.MasterCategoryType,
                        is_active = a.IsActive,
                        category = a.category,
                        note_categories_chaild = (from a1 in intelliStaffContext.NoteCategoriesChild
                                                  where a1.MasterCategoryId == a.MasterCategoryId
                                                  select new Core.ApplicantAggregate.NoteCategoriesChild
                                                  {
                                                    ChildCategoryId = a1.ChildCategoryId,
                                                    category = a1.category,
                                                    MasterCategoryId = a1.MasterCategoryId,
                                                    BasePointValue = a1.BasePointValue,
                                                    description = a1.description
                                                  }).ToList()
                      }).ToListAsync();
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }

  }
  public async  Task<IEnumerable<NotesResponse>> GetNoteslList(ApplicantNotesRequest objReq)
  {
    List<Core.ApplicantAggregate.NotesResponse> objResp = new List<NotesResponse>();
    try
    {
      string result = "";
      StringBuilder objJson = new StringBuilder();
      objJson.Append("{");
      objJson.Append("\"TenantId\": \"" + objReq.TenantId + "\",");
      objJson.Append("\"EntityTypeId\": \"" + objReq.EntityTypeId + "\",");
      objJson.Append("\"ApplicantId\": \"" + objReq.ApplicantId + "\",");
      objJson.Append("\"NotesId\": \"" + objReq.EntityId + "\",");
      objJson.Append("\"CreatedBy\": \"" + objReq.CreatedBy + "\"");
      objJson.Append("}");
      var body = JsonConvert.SerializeObject(objJson);
      using (WebClient client = new WebClient())
      {
        var URL = this.Configuration.GetSection("Api")["NotesServiceAPI"];
        client.BaseAddress = URL + "GetNoteslList/";
        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        client.Headers[HttpRequestHeader.Accept] = "application/json";
        client.Encoding = System.Text.Encoding.UTF8;
        result = client.UploadString(client.BaseAddress, "POST", objJson.ToString());
        var response = JsonConvert.DeserializeObject(result);
        if (response != null)
        {
          objResp = JsonConvert.DeserializeObject<List<NotesResponse>>(response.ToString());
        }
      }
      return   objResp.ToList();
    }
    catch (Exception ex)
    {
      return null;
    }
  }
  public Task<List<Core.ApplicantAggregate.Campaign>> GetCampaignList(int TenantId)
  {
    return await (from a in intelliStaffContext.Campaign.AsNoTracking()
                  where a.TenantId == TenantId && a.IsActive == 1
                  select new Core.ApplicantAggregate.Campaign
                  {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    
                  }).ToListAsync();

  }
  public async Task<int> CreateCandidateProfile(CandInput objInput)
  {
    int candidateId = 0;
    using(var transaction = intelliStaffContext.Database.BeginTransaction())
    {
      candidateId = await CreateCandidate(objInput, candidateId, transaction);
      await transaction.CommitAsync();
      return candidateId;
    }
  }
  public async Task<bool> SendCampaignToCandidate(SendCampaignRequest objInput)
  {
    bool isSuccess = false;
    using (var transaction = intelliStaffContext.Database.BeginTransaction())
    {
      Guid newCampId = Guid.NewGuid();
      int CID = 0;
      try
      {
        Applicant objApplicant = intelliStaffContext.Applicant.Single(p => p.Id == objInput.ApplicantId);
        if (objApplicant != null)
          CID = objApplicant.CandidateId.Value;
        if (objInput.CandidateId == 0)
        {
          var obj = new CanProfGen
          {
            Divid = objInput.DivID,
            Address = objInput.Address,
            City = objInput.City,
            EMail = objInput.EMail,
            Dateenter = DateTime.Now,
            HomePhone = objInput.HomePhone,
            FirstName = objInput.FirstName,
            LastName = objInput.LastName,
            State = objInput.State,
            TenantId = objInput.TenantId,
            office = 1,
            CreatedBy = objInput.UserId,
            CreatedOn = DateTime.Now,
            SSN = "000-000-0000",
            CandID = intelliStaffContext.CanProfGen.Max(p => p.CandID) + 1,

          };
          intelliStaffContext.CanProfGen.Add(obj);
          await intelliStaffContext.SaveChangesAsync();
          CID = obj.CandID;
          //need to update candidate id in applicant table
          var ApplicantObj = (from p in intelliStaffContext.Applicant where p.Email == objInput.EMail select p);
          foreach (var app in ApplicantObj)
          {
            app.CandidateId = CID;
            intelliStaffContext.Applicant.Update(app);
          }
          await intelliStaffContext.SaveChangesAsync();
        }
        if (objApplicant.OldApplicantId == 0)
        {
          var appQueue = new ApplicantQueue
          {

            Div_ID = objApplicant.DivisionId,
            First_Name = objInput.FirstName,
            Last_Name = objInput.LastName,
            E_Mail = objInput.EMail,
            Address = objInput.Address,
            City = objInput.City,
            State = objInput.State,
            CreatedOn = DateTime.Now,
            CFHC = false,
            NewApplicantId = objInput.ApplicantId,
            TenantId = objInput.TenantId,
            PhaseComplete = "",
            agent_i9_verification_req = false,
            cand_id = CID,
          };
          intelliStaffContext.ApplicantQueue.Add(appQueue);
          await intelliStaffContext.SaveChangesAsync();

          objApplicant.OldApplicantId = appQueue.Applicant_ID;
          intelliStaffContext.Applicant.Update(objApplicant);
          await intelliStaffContext.SaveChangesAsync();
        }
        else
        {
          try
          {

            if (objApplicant.OldApplicantId > 0)
            {
              var objAppQueue = (from p in intelliStaffContext.ApplicantQueue.AsNoTracking().Where(o=>o.Applicant_ID == objApplicant.OldApplicantId) select p);
              foreach (var app in objAppQueue)
              {
                app.cand_id = CID;
                intelliStaffContext.ApplicantQueue.Update(app);
              }
              await intelliStaffContext.SaveChangesAsync();
            }
          }
          catch (Exception ex)
          {
            _logger.Log(ex);
          }
        }

        if (objInput.objStage != null)
        {
          //case 1 
          var temp1 = (from a in intelliStaffContext.RecuritmentStageDetails
                       where a.RecruitingStageTypeId == objInput.objStage.RecruitingStageTypeId && a.ApplicantId == objInput.objStage.ApplicantId && a.CandidateId == (Convert.ToInt32(objInput.objStage.CandidateId) > 0 ? objInput.objStage.CandidateId : a.CandidateId)
                       select a).Count();
          var subStageIds = (from s in intelliStaffContext.RecruitingSubStageType
                             where s.Id == objInput.objStage.RecruitingSubStageTypeId
                             select (s.IsRecruitingStageCompleted == 0 ? false : true)).ToList();
          bool StageFlag = false;
          if (subStageIds.Count() > 0)
            StageFlag = Convert.ToBoolean(subStageIds[0]);
          if (temp1 == 0)
          {
            int SID = 0;
            var stageDetails = new RecuritmentStageDetail
            {
              RecruitingStageTypeId = objInput.objStage.RecruitingStageTypeId,
              ApplicantId = objInput.ApplicantId,
              TenantId=objInput.TenantId,
              CandidateId = objInput.CandidateId,
              Comments = objInput.objStage.Comments,
              CreatedBy = objInput.objStage.UserId,
              IsRecruitingStageCompleted = StageFlag,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecuritmentStageDetail.Add(stageDetails);
            await intelliStaffContext.SaveChangesAsync();
            SID = stageDetails.Id;
            var stageSubDetails = new RecuritmentSubStageDetail
            {
              RecruitingStageId = SID,
              EntityTypeId = objInput.EntityTypeId,
              EntityId = objInput.EntityId,
              RecruitingSubStageId = objInput.objStage.RecruitingSubStageTypeId,
              Comments = objInput.objStage.Comments,
              CreatedBy = objInput.objStage.UserId,
              IsRecruitingStageCompleted = StageFlag,
              RecordStatus = 1,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
            };
            intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
            await intelliStaffContext.SaveChangesAsync();


          }
          else
          {
            var objStage = (from a in intelliStaffContext.RecuritmentStageDetail
                            where a.RecruitingStageTypeId.Equals(objInput.objStage.RecruitingStageTypeId && a.ApplicantId == objInput.objStage.ApplicantId)
                            select a).Take(1);

            int SID = 0;
            foreach (var stageObj in objStage)
            {
              stageObj.IsRecruitingStageCompleted = StageFlag;
              SID = stageObj.Id;
            }
            //update the recruitingstage table
            var stageSubDetails = new RecuritmentSubStageDetail
            {
              RecruitingStageId = SID,
              EntityTypeId = objInput.objStage.EntityTypeId,
              EntityId = objInput.objStage.EntityId,
              RecruitingSubStageId = objInput.objStage.RecruitingSubStageTypeId,
              Comments = objInput.objStage.Comments,
              CreatedBy = objInput.objStage.UserId,
              IsRecruitingStageCompleted = StageFlag,
              CreatedOn = Convert.ToDateTime(System.DateTime.Now),
              RecordStatus = 1,
            };
            intelliStaffContext.RecruitingSubStageDetail.Add(stageSubDetails);
            await intelliStaffContext.SaveChangesAsync();
          }
         
          var objInv = new CampaignInvitations
          {
            CampaignInvitationGuid = newCampId,
            CampaignId = objInput.CampaignId,
            ApplicantId = objApplicant.OldApplicantId.Value,
            DivId = objInput.DivID,
            FirstName = objInput.FirstName,
            LastName = objInput.LastName,
            email = objInput.EMail,
            source = objInput.Source,
            CampaignInvitationSentby = objInput.UserId,
            CreatedDate = DateTime.Now,
            IsAutoInvite=0,
            
          };
          intelliStaffContext.Campaign_Invitations.Add(objInv);
          await intelliStaffContext.SaveChangesAsync();


        }
        if (objInput.CandidateId > 0)
        {
          objInput.objStage.CandidateId = objInput.CandidateId;
          objInput.objStage.CreatedBy = objInput.UserId;
          objInput.objStage.TenantId = objInput.TenantId;
          objInput.objStage.EntityTypeId = 2;
          objInput.objStage.EntityId = objInput.CandidateId;
        }
        if (!string.IsNullOrEmpty(objInput.NotesText))
        {
          if (CID == 0)
          {
            objInput.EntityId = objInput.ApplicantId;
            objInput.EntityTypeId = 1;
          }
          else
          {
            objInput.EntityId = CID;
            objInput.EntityTypeId = 2;
          }
          AddNoteByStage(objInput.objStage);
        }
        transaction.Commit();

        using (var context = intelliStaffContext)
        {
          string ConnectionString = context.Database.GetDbConnection().ConnectionString;
          SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(ConnectionString);
          builder.ConnectTimeout = 2500;
          SqlConnection con = new SqlConnection(builder.ConnectionString);
          System.Data.Common.DbDataReader sqlReader;
          con.Open();
          using (SqlCommand cmd = con.CreateCommand())
          {
            cmd.CommandText = "uspInsert_CampaignEmailQueue";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandTimeout = 5000;
            cmd.Parameters.Add("@guid", System.Data.SqlDbType.UniqueIdentifier).Value = newCampId;
            cmd.Parameters.Add("@UserId", System.Data.SqlDbType.Int).Value = objInput.UserId;
            cmd.ExecuteNonQuery();

          }
          con.Close();
        }
        isSuccess = true;
        return isSuccess;
      }
      catch (Exception ex)
      {
        _logger.Log(ex);
        transaction.Rollback();
        return isSuccess;
      }
    }
  }
  public async Task<IEnumerable<Core.ApplicantAggregate.JobDetailsResult>> GetApplicantJobDetails(int JobPostId)
  {

    try
    {
      
      if (intelliStaffContext != null)
      {


        return await (from a in intelliStaffContext.JobPostDetails.AsNoTracking()
                      from b in intelliStaffContext.JobPostCategoryMapping.AsNoTracking()
                      .Where(r1 => r1.JobPostId == a.JobPostId).DefaultIfEmpty()
                      from jcm in intelliStaffContext.JobPostDescription
                      .Where(r10 => r10.JobPostId == a.JobPostId).DefaultIfEmpty()
                      from c in intelliStaffContext.Divisions
                      .Where(r2 => r2.Div_ID == a.DivisionId).DefaultIfEmpty()
                      from js in intelliStaffContext.JobPost
                      from ss in intelliStaffContext.SkillSet
                      .Where(rs3 => rs3.Id == js.SkillId).DefaultIfEmpty()
                      from jc in intelliStaffContext.JobCategory
                      .Where(r3 => r3.Id == b.CategoryId).DefaultIfEmpty()
                      from jsc in intelliStaffContext.JobSubCategory
                       .Where(r4 => r4.Id == b.SubCategoryId).DefaultIfEmpty()
                      from jl in intelliStaffContext.JobLevel
                      .Where(r5 => r5.Id == a.JobLevelId).DefaultIfEmpty()
                      from jel in intelliStaffContext.EducationLevel
                       .Where(r6 => r6.Id == a.EducationlevelId).DefaultIfEmpty()
                      from loc in intelliStaffContext.Locations
                       .Where(r6 => r6.Location_Id == a.JobSearchLocationId).DefaultIfEmpty()                       
                      from un in intelliStaffContext.User_Names
                        .Where(r8 => r8.UserID == a.CreatedBy).DefaultIfEmpty()
                      where a.JobPostId == (JobPostId == 0 ? a.JobPostId : JobPostId)

                      select new Core.ApplicantAggregate.JobDetailsResult
                      {                       
                        Email = un.Email,
                        Division = c.Division,                      
                        Phone = un.Phone,
                        JObLevel = jl.JobLevel,
                        JObSubCategory = jsc.SubCategory,
                        JobCategory = jc.Category,
                        Responsibilities = jcm.Responsibility,
                        Description = jcm.Description,
                        OrderId = b.JobPostId,
                        EducationLevel = jel.EducationLevel,
                        JobLocation = loc.Location_Name,                      
                        JobTitle = a.JobTitle,
                        EnteredBy = un.Name,
                        SkillSet = ss.SkillName,


                      }).ToListAsync();
      }

      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }
  }
  public async Task<List<Core.ApplicantAggregate.EmailTemplates>> GetEmailTemplates(int TenantId)
  {
    try
    {
      if (intelliStaffContext != null)
      {
        return await (from d in intelliStaffContext.EmailTemplates
                      .Where(r => r.TenantId == TenantId)
                      select new Core.ApplicantAggregate.EmailTemplates
                      {
                        ID = d.ID,
                        EmailBody = d.EmailBody,
                        EmailSubject = d.EmailSubject,
                        EmailTemplateTypeId = d.EmailTemplateTypeId.Value,
                        EntityId = d.EntityId.Value,
                        Status = d.Status.Value,
                        UserId = d.UserId.Value,
                        TenantId = d.TenantId.Value,
                        TemplateName = d.TemplateName,
                      }).ToListAsync();
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }

  }
  public async Task SubmitOrder(CandInput objInput)
  {
    string result = "Failed";
    using (var transaction = intelliStaffContext.Database.BeginTransaction())
    {
      try
      {
        if (intelliStaffContext != null)
        {
          var temp = (from a in intelliStaffContext.Order_Gen
                      where a.OrderID == objInput.OrderId
                      select a).Count();
          if (temp == 0)
          {
            (from a in intelliStaffContext.Order_Gen_His
             where a.OrderID == objInput.OrderId
             select a).Count();
          }
          if (temp > 0)
          {
            int CID = objInput.Candid;
            if (objInput.Candid == 0)
            {
              CID=await CreateCandidate(objInput, CID, transaction);
            }
            var order = new OrdCallResults
            {
              CandId = objInput.Candid,
              OrderId = objInput.OrderId,
              comments = "Others",
            };
            intelliStaffContext.Add(order);
            await intelliStaffContext.SaveChangesAsync();
            if(order.Id > 0)
            {

            }
            result = "Success";
            await Task.FromResult(result);
            await transaction.CommitAsync();
          }
        }
        await Task.FromResult(result);
      }
      catch (Exception ex)
      {
        _logger.Log(ex);
        await Task.FromResult(result);
      }
    }
    
  }
  public async Task<int> SendEmailsAsync(Core.ApplicantAggregate.CandidateEmailList objEmails)
  {
    int result = 0;
    _logger.Log("Inside the SendEmailsAsync, sending email to " + objEmails.ToMailId);
    var server = _emailConfig.SmtpServer;
    var port = _emailConfig.Port;
    var fromEmail = _emailConfig.From;
    if (server == "none")
      return result;
    var mailServer = new SmtpClient(server, Convert.ToInt32(port));
    var msg = new MailMessage();
    msg.From = new MailAddress(fromEmail);
    msg.IsBodyHtml = true;
    msg.Subject = objEmails.EmailSubject;
    msg.Body = objEmails.EmailBody;
    if (!Convert.ToBoolean(_emailConfig.UseDefaultCredentials))
      mailServer.UseDefaultCredentials = true;
    else
    {
      var username = _emailConfig.UserName;
      var password = _emailConfig.Password;
      mailServer.Credentials = new System.Net.NetworkCredential(username, password);
    }
    string? bcc = objEmails.EmailBCC;
    if (!string.IsNullOrEmpty(bcc))
    {
      var seps = new char[] { ',', ';' };
      foreach (var ss in bcc.Split(seps))
      {
        if (ss.Length != 0)
        {
          msg.Bcc.Add(ss);
        }
      }
    }
    string? cc = objEmails.EmailCC;
    if (!string.IsNullOrEmpty(cc))
    {
      var seps = new char[] { ',', ';' };
      foreach (var ss in cc.Split(seps))
      {
        if (ss.Length != 0)
        {
          msg.CC.Add(ss);

        }
      }
    }
    if (!string.IsNullOrEmpty(objEmails.EmailBCC))
    {
      var seps = new char[] { ',', ';' };
      foreach (var ss in objEmails.EmailBCC.Split(seps))
      {
        if (ss.Length != 0)
        {

          msg.Bcc.Add(ss);

        }
      }
    }
    var toadd = objEmails.ToMailId;
    if (!string.IsNullOrEmpty(objEmails.ToMailId))
    {
      var seps = new char[] { ',', ';' };
      foreach (var ss in objEmails.ToMailId.Split(seps))
      {
        if (ss.Length != 0)
        {
          toadd = ss;
          msg.To.Add(toadd);
          toadd = string.Empty;
        }
        var disableemail = false;
        if (!disableemail)
        {
          try
          {
            mailServer.Send(msg);
          }
          catch (Exception ex)
          {
            _logger.Log(ex);
          }

        }
      }
    }
    result = 1;
    _logger.Log($"Sent email to {objEmails.ToMailId} from {objEmails.EmailFrom} with subject {objEmails.EmailSubject}.");
    return result;
  }
  public async Task CreateEmailTemplateAsync(IntelliStaff.Core.ApplicantAggregate.EmailTemplates ObjEmail)
  {
    int TempId = 0;
    try
    {
      
      if (ObjEmail != null && intelliStaffContext != null)
      {
        //case 1 
        var temp1 = (from a in intelliStaffContext.EmailTemplates
                     where a.TemplateName == ObjEmail.TemplateName
                     select a).Count();

        if (temp1 == 0)
        {
          var obj = new EmailTemplates
          {
            EmailSubject = ObjEmail.EmailSubject,
            EmailTemplateTypeId = ObjEmail.EmailTemplateTypeId,
            EmailBody = ObjEmail.EmailBody,
            TemplateName = ObjEmail.TemplateName,
            Status = true,
            UserId = ObjEmail.UserId,
            TenantId = ObjEmail.TenantId,
            EntityId = ObjEmail.EntityId,
          };
          intelliStaffContext.EmailTemplates.Add(obj);
          await intelliStaffContext.SaveChangesAsync();
          TempId = 1;
        }
      } 
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      TempId = -1;
    }
    await Task.FromResult(TempId);
  }
  public async Task<IEnumerable<JobPostingApplicantDetails>> GetJobPostApplicantAdvanceSerachDetails(ApplicantAdvanceSearchRequest objRequest)
  {
    Guid sessionId = Guid.NewGuid();
    try
    {
      if (intelliStaffContext != null)
      {
        intelliStaffContext.Database.SetCommandTimeout(60000);

        if (objRequest.JobPostId != null && objRequest.JobPostId != 0)
        {
          objRequest.JobStartDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddYears(-5);
          objRequest.JobEndDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
        }

        if (String.IsNullOrEmpty(objRequest.JobStartDate.ToString()) && String.IsNullOrEmpty(objRequest.JobEndDate.ToString()))
        {
          if (objRequest.JobPostId != null && objRequest.JobPostId != 0)
          {
            objRequest.JobStartDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddYears(-5);
            objRequest.JobEndDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
          }
          else
          {
            objRequest.JobStartDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString()).AddMonths(-1).AddDays(1);
            objRequest.JobEndDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
          }
        }
        else
        {

          DateTime stdate = Convert.ToDateTime(objRequest.JobStartDate);
          DateTime endate = Convert.ToDateTime(objRequest.JobEndDate);

          objRequest.JobStartDate = new DateTime(stdate.Year, stdate.Month, stdate.Day, 0, 0, 0, 0);
          objRequest.JobEndDate = new DateTime(endate.Year, endate.Month, endate.Day, 23, 59, 59, 999);
        }
        List<int> ids = null;
        List<int> objStages = null;
        List<int> objStages1 = null;
        List<int> objStagesToggle = null;
        List<int> objjoppost = null;
        List<int> objJobPostId = null;
        List<int> Recuids = null;
        List<int> objRecu = null;
        List<int> objRecu1 = null;
        List<int> objwork = null;
        List<int> objCampaign = null;
        int ischeckwherecondition = 0;
        int TotalRows = 0;
        List<int> filterAppIds = null;

        //objStages = await intelliStaffContext.RecruitingStageType.Select(x => x.RecruitingStageTypeId).ToListAsync();


        //get job titles applicant id
        if (!string.IsNullOrEmpty(objRequest.JobPost) && objRequest.JobPost != "0")
        {
          objjoppost = (from a in intelliStaffContext.Applicant.AsNoTracking()
                        from b in intelliStaffContext.JobPostDetails.AsNoTracking()
                        .Where(o => o.JobPostId == a.JobPostId && o.IsActive==1)
                        where b.JobTitle.Contains(objRequest.JobPost)
                        select a.Id
                     ).Distinct().ToList();
          
          if (objjoppost == null)
          {
            return null;
          }
          else
          {
            if (objjoppost.Count() == 0)
            {
              objjoppost = null;
              return null;
            }
          }

        }

        //get jobs by jobpostid
        if (objRequest.JobPostId != null && objRequest.JobPostId != 0)
        {
          objJobPostId = (from a in intelliStaffContext.Applicant.AsNoTracking()
                          from b in intelliStaffContext.JobPostDetails.AsNoTracking()
                          .Where(o => o.JobPostId == a.JobPostId && o.IsActive == 1)
                          where b.JobPostId == objRequest.JobPostId
                          select a.Id
                     ).Distinct().ToList();

          if (objJobPostId == null)
          {
            return null;
          }
          else
          {
            if (objJobPostId.Count() == 0)
            {
              objJobPostId = null;
              return null;
            }
          }
        }

        //get recuiter applicant id
        if (!string.IsNullOrEmpty(objRequest.Recruiter) && objRequest.Recruiter != "0")
        {
          Recuids = objRequest.Recruiter?.Split(',')?.Select(int.Parse).ToList();
          objRecu = (from s11 in Recuids
                     select s11).ToList();
          objRecu1 = (from a in intelliStaffContext.Applicant
                      where objRecu.Contains(a.CreatedBy.Value)
                      select a.Id
                      ).Distinct().ToList();


          if (objRecu1 == null)
          {
            return null;
          }
          else
          {
            if (objRecu1.Count() == 0)
            {
              objRecu1 = null;
              return null;
            }
          }
        }
        


        //get working list status applicant id
        if (objRequest.WorkingList != null && objRequest.WorkingList != 0)
        {

          objwork = (from a in intelliStaffContext.Applicant.AsNoTracking()
                     from b in intelliStaffContext.workinglistitem.AsNoTracking()
                       .Where(o => o.CandidateID == a.CandidateId)
                     from c in intelliStaffContext.WorkingListNew.AsNoTracking()
                     .Where(j => j.WorkingListId == b.WorkingListId)
                     where b.CandidateID == a.CandidateId && b.WorkingListId == objRequest.WorkingList
                     select a.Id
                      ).Distinct().ToList();

          if (objwork == null)
          {
            return null;
          }
          else
          {
            if (objwork.Count() == 0)
            {
              objwork = null;
              return null;
            }
          }

        }


        //get Campaign list applicant id
        if (objRequest.CampaignStatus != null && objRequest.CampaignStatus != 0)
        {

          objCampaign = (from a in intelliStaffContext.Campaign_Invitations.AsNoTracking()
                         from b in intelliStaffContext.Campaign.AsNoTracking()
                       .Where(o => o.Id == a.CampaignId)
                         from c in intelliStaffContext.Applicant.AsNoTracking()
                         .Where(j => j.Id == a.ApplicantId)
                         where a.ApplicantId == c.Id && a.CampaignId == objRequest.CampaignStatus
                         select c.Id
                      ).Distinct().ToList();
          if (objCampaign == null)
          {
            return null;
          }
          else
          {
            if (objCampaign.Count() == 0)
            {
              objCampaign = null;
              return null;
            }
          }

        }

        //get application status applicant id
        if (!string.IsNullOrEmpty(objRequest.ApplicantStatus))
        {

          IQueryable<JobPostingApplicantDetails> baseQuery = (from a in intelliStaffContext.Applicant.AsNoTracking()
                                         from ass in intelliStaffContext.ApplicantSubSource.AsNoTracking()
                                            .Where(ass => ass.Id == a.SubSourceID)
                                         from k in intelliStaffContext.JobPostDetails.AsNoTracking()
                                             .Where(k1 => k1.JobPostId == a.JobPostId).DefaultIfEmpty()
                                         from d in intelliStaffContext.Divisions.AsNoTracking()
                                             .Where(d => d.Div_ID == a.DivisionId)
                                         from j in intelliStaffContext.JobBoardDetails.AsNoTracking()
                                             .Where(j => j.JobBoardId == ass.ApplicantSourceId).DefaultIfEmpty()
                                         from wli in intelliStaffContext.workinglistitem.AsNoTracking()
                                            .Where(wl1 => wl1.CandidateID == a.CandidateId).DefaultIfEmpty()
                                         from cpg in intelliStaffContext.CanProfGen.AsNoTracking()
                                            .Where(cpg => a.CandidateId != 0 && cpg.CandID == a.CandidateId).DefaultIfEmpty()
                                         where a.DivisionId == (Convert.ToInt32(objRequest.DivisionId) == 0 ? a.DivisionId : Convert.ToInt32(objRequest.DivisionId))
                                          && a.SubSourceID == (Convert.ToInt32(objRequest.JobBoardId) == 0 ? a.SubSourceID : Convert.ToInt32(objRequest.JobBoardId))
                                          && a.FirstName.StartsWith((string.IsNullOrEmpty(objRequest.ApplicantFirstName) ? a.FirstName : objRequest.ApplicantFirstName))
                                          && a.LastName.StartsWith((string.IsNullOrEmpty(objRequest.ApplicantLastName) ? a.LastName : objRequest.ApplicantLastName))
                                          && a.Email.StartsWith((string.IsNullOrEmpty(objRequest.ApplicantEmail) ? a.Email : objRequest.ApplicantEmail))
                                          && (a.SubmitDateTime >= Convert.ToDateTime(objRequest.JobStartDate) && a.SubmitDateTime <= Convert.ToDateTime(objRequest.JobEndDate))
                                          && a.RecordStatus == (Convert.ToInt32(objRequest.ApplicantType))
                                          && ((objRecu1 != null) ? objRecu1.Contains(a.Id) : (a.Id == a.Id))
                                          && ((objwork != null) ? objwork.Contains(a.Id) : (a.Id == a.Id))
                                          && ((objCampaign != null) ? objCampaign.Contains(a.Id) : (a.Id == a.Id))
                                          && ((objjoppost != null) ? objjoppost.Contains(a.Id) : (a.Id == a.Id))
                                          && ((objJobPostId != null) ? objJobPostId.Contains(a.Id) : (a.Id == a.Id))
                                          && cpg.StatusType != 4 // HARDCODED FOR HOTFIX, NEED TO CHANGE INTO DYNAMIC AS PART OF SPRINT 12
                                          select new Core.ApplicantAggregate.JobPostingApplicantDetails
                                          {
                                            Id = a.Id,
                                            FirstName = a.FirstName,
                                            SubmitDate = a.SubmitDateTime ?? DateTime.MinValue
                                          });


          if (objRequest.isToggle)
          {
            baseQuery = baseQuery.Where(x => !intelliStaffContext.ApplicantSearchMeta.Where(y =>y.ReferenceKey == sessionId.ToString()).Select(c => c.ApplicantId).Contains(x.Id));
          }
          else
          {
            baseQuery = baseQuery.Where(x => intelliStaffContext.ApplicantSearchMeta.Where(y => y.ReferenceKey == sessionId.ToString()).Select(c => c.ApplicantId).Contains(x.Id));
          }

          ids = objRequest.ApplicantStatus?.Split(',')?.Select(int.Parse).ToList();
          objStages = (from s11 in ids
                       select s11).ToList();

          await intelliStaffContext.Database.ExecuteSqlRawAsync($"EXEC GenerateSearchMeta '{sessionId.ToString()}' , '{string.Join(",", objStages)}'");


          TotalRows = await baseQuery.CountAsync();

          baseQuery = SortByDateandName(objRequest, baseQuery);

          List<JobPostingApplicantDetails> applicantList = await baseQuery.Skip((objRequest.skip - 1) * objRequest.LimitRows).Take(objRequest.LimitRows).ToListAsync();
          filterAppIds = applicantList.Select(x => x.Id).ToList();
        }

        else
        {
          IQueryable<JobPostingApplicantDetails> baseQuery = (from a in intelliStaffContext.Applicant.AsNoTracking()
                                                              from ass in intelliStaffContext.ApplicantSubSource.AsNoTracking()
                                                                 .Where(ass => ass.Id == a.SubSourceID)
                                                              from k in intelliStaffContext.JobPostDetails.AsNoTracking()
                                                                  .Where(k1 => k1.JobPostId == a.JobPostId).DefaultIfEmpty()
                                                              from d in intelliStaffContext.Divisions.AsNoTracking()
                                                                  .Where(d => d.Div_ID == a.DivisionId)
                                                              from j in intelliStaffContext.JobBoardDetails.AsNoTracking()
                                                                  .Where(j => j.JobBoardId == ass.ApplicantSourceId).DefaultIfEmpty()
                                                              from wli in intelliStaffContext.workinglistitem.AsNoTracking()
                                                                 .Where(wl1 => wl1.CandidateID == a.CandidateId).DefaultIfEmpty()
                                                              from cpg in intelliStaffContext.CanProfGen.AsNoTracking()
                                                                 .Where(cpg => a.CandidateId != 0 && cpg.CandID == a.CandidateId).DefaultIfEmpty()
                                                              where a.DivisionId == (Convert.ToInt32(objRequest.DivisionId) == 0 ? a.DivisionId : Convert.ToInt32(objRequest.DivisionId))
                                                               && a.SubSourceID == (Convert.ToInt32(objRequest.JobBoardId) == 0 ? a.SubSourceID : Convert.ToInt32(objRequest.JobBoardId))
                                                               && a.FirstName.StartsWith((string.IsNullOrEmpty(objRequest.ApplicantFirstName) ? a.FirstName : objRequest.ApplicantFirstName))
                                                               && a.LastName.StartsWith((string.IsNullOrEmpty(objRequest.ApplicantLastName) ? a.LastName : objRequest.ApplicantLastName))
                                                               && a.Email.StartsWith((string.IsNullOrEmpty(objRequest.ApplicantEmail) ? a.Email : objRequest.ApplicantEmail))
                                                               && (a.SubmitDateTime >= Convert.ToDateTime(objRequest.JobStartDate) && a.SubmitDateTime <= Convert.ToDateTime(objRequest.JobEndDate))
                                                               && a.RecordStatus == (Convert.ToInt32(objRequest.ApplicantType))
                                                               && ((objRecu1 != null) ? objRecu1.Contains(a.Id) : (a.Id == a.Id))
                                                               && ((objwork != null) ? objwork.Contains(a.Id) : (a.Id == a.Id))
                                                               && ((objCampaign != null) ? objCampaign.Contains(a.Id) : (a.Id == a.Id))
                                                               && ((objjoppost != null) ? objjoppost.Contains(a.Id) : (a.Id == a.Id))
                                                               && ((objJobPostId != null) ? objJobPostId.Contains(a.Id) : (a.Id == a.Id))
                                                               && cpg.StatusType != 4 // HARDCODED FOR HOTFIX, NEED TO CHANGE INTO DYNAMIC AS PART OF SPRINT 12
                                                              select new Core.ApplicantAggregate.JobPostingApplicantDetails
                                                              {
                                                                Id = a.Id,
                                                                FirstName = a.FirstName,
                                                                SubmitDate = a.SubmitDateTime ?? DateTime.MinValue
                                                              });

          TotalRows = baseQuery.Count();

          baseQuery = SortByDateandName(objRequest, baseQuery);

          List<JobPostingApplicantDetails> applicantList = await baseQuery.Skip((objRequest.skip - 1) * objRequest.LimitRows).Take(objRequest.LimitRows).ToListAsync();
          filterAppIds = applicantList.Select(x => x.Id).ToList();
        }

        var obj = (from a in intelliStaffContext.Applicant.AsNoTracking()
                   from ass in intelliStaffContext.ApplicantSubSource.AsNoTracking()
                      .Where(ass => ass.Id == a.SubSourceID)
                   from k in intelliStaffContext.JobPostDetails.AsNoTracking()
                       .Where(k1 => k1.JobPostId == a.JobPostId).DefaultIfEmpty()
                   from d in intelliStaffContext.Divisions.AsNoTracking()
                       .Where(d => d.Div_ID == a.DivisionId)
                   from j in intelliStaffContext.JobBoardDetails.AsNoTracking()
                       .Where(j => j.JobBoardId == ass.ApplicantSourceId).DefaultIfEmpty()
                   from wli in intelliStaffContext.workinglistitem.AsNoTracking()
                      .Where(wl1 => wl1.CandidateID == a.CandidateId).DefaultIfEmpty()
                   from cpg in intelliStaffContext.CanProfGen.AsNoTracking()
                      .Where(cpg => a.CandidateId != 0 && cpg.CandID == a.CandidateId).DefaultIfEmpty()
                   where 
                       filterAppIds.Contains(a.Id)
                   orderby a.SubmitDateTime descending

                   select new JobPostingApplicantDetails
                   {
                     Id = a.Id,
                     FirstName = a.FirstName,
                     LastName = a.LastName,
                     DivisionId = a.DivisionId.Value,
                     DivisionName = d.Division,
                     ApplicantName = a.FirstName + " " + a.LastName,
                     ApplicantEmail = a.Email,
                     ApplicantJobTitle = k.JobTitle,
                     ApplicantCity = a.City,
                     ApplicantState = a.State,
                     ApplicantZip = a.ZipCode,
                     ApplicantAddress = (a.Address ?? $"{a.City}-{a.State}-{a.ZipCode}").Trim('-'),
                     ApplicantPhone = a.CellPhoneNumber,
                     ApplicantAltPhone = a.HomePhoneNumber,
                     ApplicantApplicantId = a.Id,
                     ApplicantCandidateId = a.CandidateId,
                     ApplicantJobPostId = a.JobPostId,
                     SubmitDate = a.SubmitDateTime.Value,
                     ApplicantResumeId = (from res in intelliStaffContext.ICUResumeQueue
                                            .Where(o => o.NewApplicantId == a.Id && a.Id != 0 && o.ResponseResult == "Success")
                                          select res.UniqueResumeId).FirstOrDefault(),

                     IsResumeAvilable = (from icq in intelliStaffContext.ICUResumeQueue.AsNoTracking()
                                         from b in intelliStaffContext.IcuResumeQueueFile.AsNoTracking()
                                         .Where(ass => ass.queue_file_id == icq.id && icq.IsProcess == 1 && icq.ApplicantId == a.OldApplicantId && a.OldApplicantId != 0)
                                         select icq.IsProcess).FirstOrDefault(),
                     AppJobs = null,

                     ApplicantJobCount = 0,
                     RecruitingParentStatus = (from a1 in intelliStaffContext.RecuritmentStageDetail.AsNoTracking()
                                               from j1 in intelliStaffContext.RecruitingStageType.AsNoTracking()
                                                .Where(j => j.RecruitingStageTypeId == a1.RecruitingStageTypeId)

                                               where a1.ApplicantId == a.Id
                                               select new Core.ApplicantAggregate.StagesArray
                                               {
                                                 RecruitingStageId = a1.RecruitingStageTypeId,
                                                 ParentStageName = j1.StageName,
                                               }
                                     ).Distinct().ToList(),
                     RecruitingSubStages = (from a1 in intelliStaffContext.RecuritmentStageDetail.AsNoTracking()
                                            from b in intelliStaffContext.RecruitingSubStageDetail.AsNoTracking()
                                            .Where(o => o.RecruitingStageId == a1.Id)
                                            from j1 in intelliStaffContext.RecruitingSubStageType.AsNoTracking()
                                             .Where(j => j.Id == b.RecruitingSubStageId)
                                            from j2 in intelliStaffContext.User_Names.AsNoTracking()
                                            .Where(o => o.UserID == a1.CreatedBy)
                                            where a1.ApplicantId == a.Id
                                            orderby b.Id descending
                                            select new SubStagesArray
                                            {
                                              RecruitingStageId = a1.RecruitingStageTypeId,
                                              RecrutingStage = j1.SubStageName,
                                              RecruitingSubStageId = b.RecruitingSubStageId,
                                              IsRecruitingStageCompleted = a1.IsRecruitingStageCompleted.Value,
                                              CreatedOn = a1.CreatedOn,
                                              UserName = j2.Name,
                                            }
                                     ).Distinct().ToList(),
                     WorkingListQueues = (from w1 in intelliStaffContext.workinglistitem.AsNoTracking()
                                          from w2 in intelliStaffContext.WorkingListNew.AsNoTracking()
                                           .Where(j1 => j1.WorkingListId == w1.WorkingListId)
                                          where w1.CandidateID == a.CandidateId
                                          orderby w1.CreatedOn descending
                                          select w2.WorkingListName
                                            ).Distinct().ToList(),
                     CampaignsList = (from w1 in intelliStaffContext.Campaign_Invitations.AsNoTracking()
                                      from w2 in intelliStaffContext.Campaign.AsNoTracking()
                                           .Where(j1 => j1.Id == w1.CampaignId)
                                      where w1.ApplicantId == a.OldApplicantId
                                      orderby w1.CreatedDate descending
                                      select w2.Name
                                            ).Distinct().ToList(),
                     LastLoginDate = (from y in intelliStaffContext.UserLoginInfo.AsEnumerable() where (y.UserId == objRequest.UserId) orderby y.Id descending select y.LoginTime).SingleOrDefault(),
                     ApplicantTotalCount = TotalRows,
                     ApplicantColorCode = String.IsNullOrEmpty(a.SubmitDateTime.Value.ToString()) ? "red" : ((DateTime.Now - a.SubmitDateTime.Value).Days == 0 ? "green" : (DateTime.Now - a.SubmitDateTime.Value).Days > 7 ? "red" : "orange"),
                     JobBoardName = ass.SubSourceName
                   });
        obj = SortByDateandName(objRequest, obj);

        await intelliStaffContext.Database.ExecuteSqlRawAsync($"DELETE FROM ApplicantSearchMeta WHERE ReferenceKey = '{sessionId.ToString()}'");        

        return await obj.ToListAsync();
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }
    finally
    {
      await intelliStaffContext.Database.ExecuteSqlRawAsync($"DELETE FROM ApplicantSearchMeta WHERE ReferenceKey = '{sessionId.ToString()}'");
    }
  }

  private IQueryable<JobPostingApplicantDetails> SortByDateandName(ApplicantAdvanceSearchRequest objRequest, IQueryable<JobPostingApplicantDetails> obj)
  {
    if (objRequest.DateSort == 0)
    {
      if (objRequest.NameSort == 2)
      {
        obj = obj.OrderByDescending(x => x.SubmitDate).ThenBy(x => x.FirstName);
      }
      else if (objRequest.NameSort == 3)
      {
        obj = obj.OrderByDescending(x => x.SubmitDate).ThenByDescending(x => x.FirstName);
      }
      else
      {
        obj = obj.OrderByDescending(x => x.SubmitDate);
      }
    }
    else if (objRequest.DateSort == 1)
    {
      if (objRequest.NameSort == 2)
      {
        obj = obj.OrderBy(x => x.SubmitDate).ThenBy(x => x.FirstName);
      }
      else if (objRequest.NameSort == 3)
      {
        obj = obj.OrderBy(x => x.SubmitDate).ThenByDescending(x => x.FirstName);
      }
      else
      {
        obj = obj.OrderBy(x => x.SubmitDate);
      }
    }
    if (objRequest.NameSort == 2)
    {
      if (objRequest.DateSort == 0)
      {
        obj = obj.OrderBy(x => x.FirstName).ThenByDescending(x => x.SubmitDate);
      }
      else if (objRequest.DateSort == 1)
      {
        obj = obj.OrderBy(x => x.FirstName).ThenBy(x => x.SubmitDate);
      }
      else
      {
        obj = obj.OrderBy(x => x.FirstName);
      }
    }
    else if (objRequest.NameSort == 3)
    {
      if (objRequest.DateSort == 0)
      {
        obj = obj.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.SubmitDate);
      }
      else if (objRequest.DateSort == 1)
      {
        obj = obj.OrderByDescending(x => x.FirstName).ThenBy(x => x.SubmitDate);
      }
      else
      {
        obj = obj.OrderByDescending(x => x.FirstName);
      }
    }

    return obj;
  }

  public async Task<IEnumerable<JobPostingApplicantDetails>> GetJobPostCandidateSerachDetails(int CandidateId)
  {
    try
    {
      if (intelliStaffContext != null)
      {
        var obj = (from a in intelliStaffContext.Applicant                   
                   from k in intelliStaffContext.JobPostDetails
                     .Where(k1 => k1.JobPostId == a.JobPostId).DefaultIfEmpty()
                   from d in intelliStaffContext.Divisions
                     .Where(d => d.Div_ID == a.DivisionId)
                   from ass in intelliStaffContext.ApplicantSubSource
                     .Where(ass => ass.Id == a.SubSourceID)
                   from j in intelliStaffContext.JobBoardDetails
                       .Where(j => j.JobBoardId == ass.ApplicantSourceId).DefaultIfEmpty()
                   from cpg in intelliStaffContext.CanProfGen.AsNoTracking()
                      .Where(cpg => a.CandidateId != 0 && cpg.CandID == a.CandidateId).DefaultIfEmpty()
                   where (a.CandidateId == CandidateId)
                   && cpg.StatusType != 4 // HARDCODED FOR HOTFIX, NEED TO CHANGE INTO DYNAMIC AS PART OF SPRINT 12
                   select new JobPostingApplicantDetails
                   {
                     Id = a.Id,
                     FirstName = a.FirstName,
                     LastName = a.LastName,
                     DivisionId = a.DivisionId.Value,
                     ApplicantName = a.FirstName + " " + a.LastName,
                     ApplicantEmail = a.Email,
                     ApplicantJobTitle = k.JobTitle,
                     ApplicantCity = a.City,
                     ApplicantState = a.State,
                     ApplicantZip = a.ZipCode,
                     ApplicantAddress = (a.Address ?? $"{a.City}-{a.State}-{a.ZipCode}").Trim('-'),
                     ApplicantPhone = a.CellPhoneNumber,
                     ApplicantAltPhone = a.HomePhoneNumber,
                     ApplicantApplicantId = a.Id,
                     ApplicantCandidateId = a.CandidateId,
                     ApplicantJobPostId = a.JobPostId,
                     SubmitDate = a.SubmitDateTime.Value,
                     DivisionName = d.Division,
                     ApplicantResumeId = (from res in intelliStaffContext.ICUResumeQueue
                                            .Where(o => o.NewApplicantId == a.Id && a.Id != 0 && o.ResponseResult == "Success")
                                          select res.UniqueResumeId).FirstOrDefault(),
                     AppJobs = (from a1 in intelliStaffContext.Applicant
                                from ass in intelliStaffContext.ApplicantSubSource
                                    .Where(ass => ass.Id == a1.SubSourceID)
                                from j in intelliStaffContext.JobBoardDetails
                                    .Where(j => j.JobBoardId == ass.ApplicantSourceId).DefaultIfEmpty()
                                from k in intelliStaffContext.JobPostDetails
                                  .Where(k1 => k1.JobPostId == a1.JobPostId && a1.CandidateId != 0).DefaultIfEmpty()
                                where a1.Email == a.Email
                                select new Core.ApplicantAggregate.ApplicantJobDetails
                                {
                                  JobTitle = k.JobTitle,
                                  JobDate = a1.SubmitDateTime.ToString()

                                }).ToList(),

                     ApplicantJobCount = (from a1 in intelliStaffContext.Applicant where a1.Email == a.Email && a1.CandidateId != 0 select a).Count(),
                     RecruitingParentStatus = (from a1 in intelliStaffContext.RecuritmentStageDetail
                                               from j1 in intelliStaffContext.RecruitingStageType
                                                .Where(j => j.RecruitingStageTypeId == a1.RecruitingStageTypeId)

                                               where a1.ApplicantId == a.Id && a1.CandidateId != 0
                                               select new Core.ApplicantAggregate.StagesArray
                                               {
                                                 RecruitingStageId = a1.RecruitingStageTypeId,
                                                 ParentStageName = j1.StageName,
                                               }
                                     ).Distinct().ToList(),
                     RecruitingSubStages = (from a1 in intelliStaffContext.RecuritmentStageDetail
                                            from b in intelliStaffContext.RecruitingSubStageDetail
                                            .Where(o => o.RecruitingStageId == a1.Id)
                                            from j1 in intelliStaffContext.RecruitingSubStageType
                                             .Where(j => j.Id == b.RecruitingSubStageId)
                                            from j2 in intelliStaffContext.User_Names
                                            .Where(o => o.UserID == a1.CreatedBy)
                                            where a1.ApplicantId == a.Id
                                            orderby b.Id descending
                                            select new SubStagesArray
                                            {
                                              RecruitingStageId = a1.RecruitingStageTypeId,
                                              RecrutingStage = j1.SubStageName,
                                              RecruitingSubStageId = b.RecruitingSubStageId,
                                              IsRecruitingStageCompleted = a1.IsRecruitingStageCompleted.Value,
                                              CreatedOn = a1.CreatedOn,
                                              UserName = j2.Name,
                                            }
                                     ).Distinct().ToList(),
                     CampaignsList = (from w1 in intelliStaffContext.Campaign_Invitations
                                      from w2 in intelliStaffContext.Campaign
                                           .Where(j1 => j1.Id == w1.CampaignId)
                                      where w1.ApplicantId == a.Id
                                      orderby w1.CreatedDate descending
                                      select w2.Name
                                          ).Distinct().ToList(),

                     ApplicantTotalCount = 0,

                     ApplicantColorCode = String.IsNullOrEmpty(a.SubmitDateTime.Value.ToString()) ? "red" : ((DateTime.Now - a.SubmitDateTime.Value).Days == 0 ? "green" : (DateTime.Now - a.SubmitDateTime.Value).Days > 7 ? "red" : "orange"),
                     JobBoardName = ass.SubSourceName
                   });

        return obj.ToList();
      }
      return null;
    }
    catch (Exception ex)
    {
      return null;
    }
  }
  public async Task<IEnumerable<ApplicanLogoDetails>> GetApplicantLogo(int ApplicantId)
  {
    try
    {
      if (intelliStaffContext != null)
      {

        return await (from a in intelliStaffContext.Applicant.AsNoTracking()
                      from b in intelliStaffContext.ApplicantSubSource.AsNoTracking()
                      .Where(j => j.Id == a.SubSourceID && a.Id == ApplicantId)
                      from c in intelliStaffContext.ApplicantLogo.AsNoTracking()
                     .Where(K => K.JobBoardId == b.ApplicantSourceId).DefaultIfEmpty()


                      select new ApplicanLogoDetails
                      {
                        LogoPath = c.LogoPath ?? "",
                        SubSourceName = b.SubSourceName

                      }).ToListAsync();
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex.Message.ToString());
      return null;
    }

  }
  public async Task<EmailMessageData> GetOnBoardingEmailData(int CandidateId, string PhaseName, string AuthKey)
  {
    try
    {
      if (intelliStaffContext != null)
      {

        if(await RegisterCheckCandidate(CandidateId, AuthKey))
        {

          #region Email logic
          EmailMessageData emailMessageData;

          using (SqlConnection conn = new SqlConnection(intelliStaffContext.Database.GetConnectionString()))
          {
            using (SqlDataAdapter sq = new SqlDataAdapter())
            {
              sq.SelectCommand = new SqlCommand($"EXEC GetInvitationEmail {CandidateId}, '{PhaseName}'", conn);

              DataSet ds = new DataSet();
              sq.Fill(ds);

              emailMessageData = new EmailMessageData
              {
                FromEmailAddress = ds.Tables[0]?.Rows[0]?["FromEmailAddress"]?.ToString(),
                ToEmailAddress = ds.Tables[0]?.Rows[0]?["ToEmailAddress"]?.ToString(),
                CCEmailAddress = ds.Tables[0]?.Rows[0]?["CCEmailAddress"]?.ToString(),
                BccEmailAddress = ds.Tables[0]?.Rows[0]?["BccEmailAddress"]?.ToString(),
                Subject = ds.Tables[0]?.Rows[0]?["Subject"]?.ToString(),
                AttachmentPath = ds.Tables[0]?.Rows[0]?["AttachmentPath"]?.ToString(),
                MessageBody = ds.Tables[0]?.Rows[0]?["MessageBody"]?.ToString(),
              };

            }
          }
          return emailMessageData;
          #endregion
        }
      }
      return null;
    }
    catch (Exception ex)
    {
      _logger.Log(ex.Message.ToString());
      return null;
    }
  }

  /// <summary>
  /// To check if a candidate is registered through IAM or not
  /// </summary>
  /// <returns></returns>
  private async Task<bool> RegisterCheckCandidate(int CandidateId, string AuthKey = "")
  {
    try
    {
      Dictionary<string, string> APIs = Configuration.GetSection("Api").GetChildren().ToDictionary(x => x.Key, x => x.Value);

      CanProfGen canProfGen = await intelliStaffContext.CanProfGen.Where(x => x.CandID == CandidateId).FirstOrDefaultAsync();

      Random r = new Random();
      int randNum = r.Next(1000000);
      string sixDigitNumber = randNum.ToString("D6");

      string json = JsonConvert.SerializeObject(new CandidateRegRequest
      {
        username = canProfGen.EMail,
        email = canProfGen.EMail,
        firstName = canProfGen.FirstName,
        lastName = canProfGen.LastName,
        requestSource = 1,
        divId = canProfGen.Divid,
        password = sixDigitNumber, // Configuration.GetConnectionString("DefaultPass") ?? sixDigitNumber,
        shouldSendEmail = true,
        isTempPassword = true,
        tenantId = 1,
        cand_ID = canProfGen.CandID
      });
      StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

      _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AuthKey.Replace("Bearer ", ""));

      HttpResponseMessage responseMessage = await _httpClient.PostAsync($"{APIs["AuthAPI"]}/api/User/register/candidate", data);

      if (responseMessage.IsSuccessStatusCode)
      {
        return true;
      }
    }
    catch(Exception ex)
    {
      _logger.Log(ex);
    }

    return false;
  }

  public async Task<int> CreateNewApplicant(ApplicantInput objApplicant)
  {
    int ApplicantId = 0;
    try
    {
      if (objApplicant != null)
      {
        int objApplicantQueue = await intelliStaffContext.ApplicantQueue.AsNoTracking().Where(e => e.E_Mail == objApplicant.Email).CountAsync();

        if (objApplicantQueue == 0)
        {
          Applicant obj = new Applicant
          {
            TenantId = objApplicant.TenantId,
            FirstName = objApplicant.FirstName,
            MiddleName = objApplicant.MiddleName,
            LastName = objApplicant.LastName,
            Email = objApplicant.Email,
            Address = objApplicant.Address,
            City = objApplicant.City,
            State = objApplicant.State,
            ZipCode = objApplicant.ZipCode,
            CellPhoneNumber = objApplicant.CellPhoneNumber,
            HomePhoneNumber = objApplicant.HomePhoneNumber,
            SubSourceID = objApplicant.SubSourceID,
            DivisionId = objApplicant.DivisionId,
            SubmitDateTime = objApplicant.SubmitDateTime,
            HasResumeFile = objApplicant.HasResumeFile,
            ReferenceId = objApplicant.ReferenceId,
            CandidateId = objApplicant.CandidateId,
            RecordStatus = objApplicant.RecordStatus,
            CreatedBy = objApplicant.CreatedBy,
            CreatedOn = DateTime.Now,
            ModifiedBy = null,
            ModifiedOn = null,
            JobPostId = objApplicant.JobPostId,
            JobPostReferenceId = objApplicant.JobPostReferenceId,
            ApplicantType = objApplicant.ApplicantType,
            OldApplicantId = 0
          };
          intelliStaffContext.Applicant.Add(obj);
          await intelliStaffContext.SaveChangesAsync();

          ApplicantId = obj.Id;

          ApplicantQueue objAppQueue = new ApplicantQueue
          {
            Div_ID = objApplicant.DivisionId,
            SubmitDateTime = objApplicant.SubmitDateTime,
            Last_Name = objApplicant.LastName,
            First_Name = objApplicant.FirstName,
            Middle = objApplicant.MiddleName,
            Address = objApplicant.Address,
            City = objApplicant.City,
            State = objApplicant.State,
            Zip = objApplicant.ZipCode,
            Home_Phone = objApplicant.HomePhoneNumber,
            E_Mail = objApplicant.Email,
            NewApplicantId = ApplicantId,
            CreatedBy = objApplicant.CreatedBy,
            CreatedOn = DateTime.Now,
            PhaseComplete = "",
            CFHC = false,
            TenantId = objApplicant.TenantId,
            agent_i9_verification_req = false
          };
          intelliStaffContext.ApplicantQueue.Add(objAppQueue);
          await intelliStaffContext.SaveChangesAsync();

          int ApplicantQueueId = objAppQueue.Applicant_ID;

          if(ApplicantQueueId > 0)
          {
            //to get newly created applicant

            var appDetail = (from a in intelliStaffContext.Applicant where a.Id == ApplicantId select a);
            foreach(var App in appDetail)
            {
              App.OldApplicantId = ApplicantQueueId;
              intelliStaffContext.Applicant.Update(App);
            }
          await intelliStaffContext.SaveChangesAsync();
          }

          if (!string.IsNullOrEmpty(objApplicant.Activity) && ApplicantQueueId > 0)
          {
            AppNotes objAppQueNotes = new AppNotes
            {
              Datein = DateTime.Now,
              Activ = objApplicant.Activity,
              Appid = ApplicantQueueId,
              Userid = objApplicant.CreatedBy,
              Source = objApplicant.SourceId,
              Category = objApplicant.Category,
              CategoryType = objApplicant.CategoryType,
            };
            intelliStaffContext.AppNotes.Add(objAppQueNotes);
            await intelliStaffContext.SaveChangesAsync();
          }

          if (!string.IsNullOrEmpty(objApplicant.ResumeFile) && ApplicantQueueId > 0)
          {
            AppResumes objAppResumes = new AppResumes
            {
              ApplicantQueueId = ApplicantQueueId,
              ResumeText = objApplicant.ResumeText,
              ResumeFile = Encoding.ASCII.GetBytes(objApplicant.ResumeFile),
              ResumeType = objApplicant.ResumeFileType,
              Filename = objApplicant.ResumeFilename,
              CreatedOn = DateTime.Now,
              ResumeFileExtension = objApplicant.ResumeFileExtension,
              ResumeFileDescription = objApplicant.ResumeFileDescription
            };
            intelliStaffContext.AppResumes.Add(objAppResumes);
            await intelliStaffContext.SaveChangesAsync();
          }

          if (!string.IsNullOrEmpty(objApplicant.Activity) && ApplicantId > 0)
          {
            ApplicantNotes objApplicantNotes = new ApplicantNotes
            {
              TenantId = objApplicant.TenantId,
              ApplicantId = ApplicantId,
              Activity = objApplicant.Activity,
              CategoryType = objApplicant.CategoryType,
              CategoryId = objApplicant.CategoryId,
              IsAutomated = objApplicant.IsAutomated,
              RecordStatus = objApplicant.RecordStatus,
              CreatedBy = objApplicant.CreatedBy,
              CreatedOn = DateTime.Now
            };
            intelliStaffContext.ApplicantNotes.Add(objApplicantNotes);
            await intelliStaffContext.SaveChangesAsync();
          }

          if ((!string.IsNullOrEmpty(objApplicant.ResumeFilePath) || !string.IsNullOrEmpty(objApplicant.ResumeFilename)) && ApplicantId > 0)
          {
            ApplicantResumes objApplicantResume = new ApplicantResumes
            {
              TenantId = objApplicant.TenantId,
              ApplicantId = ApplicantId,
              ResumeFilePath = "NA",
              ResumeFileType = objApplicant.ResumeFileType,
              ResumeFilename = objApplicant.ResumeFilename,
              ResumeFileExtension = objApplicant.ResumeFileExtension,
              ResumeFileDescription = objApplicant.ResumeFileDescription,
              RecordStatus = objApplicant.RecordStatus,
              ApplicationDate = DateTime.Now,
              CreatedBy = objApplicant.CreatedBy,
              CreatedOn = DateTime.Now
            };
            intelliStaffContext.ApplicantResumes.Add(objApplicantResume);
            await intelliStaffContext.SaveChangesAsync();
          }

          ApplicantQueueAudit objApplicantQueueAudit = new ApplicantQueueAudit
          {
            ApplicantQueueId = ApplicantQueueId,
            Email = objApplicant.Email,
            DivisionId = objApplicant.DivisionId,
            SourceId = objApplicant.SourceId,
            TypeId = objApplicant.ApplicantType,
            AddedFromApplicantionId = 1,
            CreatedOn = DateTime.Now,
            CreatedBy = objApplicant.CreatedBy
          };
          intelliStaffContext.ApplicantQueueAudit.Add(objApplicantQueueAudit);
          await intelliStaffContext.SaveChangesAsync();

          return ApplicantId;
        }
      }
      return ApplicantId;
    }

    catch (Exception ex)
    {
      _logger.Log("Create new applicant error log " + ex.Message.ToString());
      return ApplicantId;
    }
  }


}
