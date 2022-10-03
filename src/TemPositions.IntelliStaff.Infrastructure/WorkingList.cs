using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
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

  public int CreatedBy { get; set; }
}
public class WorkingListUser
{
  [Key]
 
  public int WorkingListUserId { get; set; }  
  public int WorkingListId { get; set; }
  public int UserId { get; set; }

  public int CreatedBy { get; set; }
}


public class WorkingListCategoryType
{
  [Key]
  public int WorkingListCategoryTypeId  { get; set; }
  public string? Name { get; set; }
  public int Status { get; set; }
  public string? Description { get; set; }
}
public class WorkingListItem
{
  [Key]
  public int WorkingListItemId { get; set; }
  public int WorkingListId { get; set; }
  public int ItemId { get; set; }
  public int EntityTypeId { get; set; }
  public int SourceId { get; set; }
  public int Status { get; set; }
  public int CreatedBy { get; set; }
  public int CandidateID { get; set; }
  [Column(TypeName = "DateTime2")]
  public DateTime? CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  [Column(TypeName = "DateTime2")]
  public DateTime? ModifiedOn { get; set; }
}
