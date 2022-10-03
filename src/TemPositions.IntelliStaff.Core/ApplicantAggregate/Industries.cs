using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class Industries
{
  [Key]
  public int Industry_ID { get; set; }
  public string? Description { get; set; }
}
