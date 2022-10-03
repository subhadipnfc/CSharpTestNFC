using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;

public class JobPostDetails
{
  [Key]
  
  public int JobPostId { get; set; }
  public int? TenantId { get; set; }
  public int? TemplateId { get; set; }
  public int? DivisionId { get; set; }
  public string? JobTitle { get; set; }
  public int? EducationlevelId { get; set; }
  public int? JobLevelId { get; set; }
  public int? PositionTypeId { get; set; }
  public double? MinimumPay { get; set; }
  public double? MaximumPay { get; set; }
  public int? PayRateType { get; set; }
  public int? NoOfJobsAvailable { get; set; }
  public int? JobTypeId { get; set; }
  public int? JobSearchLocationId { get; set; }
  public bool? ApprovalStatus { get; set; }
  public int? ApprovalBy { get; set; }
  public DateTime? ApprovalDate { get; set; }
  public string? ApprovalComment { get; set; }
  public int? IsJobPostToJobBoard { get; set; }
  public int? DeviceType { get; set; }
  public int? ApplicationType { get; set; }
  public int? IsActive { get; set; }
  public bool? IsDeleted { get; set; } = false;
  public int? CreatedBy { get; set; }
  public DateTime? CreatedOn { get; set; }
  public int? ModifiedBy { get; set; }
  public DateTime? ModifiedOn { get; set; }

}
