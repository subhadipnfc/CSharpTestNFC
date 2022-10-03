using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class DivisionTheme
{
  [Key]
  public int DivisionID { get; set; }
  public string? APISmallLogoPath { get; set; }
  public string? DivisionName { get; set; } 
}
