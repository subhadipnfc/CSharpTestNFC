using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;
using TemPositions.IntelliStaff.Core.Interfaces;
using TemPositions.IntelliStaff.Core.Response;

namespace TemPositions.IntelliStaff.Web.Api;
public class ApplicantController : BaseApiController
{
  private readonly IApplicantRepository _ApplicantRepository;
  private readonly Tempositions.Intellistaff.Shared.Logging.Core.ILogger _logger;
  public ApplicantController(IApplicantRepository repository, Tempositions.Intellistaff.Shared.Logging.Core.ILogger logger)
  {
    _ApplicantRepository = repository;
    this._logger = logger;
  }
  

  //
  [HttpPost]

  [Route("CreateCandidateProfile")]
  public async Task<IActionResult> CreateCandidateProfile([FromBody] CandInput objRequest)
  {
    try
    {
      int candId = await _ApplicantRepository.CreateCandidateProfile(objRequest);
      return Ok(candId);
    }
    catch (Exception)
    {
      return BadRequest("Failed to create Candidate");
    }

  }
  [HttpPost]

  [Route("GetApplicantFiltersByDivision")]
  public async Task<IActionResult> GetApplicantFiltersByDivision(Int32 DivisionId, int TenantId)
  {
    try
    {
      var applicantFilters = await _ApplicantRepository.GetApplicantFiltersByDivision(DivisionId, TenantId);
      if (applicantFilters == null)
      {
        return NotFound("No Applicant Filters Found");
      }

      return Ok(applicantFilters);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get Recruiting Stages");
    }

  }
  [HttpPost]

  [Route("GetRecruitingStages")]
  public async Task<IActionResult> GetRecruitingStages(Int32 TenantId)
  {
    try
    {
      var recStages = await _ApplicantRepository.GetRecruitingStages(TenantId);
      if (recStages == null)
      {
        return NotFound("No Recruiting Stages Found");
      }

      return Ok(recStages);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get Recruiting Stages");
    }

  }

  [HttpPost]
  [Route("GetApplicantDetailsByApplicantId")]
  public async Task<IActionResult> GetApplicantDetailsByApplicantId([FromBody] RecruitingStageInput Obj)
  {
    try
    {
      var appDetails = await _ApplicantRepository.GetApplicantDetailsByApplicantId(Obj);
      if (appDetails == null)
      {
        return NotFound("No Applicant Found");
      }

      return Ok(appDetails);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get applicant Data");
    }

  }



  [HttpPost]
  [Route("InsertRecruitingStageByApplicantId")]
  public async Task<IActionResult> InsertRecruitingStageByApplicantId([FromBody] RecruitingStageInput Obj)
  {
    try
    {
      var appDetails = await _ApplicantRepository.InsertRecruitingStageByApplicantId(Obj);
      if (appDetails == false)
      {
        return NotFound("No Applicant Found");
      }

      return Ok(appDetails);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get applicant Data");
    }

  }


  [HttpGet]
  [Route("GetNoteCategories")]
  public async Task<IActionResult> GetNoteCategories(Int32 TenantId)
  {
    try
    {
      var noteCategories = await _ApplicantRepository.GetNoteCategories(TenantId);
      if (noteCategories == null)
      {
        return NotFound("No Get Note Categories Found");
      }

      return Ok(noteCategories);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get Notes Categories ");
    }

  }
  //
  [HttpPost]
  [Route("AddApplicantNotes")]
  public async Task<IActionResult> AddApplicantNotes(ApplicantNotesRequest ObjReq)
  {
    try
    {
      var noteCategories = await _ApplicantRepository.AddApplicantNotes(ObjReq);
      if (noteCategories == null)
      {
        return NotFound("No Applicant Notes added");
      }

      return Ok(noteCategories);
    }
    catch (Exception)
    {
      return BadRequest("Failed to add applicant notes ");
    }

  }
  [HttpPost]
  [Route("GetNotesDetailsByApplicantId")]
  public async Task<IActionResult> GetNotesDetailsByApplicantId(ApplicantNotesRequest ObjReq)
  {
    try
    {
      var noteCategories = await _ApplicantRepository.GetNotesDetailsByApplicantId(ObjReq);
     
      return Ok(noteCategories);
    }
    catch (Exception)
    {
      return BadRequest("Failed to get applicant notes ");
    }

  }
  [HttpPost]
  [Route("InsertRecruitingStageByEvent")]
  public async Task<IActionResult> InsertRecruitingStageByEvent(RecruitingStageInput2 ObjReq)
  {
    try
    {
      var result = await _ApplicantRepository.InsertRecruitingStageByEvent(ObjReq);
      if (result == false)
      {
        return NotFound("Notes save failed");
      }

      return Ok(result);
    }
    catch (Exception)
    {
      return BadRequest("Failed to add notes ");
    }

  }

