using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ApplicantNotes
{
  [Key]
  public int Id { get; set; }
  public int TenantId { get; set; }
  public int ApplicantId { get; set; }
  public string? Activity { get; set; }
  public int CategoryType { get; set; }
  public int CategoryId { get; set; }
  public int IsAutomated { get; set; }
  public int RecordStatus { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
}
