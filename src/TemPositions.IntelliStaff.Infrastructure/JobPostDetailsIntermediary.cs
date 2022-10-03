using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TemPositions.IntelliStaff.Infrastructure;
public class JobPostDetailsIntermediary
{
  [Key]
  public int JobPost_ID { get; set; }
  public int CreatedBy { get; set; }
  public string? Division { get; set; }
  public string? Title { get; set; }
  public string? JLevel { get; set; }
  public string? EduLevel { get; set; }
  public string? JIndustry { get; set; }
  public int Order_ID { get; set; }
  public int JobSearchLocID { get; set; }
  public string? JCategory { get; set; }
  public string? JSubCategory { get; set; }
  public string? JobName { get; set; }
  public string? Phone { get; set; }
  public string? Responsibilities { get; set; }
  public string? JobDesc { get; set; }
  public string? Skills { get; set; }
}
