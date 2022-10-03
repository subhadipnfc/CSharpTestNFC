using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
[Table("Ord_Call_Results")]
public class OrdCallResults
{
  [Key]
  [Column("ID")]
  public int Id { get; set; }
  [Column("Order_id")]
  public int OrderId { get; set; }
  [Column("cand_id")]
  public int CandId { get; set; }
  public string? Result { get; set; }
  [Column("Other_Comp")]
  public string? OtherComp { get; set; }
  [Column("Other_Res_Divis")]
  public string? OtherResDivis { get; set; }
  public string? resumesent { get; set; }
  public string? interview { get; set; }
  public int daysbefore { get; set; }
  public string? comments { get; set; }
  public int remind { get; set; }
  public int reminddone { get; set; }
  [Column("remind_date")]
  public string? RemindDate { get; set; }
  public string? interview2 { get; set; }
  public int isSubmittal { get; set; }
  public string? ResumeId { get; set; }
}
