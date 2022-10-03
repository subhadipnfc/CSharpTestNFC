using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class EmailTemplates
{
  [Key]
  public int ID { get; set; }
  public string? TemplateName { get; set; }
  public string? EmailSubject { get; set; }
  public int? TenantId { get; set; }
  public string? EmailBody { get; set; }
  public int? UserId { get; set; }
  
  public bool? Status { get; set; }
  public int? EmailTemplateTypeId { get; set; }
  public int? EntityId { get; set; }
}
