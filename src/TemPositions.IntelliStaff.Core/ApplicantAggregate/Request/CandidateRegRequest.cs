using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate.Request;
public class CandidateRegRequest
{
  public string username { get; set; }
  public string email { get; set; }
  public string firstName { get; set; }
  public string lastName { get; set; }
  public int requestSource { get; set; }
  public int divId { get; set; }
  public string password { get; set; }
  public bool shouldSendEmail { get; set; }
  public bool isTempPassword { get; set; }
  public int tenantId { get; set; }
  public int cand_ID { get; set; }
}
