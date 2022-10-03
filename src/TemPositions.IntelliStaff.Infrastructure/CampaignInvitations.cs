using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure; 
[Table("Campaign_Invitations")]
public class CampaignInvitations
{
  [Key]
  public int id { get; set; }
  [Column("campaign_invitation_guid")]
  public Guid CampaignInvitationGuid { get; set; } = new Guid();
  [Column("campaign_id")]
  public int CampaignId { get; set; }
  [Column("applicant_id")]
  public int ApplicantId { get; set; }
  [Column("job_posting_applicant_id")]
  public int JobPostingApplicantId { get; set; }
  [Column("order_id")]
  public int OrderId { get; set; }
  [Column("div_id")]
  public int DivId { get; set; }
  [Column("first_name")]
  public string? FirstName { get; set; }
  [Column("last_name")]
  public string? LastName { get; set; }

  public string? email { get; set; }
  public string? source { get; set; }
  [Column("campaign_invitation_sent_by")]
  public int CampaignInvitationSentby { get; set; }
  [Column("created_date")]
  public DateTime? CreatedDate { get; set; }
  public int IsAutoInvite { get; set; }
  [Column("how_heard_id")]
  public int HowheardId { get; set; }
}
