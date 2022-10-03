using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TemPositions.IntelliStaff.Infrastructure;
public class ApplicantSubSource
{
  [Key]
  public int Id { get; set; }
  public int ApplicantSourceId { get; set; }
  public string? SubSourceName { get; set; }
  public int RecordStatus { get; set; }
  public int TenantId { get; set; }
 

}
