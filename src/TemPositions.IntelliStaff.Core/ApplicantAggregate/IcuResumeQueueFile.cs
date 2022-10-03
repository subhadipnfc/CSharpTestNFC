using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
[Table("icu_resume_queue_file")]
public class IcuResumeQueueFile
{
  [Key] 
  public int queue_file_id { get; set; }

  [Column("file_name")]
  public string? FileName { get; set; }
  [Column("file_extension")]
  public string? FileExtension { get; set; }
  [Column("file")]
  public byte[] File { get; set; }

}
