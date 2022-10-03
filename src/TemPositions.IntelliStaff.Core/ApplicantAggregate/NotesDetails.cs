using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class NotesDetails
{
  public int NotesId { get; set; }
  public int TenantId { get; set; }
  public string? NotesText { get; set; }
  public int CategoryTypeId { get; set; }
  public string? CategoryName { get; set; }

  public string? UserName { get; set; }

  public int EntityTypeId { get; set; }

  public int EntityId { get; set; }

  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
}
