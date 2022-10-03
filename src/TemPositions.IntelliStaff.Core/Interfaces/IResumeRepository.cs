using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;
namespace TemPositions.IntelliStaff.Core.Interfaces;
public interface IResumeRepository
{
  public Task<Resume> GetSovrenResumeFile(string ResumeId);
  public Task<ResumeHTMLText> GetResumeHTML(string ResumeId);
  public Task<MonsterResumeResponse> GetSovrenResume(string ResumeId, int userid = 0);
  public Task<ApplicantAggregate.CandidateDetails> GetCandidateDescriptionByDocumentId(string DocumentId);
  public Task<CanditateAdditionalDetails> GetCandidateAddtionalDetailsByDocumentId(string DocumentId);
}

