using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class OrderGen
{
  [Key]
  [Column("Order_ID")]
  public int OrderID { get; set; }
  public int Client { get; set; }
}
