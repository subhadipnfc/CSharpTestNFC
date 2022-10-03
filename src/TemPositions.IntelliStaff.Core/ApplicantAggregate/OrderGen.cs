using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class OrderGen
{
  [Key]
 public int OrderID { get; set; }
  public int Client { get; set; }
  
}
