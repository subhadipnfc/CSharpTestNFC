using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
[Table("Cand_Notes")]
public class CandNotes
{
  [Key]
  [Column("Note_id")]
  public int Noteid { get; set; }
  [Column("Date_in")]
  public DateTime Datein { get; set; }
  public string? Activ { get; set; }
  [Column("Cand_id")]
  public int Candid { get; set; }
  [Column("User_id")]
  public int UserId { get; set; }
  public int Source { get; set; }
  public string? Category { get; set; }
  public int CategoryType { get; set; }
}