  [HttpPost]
  [Route("AddApplicantToWorkList")]
  public async Task<IActionResult> AddApplicantToWorkList(CandInput ObjReq)
  {
    try
    {
      var result = _ApplicantRepository.AddApplicantToWorkList(ObjReq);
      if (await result == false)
      {
        return NotFound("Candidate is not added to worklist");
      }

      return Ok(result);
    }
    catch (Exception)
    {
      return BadRequest("Failed to add candidate to worklist ");
    }

  }
  [HttpGet]
  [Route("GetCampaignList")]
  public async Task<IActionResult> GetCampaignList(Int32 TenantId)
  {
    try
    {
      var campaigns = await _ApplicantRepository.GetCampaignList(TenantId);
      if (campaigns == null)
      {
        return NotFound("No Campaigns Found");
      }

      return Ok(campaigns);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get Campaigns");
    }

  }
  [HttpPost]
  [Route("GetApplicantJobDetails")]
  public async Task<IActionResult> GetApplicantJobDetails(int JobPostId)
  {
    try
    {
      var queue = await _ApplicantRepository.GetApplicantJobDetails(JobPostId);     
      return Ok(queue);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get JobPost Applicant Serach Details");
    }

  }
  [HttpPost]
  [Route("SendCampaignToCandidate")]
  public async Task<IActionResult> SendCampaignToCandidate(SendCampaignRequest objReq)
  {
    try
    {
      var queue =await  _ApplicantRepository.SendCampaignToCandidate(objReq);
      return Ok(queue);
    }
    catch (Exception)
    {
      return BadRequest("Failed to send Campaign");
    }

  }
  [HttpPost]
  [Route("SubmitOrder")]
  public async Task<IActionResult> SubmitOrder(CandInput objReq)
  {
    try
    {
      var queue = _ApplicantRepository.SubmitOrder(objReq);

      return Ok(queue);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Submit Order");
    }

  }

  [HttpPost]
  [Route("CreateEmailTemplateAsync")]
  public async Task<IActionResult> CreateEmailTemplateAsync(EmailTemplates objEmail)
  {

    try
    {
      var res = _ApplicantRepository.CreateEmailTemplateAsync(objEmail);
      return Ok("Success");
    }
    catch (Exception ex)
    {
      return BadRequest("Failed :" + ex?.StackTrace?.ToString());
    }

  }

  [HttpPost]
  [Route("SendEmailsAsync")]
  public async Task<IActionResult> SendEmailsAsync(CandidateEmailList objEmail)
  {

    try
    {
      var res = await _ApplicantRepository.SendEmailsAsync(objEmail);
      return Ok("Success");
    }
    catch (Exception ex)
    {
      return BadRequest("Failed :" + ex?.StackTrace?.ToString());
    }

  }
  [HttpGet]
  [Route("GetEmailTemplates")]
  public async Task<IActionResult> GetEmailTemplates(int TenantId)
  {

    try
    {
      var emails = await _ApplicantRepository.GetEmailTemplates(TenantId);
      return Ok(emails);
    }
    catch (Exception ex)
    {
      return NotFound(new Exception(ex.Message.ToString()));
    }

  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  [HttpPost]

  [Route("GetJobPostApplicantAdvanceSerachDetails")]
  public async Task<IActionResult> GetJobPostApplicantAdvanceSerachDetails([FromBody] ApplicantAdvanceSearchRequest objRequest)
  {
    try
    {
      var applicants = await _ApplicantRepository.GetJobPostApplicantAdvanceSerachDetails(objRequest);    
      return Ok(applicants);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get JobPost Applicant Serach Details");
    }

  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  [HttpGet]

  [Route("GetCandidateSerachDetails")]
  public async Task<IActionResult> GetCandidateSerachDetails(int CandidateId)
  {
    try
    {
      var applicants = await _ApplicantRepository.GetJobPostCandidateSerachDetails(CandidateId);    
      return Ok(applicants);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get JobPost Candidate Serach Details");
    }

  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  [HttpGet]

  [Route("GetApplicantLogo")]
  public async Task<IActionResult> GetApplicantLogo(int ApplicantId)
  {
    try
    {
      var applicants = await _ApplicantRepository.GetApplicantLogo(ApplicantId);
      if (applicants == null)
      {
        return NotFound("No Logo Found");
      }

      return Ok(applicants);
    }
    catch (Exception ex)
    {
      _logger.Log(ex.Message.ToString());
      return BadRequest("Failed to Get Applicant Logo Details");
    }

  }

  /// <summary>
  /// Get Onboarding Email Data
  /// </summary>
  /// <returns></returns>
  [HttpGet]

  [Route("GetOnBoardingEmailData")]
  public async Task<IActionResult> GetOnBoardingEmailData(int CandidateId, string PhaseName)
  {
    string AuthKey = "";
    try
    {
      if (HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues authToken))
      {
        AuthKey = authToken.FirstOrDefault() ?? "";
      }

      EmailMessageData emailMessageData = await _ApplicantRepository.GetOnBoardingEmailData(CandidateId, PhaseName, AuthKey);
      if (emailMessageData == null)
      {
        return BadRequest("No Data Found");
      }

      return Ok(emailMessageData);
    }
    catch (Exception ex)
    {
      _logger.Log(ex.Message.ToString());
      return Problem("Failed to Get Applicant Email Message Details");
    }

  }
  /// <summary>
  /// Create New Applicant
  /// </summary>
  /// <returns></returns>
  [HttpPost]
  [Route("CreateNewApplicant")]
  public async Task<IActionResult> CreateNewApplicant([FromBody] ApplicantInput objApplicant)
  {
    try
    {
      int ApplicantId = await _ApplicantRepository.CreateNewApplicant(objApplicant);
      if (ApplicantId > 0)
      {
        return Ok(ApplicantId);
      }
      if(ApplicantId == 0)
      {
        _logger.Log(BadRequest() + "\n");
        return BadRequest("Applicant email is already exists ");
      }
      else
      {
        _logger.Log(BadRequest() + "\n");
        return BadRequest();
      }
    }
    catch (Exception ex)
    {
      _logger.Log(ex.Message.ToString());
      return BadRequest("Failed to create new applicant");
    }

  }

}
