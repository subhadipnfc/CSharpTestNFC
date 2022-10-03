using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class Resume
{
  public string? UniqueResumeId { get; set; }
  public string? ResumeFileName { get; set; }
  public string? ResumeFileExtension { get; set; }
  public string? ResumeBase64 { get; set; }
  //public byte[]? ResumeFile { get; set; }
}
public class ResumeHTMLText
{
  public string UniqueResumeId { get; set; }
  public string HTMLText { get; set; }
  public string FileName { get; set; }

}
public class MonsterResumeResponse
{
  public int UserId { get; set; }
  public int CandId { get; set; }
  public string ResumeId { get; set; }
  public string seekerRefCode { get; set; }

  public decimal Score { get; set; }
  public string Name { get; set; }
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string MiddleName { get; set; }
  public string Address { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string CountryCode { get; set; }
  public string PostalCode { get; set; }
  public string Phone { get; set; }
  public string AltPhone { get; set; }
  public string EMail { get; set; }
  public string ZipCode { get; set; }

  public string BoardName { get; set; }

  public string ResumeHTML { get; set; }
  public bool HasResumeFile { get; set; }
  public byte[] ResumeFile { get; set; }
  public string ContentType { get; set; }
  public string ResumeTitle { get; set; }
  public string ResumeSubmitted { get; set; }
  public DateTime ResumeModDate { get; set; }
  public string ResumeFileName { get; set; }
  public string ResumeOrigin { get; set; }
  public string ResumeOriginId { get; set; }
  public bool IsViewed { get; set; }

  public string TotalYearsOfExp { get; set; }
  public string CareerLevel { get; set; }
  public string HighestEducationDegree { get; set; }
  public List<Skills> Skills { get; set; }
  public List<Experiences> Experiences { get; set; }
  public List<Education> Educations { get; set; }

  public string TargetCompensation { get; set; }
  public string[] TargetEmploymentCategory { get; set; }
  public string[] TargetOccupation { get; set; }
  public string[] TargetIndustry { get; set; }
  public string GeoPref { get; set; }
  public string JobStatus { get; set; }
  public string JobTitle { get; set; }

  public bool HasNotes { get; set; }
}
public class Experiences
{
  public string Job { get; set; }
  public string Company { get; set; }
  public string YearFrom { get; set; }
  public string YearTo { get; set; }
  public string MonthFrom { get; set; }
  public string MonthTo { get; set; }
  public string Years { get; set; }

  public string Tenure { get { return MonthFrom + "-" + YearFrom + " To " + (YearTo.Equals("2300") ? "Current" : MonthTo + "-" + YearTo); } }
}
public class Skills
{
  public bool JellyDot { get; set; }
  public string Value { get; set; }
  public string LastUsed { get; set; }
  public decimal YearsUsed { get; set; }
}

public class Education
{
  public string degree { get; set; }
  public string SchoolName { get; set; }
  public string Major { get; set; }
  public string Year { get; set; }
}
public class MonsterSearchResponse
{
  public int ResumeFound { get; set; }
  public int ResumeReturned { get; set; }
  public List<Candidate> Candidates { get; set; }

  public string ResponseXML { get; set; }
}

public class Candidate
{
  public string Name { get; set; }
  public string Email { get; set; }
  public string ResumeID { get; set; }
  public string ResumeTitle { get; set; }
  public decimal Score { get; set; }
  public string EducationLevel { get; set; }
  public decimal Experiance { get; set; }
  public DateTime ResumeUpdate { get; set; }
  public string ResumeUpdateString { get; set; }
  public string DesiredSalary { get; set; }
  public string Location { get; set; }
  public List<Skills> Skills { get; set; }
  public List<Experiences> Experiences { get; set; }
  public List<Education> Educations { get; set; }
  public bool IsViewed { get; set; }
  public string ViewedOn { get; set; }
  public bool IsExists { get; set; }
  public bool IsInterviewed { get; set; }
  public bool IsEmailSent { get; set; }
  public bool IsInviteSent { get; set; }
  public bool IsEmailAccepted { get; set; }
  public bool IsEmailRejected { get; set; }
  public bool IsMovedVetted { get; set; }
  public bool IsNA { get; set; }
  public bool IsDNR { get; set; }
  public string DNRReason { get; set; }
  public string DNRDescription { get; set; }
  public bool IsNotDayViewed { get; set; }
  public string ContentType { get; set; }
  public bool HasNotes { get; set; }
  public List<Notes> Notes { get; set; }
  public string OriginRank { get; set; }
  public string JoBPosts { get; set; }
  public string JoBApplicants { get; set; }
  public string CandidateId { get; set; }
  public string OriginSource { get; set; }
  public string msgDelStatus { get; set; }
  public string FirstAppliedDate { get; set; }
  public string DivisionName { get; set; }
  public string LastWorkedWeekendingDate { get; set; }
  public string I9Status { get; set; }
  public string reqcode { get; set; }
  public int Result { get; set; }
  public string ResumeHTML { get; set; }
  public string City { get; set; }
  public string State { get; set; }
  public string zipcode { get; set; }
  public string Phone { get; set; }
  public string ResumeFileName { get; set; }
}
public class Notes
{
  public int NotesId { get; set; }
  public string NotesText { get; set; }
  public string EnterBy { get; set; }
  public DateTime CreateDate { get; set; }
}
