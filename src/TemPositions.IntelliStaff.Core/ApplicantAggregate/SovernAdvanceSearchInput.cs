namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class SovernAdvanceSearchInput
{
  public string? unique_job_id { get; set; }
  public string? SovrenTargetIndexes { get; set; }
  public string? JsonJobTitles { get; set; }
  public int WExperience { get; set; }
  public int MExperience { get; set; }
  public int MLevel { get; set; }
  public string JsonSkills { get; set; }
  public string SearchExpression { get; set; }
  public string JsonLocation { get; set; }
  public string DegreeTypeStrings { get; set; }
  public string SchoolNameStrings { get; set; }
  public string DegreeNameStrings { get; set; }
  public bool IsTopStudent { get; set; }
  public bool IsRecentGraduate { get; set; }
  public bool IsCurrentStudent { get; set; }
  public string Certifications { get; set; }
  public string EmployerStrings { get; set; }
  public bool EmployersMustAllBeCurrentEmployer { get; set; }
  public string LanguagesKnownStrings { get; set; }
  public bool LanguagesKnownMustAllExist { get; set; }
  public string CategoryWeights { get; set; }
  public string ExecutiveTypeStrings { get; set; }
  public string DocumentLanguageStrings { get; set; }
  public string DocumentIds { get; set; }
  public string SecurityCredentials { get; set; }
  public bool HasSecurityCredentials { get; set; }
  public bool HasPatents { get; set; }
  public bool IsMilitary { get; set; }
  public bool IsPublicSpeaker { get; set; }
  public bool IsAuthor { get; set; }
  public string CatWeightFilter { get; set; }
}
