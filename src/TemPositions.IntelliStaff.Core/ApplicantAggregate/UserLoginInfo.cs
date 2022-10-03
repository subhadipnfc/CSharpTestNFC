using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class UserLoginInfo
{
  [Key]
  public int Id { get; set; }
  public int UserId { get; set; }
  public DateTime? LoginTime { get; set; }

}
