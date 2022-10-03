using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure; 
[Table("App_Notes")]
public class AppNotes
{
  [Key]
  [Column("Note_id")]
  public int Noteid { get; set; }
  [Column("Date_in")]
  public DateTime Datein { get; set; }
  public string? Activ { get; set; }
  [Column("App_id")]
  public int Appid { get; set; }
  [Column("User_id")]
  public int Userid { get; set; }
  public int Source { get; set; }
  public int CategoryType { get; set; }
  public string? Category { get; set; }
}
