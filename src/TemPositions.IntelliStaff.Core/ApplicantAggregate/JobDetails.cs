using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class JobDetails
{
  [Key]
  public int JobPost_Id { get; set; }
  //public int TenantId { get; set; }
  //public int TemplateId { get; set; }
  public string? JCategory { get; set; }
  public string? Title { get; set; }
  public string? JSubCategory { get; set; }
  public string? JIndustry { get; set; }
  public string? Division { get; set; }
  public string? JLevel { get; set; }
  public string? EduLevel { get; set; }
  public string? State { get; set; }
  public string? City { get; set; }
  public string? JobDesc { get; set; }
  public string? Responsibilities { get; set; }
  public string? Skills { get; set; }
  public int EType { get; set; }
  public int User { get; set; }
  public string? Email { get; set; }
  public string? Phone { get; set; }
  public int Order_ID { get; set; }
  public int HotJob { get; set; }
  public int JobSearchLocID { get; set; }
  public string? ZipCode { get; set; }
  public string? JobName { get; set; }
  public string? SkillSet { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedDate { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
}
