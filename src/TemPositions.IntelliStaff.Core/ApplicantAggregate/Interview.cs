using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class  Interview
{
  [Key]
  public int Id { get; set; }

  public int EntityId { get; set; }

  public int EntityType { get; set; }

  public DateTime? InterviewDate { get; set; }
  public string? Comments { get; set; }
  public int Duration { get; set; }
  public int Status { get; set; }

  public int CreatedBy { get; set; }

  public DateTime? CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public int TenantId { get; set; }
  public DateTime? ModifiedOn { get; set; }
}
