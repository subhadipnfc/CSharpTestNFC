using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using TemPositions.IntelliStaff.Core.Interfaces;

namespace TemPositions.IntelliStaff.Web.Api;

public class ResumeController : BaseApiController
{
  private readonly IResumeRepository _ResumeRepository;
  private readonly Tempositions.Intellistaff.Shared.Logging.Core.ILogger _logger;
  public ResumeController(IResumeRepository repository, Tempositions.Intellistaff.Shared.Logging.Core.ILogger logger)
  {
    _ResumeRepository = repository;
    this._logger = logger;
  }
  [HttpGet]

  [Route("GetSovrenResumeFile")]
  public async Task<FileStreamResult> GetSovrenResumeFile(string ResumeId)
  {
    MemoryStream ms = null;
    try
    {
      var resume = await _ResumeRepository.GetSovrenResumeFile(ResumeId);

      if (resume == null && resume?.ResumeBase64 != String.Empty)
      {
        return File(ms, "");
      }
      else
      {
        byte[] byteArray = System.Convert.FromBase64String(resume.ResumeBase64);
        if (byteArray != null)
        {
          ms = new MemoryStream(byteArray);
        }
        // Response...
        //System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
        //{
        //  FileName = resume.ResumeFileName + "." + resume.ResumeFileExtension
        //};
        //Response.Headers.Add("Content-Disposition", cd.ToString());
        Response.Headers.Add("Content-Disposition", resume.ResumeFileName);
        Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(resume.ResumeFileExtension);
        var ContentType = "";
        new FileExtensionContentTypeProvider().TryGetContentType(resume.ResumeFileName, out ContentType);
        if (regKey != null && regKey.GetValue("Content Type") != null)
          ContentType = regKey.GetValue("Content Type").ToString();
        return File(ms, ContentType);
      }

    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return File(ms, "");
    }

  }
  [HttpGet]
  [Route("GetResumeHTML")]
  public async Task<IActionResult> GetResumeHTML(string ResumeId)
  {

    try
    {
      var resume = await _ResumeRepository.GetResumeHTML(ResumeId);
      return Ok(resume);
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return NotFound(new Exception(ex.Message.ToString()));
    }

  }
  [HttpGet]
  [Route("GetSovrenResume")]
  public async Task<IActionResult> GetSovrenResume(string ResumeId, int userid = 0)
  {

    try
    {
      var resume = await _ResumeRepository.GetSovrenResume(ResumeId,userid);
      return Ok(resume);
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return NotFound(new Exception(ex.Message.ToString()));
    }

  }
  [HttpGet]
  [Route("GetCandidateDescriptionByDocumentId")]
  public async Task<IActionResult> GetCandidateDescriptionByDocumentId(string DocumentId)
  {

    try
    {
      var candidateDetails = await _ResumeRepository.GetCandidateDescriptionByDocumentId(DocumentId);
      return Ok(candidateDetails);
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return NotFound(new Exception(ex.Message.ToString()));
    }

  }
  [HttpGet]
  [Route("GetCandidateAddtionalDetailsByDocumentId")]
  public async Task<IActionResult> GetCandidateAddtionalDetailsByDocumentId(string DocumentId)
  {

    try
    {
      var canditateAdditionalDetails = await _ResumeRepository.GetCandidateAddtionalDetailsByDocumentId(DocumentId);
      return Ok(canditateAdditionalDetails);
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return NotFound(new Exception(ex.Message.ToString()));
    }

  }
}
