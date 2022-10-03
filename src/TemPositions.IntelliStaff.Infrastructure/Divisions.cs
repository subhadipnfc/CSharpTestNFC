using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class Divisions
{
  [Key]
  public int Div_ID { get; set; }
  public string? Division { get; set; }
  public string? div_short { get; set; }
  public int Office { get; set; }
  public string? ManagerPassword { get; set; }
  public string? ManagerPassword2 { get; set; }
  public int ShowBdofEd { get; set; }
  public int Mgr_id { get; set; }
  public double TAX { get; set; }
  public string? wccode { get; set; }
  public int srs1div_id { get; set; }
  public double vac_hours { get; set; }
  public double vac_pay_hours { get; set; }
  public double vac_pay_rate { get; set; }
  public string? Display_Name { get; set; }
  public string? wc_code_ext { get; set; }
  public string? logo { get; set; }

  public string? div_info_email { get; set; }
  public int hide_unscheduled_ts_days { get; set; }

  public string? div_notifications_email { get; set; }
  public string? Paystub_Div_name { get; set; }
  public string? PayStub_Div_Address { get; set; }
  public string? PayStub_Div_City { get; set; }
  public string? PayStub_Div_State { get; set; }
  public string? PayStub_Div_Zip { get; set; }
  public string? Paystub_Div_Phone { get; set; }
  public int rep_id { get; set; }
  public int default_order_assign_to { get; set; }
  public int default_no_ts_auto_aprv { get; set; }
  public int TenantId { get; set; }
  public DateTime CreatedOn { get; set; }
  public int CreatedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
  public int ModifiedBy { get; set; }
}
