using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ApplicantQueue
{
  [Key]
  public int Applicant_ID { get; set; }
  public int? Div_ID { get; set; }
  public int? cand_id { get; set; }
  public byte? office { get; set; } = 0;
  public DateTime? SubmitDateTime { get; set; }
  public string? Last_Name { get; set; }
  public string? First_Name { get; set; }
  public string? Middle { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? Zip { get; set; }
  public string? Home_Phone { get; set; }
  public string? Birthday { get; set; }
  public string? Beep { get; set; }
  public string? Alt_Phone { get; set; }
  public DateTime? Date_Avail { get; set; }
  public string? Fax_Phone { get; set; }
  public string? E_Mail { get; set; }
  public string? SSN { get; set; }

  public bool? CFHC { get; set; } = false;
 
  public DateTime? created_date { get; set; }
  public int? TenantId { get; set; }
  public DateTime? CreatedOn { get; set; }
  public int? CreatedBy { get; set; }
  public DateTime? ModifiedOn { get; set; }
  public int? ModifiedBy { get; set; }
  public int? NewApplicantId { get; set; }
  public string? PhaseComplete { get; set; }


  public int? agent_i9_verification_req { get; set; }


}
