using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ProfileREview
{
  [Key]
  public int Id { get; set; }

public int EntityId { get; set; }

  public int EntityType { get; set; }

  public DateTime? ReviewDate { get; set; }
  public string? Comments { get; set; }
  public int Status { get; set; }

  public int CreatedBy { get; set; }

  public DateTime? CreatedOn { get; set; }
}
