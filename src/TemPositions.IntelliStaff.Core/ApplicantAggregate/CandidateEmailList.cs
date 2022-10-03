using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class CandidateEmailList
{
  [Key]
  public int Id { get; set; }
  public int EmailTemplateId { get; set; }

  public string? EmailSubject { get; set; }
  public string? ToMailId { get; set; }

  public string? EmailTo { get; set; }
  public string? EmailCC { get; set; }
  public string? EmailBCC { get; set; }

  public int Status { get; set; } = 1;
  public string? EmailBody { get; set; }
  public int CreatedBy { get; set; }
  public string? EmailFrom { get; set; } = "";
  public DateTime? CreatedOn { get; set; }

  public int ModifiedBy { get; set; }

  public DateTime? ModifiedOn { get; set; }
  
}
