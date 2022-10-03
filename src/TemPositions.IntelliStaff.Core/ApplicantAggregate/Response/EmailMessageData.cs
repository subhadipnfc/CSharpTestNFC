using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.Response;
public class EmailMessageData
{
  [NotMapped]
  public string FromEmailAddress { get; set; }
  [NotMapped]
  public string ToEmailAddress { get; set; }
  [NotMapped]
  public string? CCEmailAddress { get; set; }
  [NotMapped]
  public string? BccEmailAddress { get; set; }
  [NotMapped]
  public string Subject { get; set; }
  [NotMapped]
  public string? AttachmentPath { get; set; }
  [NotMapped]
  public string MessageBody { get; set; }
}
