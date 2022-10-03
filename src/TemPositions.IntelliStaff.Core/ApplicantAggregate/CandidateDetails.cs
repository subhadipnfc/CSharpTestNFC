using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class CandidateDetails
{
  public string? Description { get; set; }
  public string? MonthsOfWorkExperience { get; set; }
  public string? AverageMonthsPerEmployer { get; set; }
  public string? FulltimeDirectHirePredictiveIndex { get; set; }
  public string? MonthsOfManagementExperience { get; set; }
  public string? HighestManagementScore { get; set; }
  public string? ExecutiveType { get; set; }
  public string? ManagementStory { get; set; }
  public string? AttentionNeeded { get; set; }

  public SkillsTaxonomyOutput? SkillsTaxonomyOutput { get; set; }
}
public class SkillsTaxonomyOutput
{
  public List<TaxonomyRoot>? TaxonomyRoot { get; set; }
}
public class TaxonomyRoot
{
  public string? Name { get; set; }
  public List<Taxonomy>? Taxonomy { get; set; }
}
public class Taxonomy
{
  public string? Name { get; set; }
  public string? Id { get; set; }
  public string? PercentOfOverall { get; set; }
  public List<Subtaxonomy>? Subtaxonomy { get; set; }
}
public class Subtaxonomy
{
  public string? Name { get; set; }
  public string? Id { get; set; }
  public string? PercentOfOverall { get; set; }
  public string? PercentOfParentTaxonomy { get; set; }
  public List<Skill>? Skills { get; set; }
}
public class Skill
{
  public string? Name { get; set; }
  public string? Id { get; set; }
  public string? ExistsInText { get; set; }
  public string? TotalMonths { get; set; }
  public string? LastUsed { get; set; }
  public string? WhereFound { get; set; }
  public string? ChildrenTotalMonths { get; set; }
  public string? ChildrenLastUsed { get; set; }
  public List<ChildSkill>? ChildSkills { get; set; }

}
public class ChildSkill
{
  public string? Name { get; set; }
  public string? Id { get; set; }
  public string? ExistsInText { get; set; }
  public string? TotalMonths { get; set; }
  public string? LastUsed { get; set; }
  public string? WhereFound { get; set; }
}
