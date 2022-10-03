using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class EducationLevels
{
  [Key]
  public int Id { get; set; }
  //public int TenantId { get; set; }
  //public int TemplateId { get; set; }
  public string? EducationLevel { get; set; }
}
