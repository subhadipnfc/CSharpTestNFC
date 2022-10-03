using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[Table("Can_Prof_Gen")]
public class CanProfGen
{
  [Key]
  [Column("Cand_ID")]
  public int CandID { get; set; }
  [Column("Last_Name")]
  public string? LastName { get; set; }
  public string? Middle { get; set; }
  [Column("First_Name")]
  public string? FirstName { get; set; }
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? State { get; set; }
  public string? Zip { get; set; }
  [Column("Home_Phone")]
  public string? HomePhone { get; set; }
  public string? Beep { get; set; }
  [Column("Alt_Phone")]
  public string? AltPhone { get; set; }
  [Column("E_Mail")]
  public string? EMail { get; set; }
  [Column("Min_Pay")]
  public decimal MinPay { get; set; }
  public int Crime { get; set; }
  public string? SSN { get; set; }
  [Column("I9_Compl")]
  public string? I9Compl { get; set; }
  [Column("Un_Skill")]
  public string? UnSkill { get; set; }
  [Column("E_Rating")]
  public float ERating { get; set; }
  [Column("div_id")]
  public int Divid { get; set; }
  public int office { get; set; }
  [Column("EMp_SINCE")]
  public DateTime EMpSINCE { get; set; }
  [Column("date_enter")]
  public DateTime Dateenter { get; set; }
  [Column("Date_Avail")]
  public DateTime DateAvail { get; set; }
  [Column("Fax_Phone")]
  public string? FaxPhone { get; set; }
  public int REp { get; set; }
  [Column("ref_id")]
  public string? Refid { get; set; }
  public int DirectDep { get; set; }
  [Column("DNR_Reas_id")]
  public int DNRReasid { get; set; }
  [Column("Dnr_Checked")]
  public int DnrChecked { get; set; }
  [Column("Dnr_Desc")]
  public string? DnrDesc { get; set; }
  public string? notes { get; set; }
  public int hot { get; set; }
  [Column("pos_seeking")]
  public string? Posseeking { get; set; }
  public int Unempl { get; set; }
  public DateTime unemplasof { get; set; }
  public int NeedUpdated { get; set; }
  public DateTime LastTimeslipWE { get; set; }
  public int fromast { get; set; }
  [Column("Net_Profit")]
  public int NetProfit { get; set; }
  [Column("I9_Verified")]
  public int I9Verified { get; set; }
  [Column("Health_Sup")]
  public int HealthSup { get; set; }
  [Column("In_Empowerment_Zone")]
  public int InEmpowerment_Zone { get; set; }
  [Column("app_submit_date")]
  public DateTime Appsubmitdate { get; set; }
  [Column("no_paystub")]
  public int Nopaystub { get; set; }
  [Column("Contact_Name")]
  public string? ContactName { get; set; }
  [Column("Contact_Relationship")]
  public string? ContactRelationship { get; set; }
  [Column("Contact_Phone")]
  public string? ContactPhone { get; set; }
  [Column("Text_Msg_Email")]
  public string? TextMsgEmail { get; set; }
  [Column("PJB_Contact_Method")]
  public int PJBContactMethod { get; set; }
  [Column("sf_hc_waiver")]
  public int Sfhcwaiver { get; set; }
  [Column("recruiter_id")]
  public int Recruiterid { get; set; }
  [Column("send_text_msgs")]
  public int Sendtextmsgs { get; set; }
  [Column("cell_carrier_id")]
  public int Cellcarrierid { get; set; }
  [Column("payroll_card_agree_timestamp")]
  public DateTime Payrollcardagreetimestamp { get; set; }
  [Column("agent_i9_verification_req")]
  public int Agenti9verificationreq { get; set; }
  [Column("wotc_printed")]
  public DateTime Wotcprinted { get; set; }
  [Column("no_ivr_calls")]
  public int Noivrcalls { get; set; }
  [Column("pjb_contact_ivr_call")]
  public int Pjbcontactivrcall { get; set; }
  [Column("pjb_contact_email")]
  public int Pjbcontactemail { get; set; }
  [Column("pjb_contact_text")]
  public int Pjbcontact_text { get; set; }
  [Column("independent_contractor")]
  public int Independentcontractor { get; set; }
  [Column("aoc_referral")]
  public int Aocreferral { get; set; }
  public string? apt { get; set; }
  [Column("occ_year_of_exp")]
  public string? Occyearofexp { get; set; }
  [Column("Cre_Auto_Emails")]
  public int CreAutoEmails { get; set; }
  [Column("Max_Pay")]
  public decimal MaxPay { get; set; }
  [Column("rcv_ivr")]
  public int Rcvivr { get; set; }
  [Column("EVerify_Status")]
  public int EVerifyStatus { get; set; }
  [Column("max_education_level")]
  public int Maxeducationlevel { get; set; }
  [Column("rn_number")]
  public string? Rnnumber { get; set; }
  [Column("is_perm_candidate")]
  public int Ispermcandidate { get; set; }
  [Column("is_clinical")]
  public int Isclinical { get; set; }
  [Column("update_mobilenumber_date")]
  public DateTime Updatemobilenumberdate { get; set; }
  public int AgreementType { get; set; }
  public int TenantId { get; set; }
  public DateTime CreatedOn { get; set; }
  public int CreatedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime DOB { get; set; }
  public int Spouse { get; set; }
  [Column("Referral_Source")]
  public string? ReferralSource { get; set; }
  [Column("How_Heard_ID")]
  public int HowHeardID { get; set; }
  [Column("ref_sub_source_id")]
  public int Refsubsourceid { get; set; }
  [Column("liability_agree_datetime")]
  public DateTime Liabilityagreedatetime { get; set; }
  public int CFHC { get; set; }
  [Column("CFHC_Expiration")]
  public DateTime CFHCExpiration { get; set; }
  public int StatusType { get; set; }
  public int OnBoardingType { get; set; } = 1;
  public int IsAdmittedInUS { get; set; }
}
