using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
[Table("App_Resumes")]
public class AppResumes
{
  [Column("Applicant_ID")]
  public int ApplicantQueueId { get; set; }
  public string? ResumeText { get; set; } 
  
  [Column("Resume_File")]
  public byte[]? ResumeFile { get; set; }

  [Column("Resume_Type")]
  public int? ResumeType { get; set; }

  [Column("filename")]
  public string? Filename { get; set; }

  [Column("Time_Stamp")]
  public DateTime? CreatedOn { get; set; }

  [Column("Res_Extn")]
  public string? ResumeFileExtension { get; set; }

  [Key]
  [Column("Id")]
  public int ResumeId { get; set; }

  [Column("Primary_Done")]
  public bool? PrimaryDone { get; set; }

  [Column("Res_Desc")]
  public string? ResumeFileDescription { get; set; }

}
