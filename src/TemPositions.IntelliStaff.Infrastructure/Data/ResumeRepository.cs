using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TemPositions.IntelliStaff.Core.Interfaces;
using TemPositions.IntelliStaff.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;

namespace TemPositions.IntelliStaff.Infrastructure.Data;
public class ResumeRepository : IAggregateRoot, IResumeRepository
{
  private readonly IntelliStaffContext intelliStaffContext;
  private IConfiguration Configuration;
  private readonly Tempositions.Intellistaff.Shared.Logging.Core.ILogger _logger;
  public ResumeRepository(IntelliStaffContext context, IConfiguration _configuration, Tempositions.Intellistaff.Shared.Logging.Core.ILogger logger)
  {
    this.intelliStaffContext = context;
    Configuration = _configuration;
    _logger = logger;
  }
  public class Base64Resume
  {
    public string? UniqueResumeId { get; set; }
    public string? ResumeFileName { get; set; }
    public string? ResumeFileExtension { get; set; }
    public string? ResumeBase64 { get; set; }
  }
  public async Task<Core.ApplicantAggregate.Resume> GetSovrenResumeFile(string ResumeId)
  {
    Core.ApplicantAggregate.Resume? objResume = new Core.ApplicantAggregate.Resume();
    try
    {
      if (intelliStaffContext != null)
      {
        objResume = (from a in intelliStaffContext.ICUResumeQueue.AsNoTracking()
                       from b in intelliStaffContext.IcuResumeQueueFile.AsNoTracking()
                       .Where(ass => ass.queue_file_id == a.id && a.UniqueResumeId == ResumeId && a.IsProcess == 1)                      
        select new Core.ApplicantAggregate.Resume
        {
          UniqueResumeId = a.UniqueResumeId,
          ResumeFileName = b.FileName,
          ResumeBase64 = Convert.ToBase64String(b.File),
          ResumeFileExtension = b.FileExtension,
        }).SingleOrDefault();

        if (objResume.ResumeBase64 != null)
        {
          return objResume;
        }
        else
        {
          var objJson = "{\"DocumentId\":\"" + ResumeId + "\"}";
          var body = JsonConvert.SerializeObject(objJson);
          string result = "";
          string headerInfo = string.Empty;

          using (WebClient client = new WebClient())
          {
            var URL = this.Configuration.GetSection("Api")["SovrenResumeSearchAPI"];
            client.BaseAddress = URL + "SovrenResumeSearch/GetDocumentBase64ResumeForDownload/";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            result = client.UploadString(client.BaseAddress, "POST", objJson);
            var response = JsonConvert.DeserializeObject(result);
            if (response != null)
            {
              objResume = JsonConvert.DeserializeObject<Core.ApplicantAggregate.Resume>(response.ToString());
            }

          }
        }
         
      }

    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }
    return objResume;
  }
  public async Task<Core.ApplicantAggregate.ResumeHTMLText> GetResumeHTML(string ResumeId)
  {
    Core.ApplicantAggregate.ResumeHTMLText? objResumeHTMLText = new Core.ApplicantAggregate.ResumeHTMLText();
    try
    {
      if (intelliStaffContext != null)
      {
       

          var objJson = "{\"UniqueResumeId\":\"" + ResumeId + "\"}";
          var body = JsonConvert.SerializeObject(objJson);
          string result = "";
          string headerInfo = string.Empty;

          using (WebClient client = new WebClient())
          {
            var URL = this.Configuration.GetSection("Api")["SovrenResumeSearchAPI"];
            client.BaseAddress = URL + "PDFToHTML/ConvertPDFToHTML/";
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.Headers[HttpRequestHeader.Accept] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            result = client.UploadString(client.BaseAddress, "POST", objJson);
            var response = JsonConvert.DeserializeObject<Core.ApplicantAggregate.ResumeHTMLText>(result.ToString());
            objResumeHTMLText = response;
          }
        

      }

    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      throw ex;
    }
    return objResumeHTMLText;
  }
  public static string GetBase64StringForImage(string imgPath)
  {
    byte[] bytes = Encoding.ASCII.GetBytes(imgPath);
    string base64String = Convert.ToBase64String(bytes);
    return base64String;
  }
  public async Task<Core.ApplicantAggregate.MonsterResumeResponse> GetSovrenResume(string ResumeId, int userid = 0)
  {
    var objMonsterSearchResponse = new Core.ApplicantAggregate.MonsterSearchResponse();
    var objResumeResponse = new Core.ApplicantAggregate.MonsterResumeResponse();
    try
    {
      var objJson = "{\"DocumentId\":\"" + ResumeId + "\"}";
      var body = JsonConvert.SerializeObject(objJson);
      using (WebClient client = new WebClient())
      {
        var URL = this.Configuration.GetSection("Api")["SovrenResumeSearchAPI"];
        client.BaseAddress = URL + "SovrenResumeSearch/GetResumeByDocumentID/";
        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        client.Headers[HttpRequestHeader.Accept] = "application/json";
        client.Encoding = System.Text.Encoding.UTF8;
        string result = client.UploadString(client.BaseAddress, "POST", objJson);
        //var response = new JavaScriptSerializer().Deserialize<MonsterSearchResponse>(result);
        //var response = JsonConvert.DeserializeObject<MonsterSearchResponse>(result);
        var response = JsonConvert.DeserializeObject(result);
        var response1 = JsonConvert.DeserializeObject<Core.ApplicantAggregate.MonsterSearchResponse>(response.ToString());
        objMonsterSearchResponse = response1;
        if (objMonsterSearchResponse?.Candidates != null && objMonsterSearchResponse.Candidates.Count > 0)
        {
          var objCandidate = objMonsterSearchResponse.Candidates[0];
          objResumeResponse.ResumeId = objCandidate.ResumeID;
          objResumeResponse.Name = objCandidate.Name;
          objResumeResponse.EMail = objCandidate.Email;
          objResumeResponse.ResumeModDate = objCandidate.ResumeUpdate;
          objResumeResponse.Skills = objCandidate.Skills;
          objResumeResponse.Educations = objCandidate.Educations;
          objResumeResponse.Experiences = objCandidate.Experiences;
          objResumeResponse.TotalYearsOfExp = Convert.ToString(objCandidate.Experiance);
          objResumeResponse.HighestEducationDegree = objCandidate.EducationLevel;
          objResumeResponse.Address = objCandidate.Location;
          objResumeResponse.ResumeHTML = objCandidate.ResumeHTML;
          objResumeResponse.City = objCandidate.City;
          objResumeResponse.State = objCandidate.State;
          objResumeResponse.ResumeFileName = objCandidate.ResumeFileName;
          objResumeResponse.Phone = objCandidate.Phone;
          objResumeResponse.PostalCode = objCandidate.zipcode;

        }
      }
      
    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      throw ex;
    }
    return objResumeResponse;
  }
  public async Task<Core.ApplicantAggregate.CandidateDetails> GetCandidateDescriptionByDocumentId(string DocumentId)
  {
    Core.ApplicantAggregate.CandidateDetails? objCandidate = new Core.ApplicantAggregate.CandidateDetails();
    try
    {
      if (intelliStaffContext != null)
      {
        var objJson = "{\"DocumentId\":\"" + DocumentId + "\"}";
        var body = JsonConvert.SerializeObject(objJson);
        string result = "";
        string headerInfo = string.Empty;

        using (WebClient client = new WebClient())
        {
          var URL = this.Configuration.GetSection("Api")["SovrenResumeSearchAPI"];
          client.BaseAddress = URL + "SovrenResumeSearch/GetCandidateDescriptionByDocumentId";
          client.Headers[HttpRequestHeader.ContentType] = "application/json";
          client.Headers[HttpRequestHeader.Accept] = "application/json";
          client.Encoding = System.Text.Encoding.UTF8;
          result = client.UploadString(client.BaseAddress, "POST", objJson);
          var response = JsonConvert.DeserializeObject(result);
          if (response != null)
          {
            objCandidate = JsonConvert.DeserializeObject<Core.ApplicantAggregate.CandidateDetails>(response.ToString());
          }

        }
      }

    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }
    return objCandidate;
  }
  public async Task<Core.ApplicantAggregate.CanditateAdditionalDetails> GetCandidateAddtionalDetailsByDocumentId(string DocumentId)
  {
    Core.ApplicantAggregate.CanditateAdditionalDetails? objCandidateAdditionalDetails = new Core.ApplicantAggregate.CanditateAdditionalDetails();
    try
    {
      if (intelliStaffContext != null)
      {
        var objJson = "{\"DocumentId\":\"" + DocumentId + "\"}";
        var body = JsonConvert.SerializeObject(objJson);
        string result = "";
        string headerInfo = string.Empty;

        using (WebClient client = new WebClient())
        {
          var URL = this.Configuration.GetSection("Api")["SovrenResumeSearchAPI"];
          client.BaseAddress = URL + "SovrenResumeSearch/GetCandidateAddtionalDetailsByDocumentId";
          client.Headers[HttpRequestHeader.ContentType] = "application/json";
          client.Headers[HttpRequestHeader.Accept] = "application/json";
          client.Encoding = System.Text.Encoding.UTF8;
          result = client.UploadString(client.BaseAddress, "POST", objJson);
          var response = JsonConvert.DeserializeObject(result);
          if (response != null)
          {
            objCandidateAdditionalDetails = JsonConvert.DeserializeObject<Core.ApplicantAggregate.CanditateAdditionalDetails>(response.ToString());
          }

        }
      }

    }
    catch (Exception ex)
    {
      _logger.Log(ex);
      return null;
    }
    return objCandidateAdditionalDetails;
  }
}
