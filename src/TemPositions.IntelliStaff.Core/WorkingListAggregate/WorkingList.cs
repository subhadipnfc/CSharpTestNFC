using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.WorkListAggregate;
public class WorkingList
{
  [Key]
  public int WorkingListId { get; set; }
  public int Status { get; set; }
  public string? WorkingListName { get; set; }

  public int WorkingListTypeId { get; set; }
  public int WorkingListAccessTypeId { get; set; }
 
}

public class WorkingListNew
{
  [Key]
  public int WorkingListId { get; set; }
  public int Status { get; set; }
  public string? WorkingListName { get; set; }
  public int WorkingListCategoryTypeId { get; set; }
  public int WorkingListAccessTypeId { get; set; }
}
public class WorkingListUser
{
  [Key]
  public int WorkingListUserId { get; set; }
  public int WorkingListId { get; set; }
  public int UserId { get; set; }
}


public class WorkingListCategoryType
{
  [Key]
  public int WorkingListCategoryTypeId { get; set; }
  public string? Name { get; set; }
  public int Status { get; set; }
  public string? Description { get; set; }
}
public class AddWorkingList
{
  [Key]
  public string? WorkingListName { get; set; }
  public int Status { get; set; } 
  public int WorkingListCategoryTypeId { get; set; }
  public int WorkingListAccessTypeId { get; set; }
  public int CreatedBy { get; set; }
  public List<AddWorkingListUser>? WorkingListUser { get; set; }

}

public class AddWorkingListUser
{
  public int UserId { get; set; }
  public int WorkingListId { get; set; }
  public int CreatedBy { get; set; }
}

public class SearchWorkingList
{
  [Key]
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? EmailAddress { get; set; }
  public string? ApplicantStatus { get; set; }  
  public int WorkingListId { get; set; }
  public int RecruitingSubStageTypeId { get; set; }


}
public class SearchWorkingListQueue
{
  [Key]
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? EmailAddress { get; set; }
  public int Status { get; set; }
  public int WorkingListId { get; set; }
  public string? Phone { get; set; }
  public int TenantId { get; set; } = 0;
  public int UserId { get; set; }

  public int JobPostId { get; set; }

  public int ApplicantId { get; set; }

  public int CandidateId { get; set; }

}

public class SearchWorkingListResult
{
  [Key]
  public int? CandidateID { get; set; }
  public string? CampaignStatus { get; set; }
  public string? CampaignComplete { get; set; }
  public string? AddedDate { get; set; } 
  public string? e_mail { get; set; }
  public int? workingListId { get; set; }
  public int? workingListIteamId { get; set; }
  public int? cand_id { get; set; }

  public string? first_name { get; set; }
  public string? last_name { get; set; }
  public int? div_id { get; set; }
  
  public string? address { get; set; }
  public string? city { get; set; }
  public string? state { get; set; }
  public string? zip { get; set; }  
  public string? home_Phone { get; set; } 
  public int? itemId { get; set; }  
  public string? status { get; set; }
  public int? tenantId { get; set; }  
  public int? applicantID { get; set; }
  public string? ApplicantColorCode { get; set; } 

}

