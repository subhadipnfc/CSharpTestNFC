using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
[Table("User_Names")]
public class UserNames
{
  [Key]
  [Column("User_ID")]
  public int UserID { get; set; }
  public string? Name { get; set; }

  public string? Email { get; set; }
  public string? Phone { get; set; }  
}
