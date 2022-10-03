using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class ApplicantLogo
{
  [Key]  
  public int Id { get; set; }
  public string? LogoName { get; set; }
  public string? LogoPath { get; set; }
  public int JobBoardId { get; set; }
}
