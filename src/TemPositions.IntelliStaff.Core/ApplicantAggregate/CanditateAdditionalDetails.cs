using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class CanditateAdditionalDetails
{
  public string? Name { get; set; }
  public string? StreetAddress { get; set; }
  public string? City { get; set; }
  public string? PostalCode { get; set; }

  public string? Country { get; set; }
  public string? Phone { get; set; }
  public string? Email { get; set; }
  public string? Schooltype { get; set; }
  public string? NormalizedSchoolname { get; set; }
  public string? DegreeType { get; set; }
  public string? DegreeName { get; set; }
  public string? Comments { get; set; }
  public string? AnyDate { get; set; }
  public string? MonthDay { get; set; }
  public string? StringDate { get; set; }
  public string? Year { get; set; }
  public string? YearMonth { get; set; }
  public string? NormalizedDegreeName { get; set; }
  public string? NormalizedDegreeType { get; set; }
  public string? WhenAvailable { get; set; }
  public string? Countrycode { get; set; }
  public string? Municipality { get; set; }
  public string? AddressLine { get; set; }
  public string? FormattedNumber { get; set; }
  public string? Location { get; set; }
  public string Emailaddress { get; set; }
  public string? Use { get; set; }
  public string? CountryCode { get; set; }
  public string? EmployerOrgName { get; set; }
  public string? PositionType { get; set; }
  public string? CurrentEmployer { get; set; }
  public string? Title { get; set; }
  public string? OrganizationName { get; set; }
  public string EmpCountryCode { get; set; }
  public string EmpMunicipality { get; set; }
  public string? EmpDescription { get; set; }
  public string? EmpStartAnyDate { get; set; }
  public string? EmpStartMonthDay { get; set; }
  public string? EmpStartStringDate { get; set; }
  public string? EmpStartYear { get; set; }
  public string? EmpStartYearMonth { get; set; }
  public string? EmpEndAnyDate { get; set; }
  public string? EmpEndMonthDay { get; set; }
  public string? EmpEndStringDate { get; set; }
  public string? EmpEndYear { get; set; }
  public string? EmpEndYearMonth { get; set; }
  public string? EmpCompanyNameInternalUseOnly { get; set; }
  public string? EmpCompanyNameText { get; set; }
  public string? EmpPosTitleInternalUseOnly { get; set; }
  public string? EmpPosTitleText { get; set; }
  public string? EmpNormalizedOrganizationName { get; set; }
  public string? EmpNormalizedTitle { get; set; }
  public string? EmpNormalizedEmployerOrgName { get; set; }
  public string? EducationHistory { get; set; }
  public string? EmployementHistory { get; set; }
  public string? ContactInfo { get; set; }
  public string? SchoolInfo { get; set; }
  public string? DegreeInfo { get; set; }
  public string? DegreeMajorInfo { get; set; }
  public string? DatesOfAttendanceInfo { get; set; }
  public List<string>? Region { get; set; }
  public string? OrgInfo { get; set; }
  public string? EmpRegion { get; set; }

  public string? EmpJobCategory { get; set; }

  public List<EmploymentHistory>? EmploymentHistory { get; set; }
  public List<ObjEducationhistory>? ObjEducationhistory { get; set; }
  public List<objLicenseOrCertification>? ObjLicenseOrCertification { get; set; }
  public List<objAcheivementAndHonors>? ObjAcheivementAndHonors { get; set; }
  public List<objSovExperienceSummary>? ObjSovExperienceSummary { get; set; }
}
public class objLicenseOrCertification
{
  public List<LicenseOrCertification>? LicenseOrCertification { get; set; }
}
public class LicenseOrCertification
{
  public string? Name { get; set; }
  public string? Id { get; set; }
  public string? Description { get; set; }
  public string? EffectiveDate { get; set; }
}
public class EmploymentHistory
{
  public List<EmployerOrg>? EmployerOrg { get; set; }
}
public class EmployerOrg
{
  public string? EmployerOrgName { get; set; }
  public List<PositionHistory>? PositionHistory { get; set; }
  public string? UserArea { get; set; }
}
public class PositionHistory
{
  public string? PositionType { get; set; }
  public string? CurrentEmployer { get; set; }
  public string? Title { get; set; }
  public OrgName? OrgName { get; set; }
  public List<OrgInfo>? OrgInfo { get; set; }
  public string? Description { get; set; }
  public StartDate? StartDate { get; set; }
  public EndDate? EndDate { get; set; }
  public List<JobCategory>? JobCategory { get; set; }
}
public class OrgName
{
  public string? OrganizationName { get; set; }
}
public class OrgInfo
{
  public PositionLocation? PositionLocation { get; set; }
}
public class PositionLocation
{
  public string? CountryCode { get; set; }
  public List<string>? Region { get; set; }
  public string? Municipality { get; set; }

}
public class StartDate
{
  public string? AnyDate { get; set; }
  public string? MonthDay { get; set; }
  public string? StringDate { get; set; }
  public string? Year { get; set; }
  public string? YearMonth { get; set; }
}
public class EndDate
{
  public string? AnyDate { get; set; }
  public string? MonthDay { get; set; }
  public string? StringDate { get; set; }
  public string? Year { get; set; }
  public string? YearMonth { get; set; }
}
public class JobCategory
{
  public string? TaxonomyName { get; set; }
  public string? CategoryCode { get; set; }
  public string? Comments { get; set; }

}
public class UserArea
{
  public string? PositionHistoryUserArea { get; set; }
  public DegreeUserArea? DegreeUserArea { get; set; }
  public SchoolOrInstitutionTypeUserArea? SchoolOrInstitutionTypeUserArea { get; set; }
}
public class SchoolOrInstitutionTypeUserArea
{
  public string? NormalizedSchoolName { get; set; }
}
public class DegreeUserArea
{
  public string? Id { get; set; }
  public string? NormalizedDegreeName { get; set; }
  public string? NormalizedDegreeType { get; set; }
}
public class ObjEducationhistory
{
  public List<SchoolOrInstitution>? SchoolOrInstitution { get; set; }
}
public class SchoolOrInstitution
{
  public string? SchoolType { get; set; }
  public List<School>? School { get; set; }
  public List<Degree>? Degree { get; set; }
}
public class School
{
  public string? SchoolName { get; set; }
}
public class Degree
{
  public string? DegreeType { get; set; }
  public string? DegreeName { get; set; }
  public DegreeDate? DegreeDate { get; set; }
  public List<DegreeMajor>? DegreeMajor { get; set; }
  public List<DatesOfAttendance>? DatesOfAttendance { get; set; }
  public string? Comments { get; set; }
  public UserArea? UserArea { get; set; }
}
public class DegreeDate
{
  public string? AnyDate { get; set; }
  public string? MonthDay { get; set; }
  public string? StringDate { get; set; }
  public string? Year { get; set; }
  public string? YearMonth { get; set; }
}
public class DegreeMajor
{
  List<string>? Name { get; set; }
}
public class DatesOfAttendance
{
  public StartDate? StartDate { get; set; }
  public EndDate? EndDate { get; set; }
}
public class objAcheivementAndHonors

{
  public string? Description { get; set; }
}
public class objSovExperienceSummary

{
  public string? Category { get; set; }
  public List<SubCategoty>? SubCategoty { get; set; }
  public string? Value { get; set; }
  public string? YearsUsed { get; set; }
}
public class SubCategoty
{
  public string? Name { get; set; }
  public string? YearsUsed { get; set; }
}
