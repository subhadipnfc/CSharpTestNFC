using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class Locations
{
  [Key]
  public int Location_Id { get; set; }  
  public string? Location_Name { get; set; }
}
