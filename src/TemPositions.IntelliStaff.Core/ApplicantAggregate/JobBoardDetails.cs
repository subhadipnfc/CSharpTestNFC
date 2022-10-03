using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class JobBoardDetails
{
  [Key]
  public int JobBoardId { get; set; }
   public int TenantId { get; set; }
  public string? BoardName { get; set; }
  public int IsActive { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }

}
