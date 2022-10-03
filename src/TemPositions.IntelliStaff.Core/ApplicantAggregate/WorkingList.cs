using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class WorkingList
{
  [Key]
  public string? WorkingListName { get; set; }
  public int Status { get; set; }
  public string? FirstName { get; set; }
  public string? WorkingListCategoryTypeId { get; set; }
  public string? WorkingListAccessTypeId { get; set; }
 
  public List<WorkingListUser>? WorkingListUser { get; set; }

}

public class WorkingListUser{
  public int UserId { get; set; }
}
public class WorkingListItem
{
  [Key]
  public int WorkingListItemId { get; set; }
  public int WorkingListId { get; set; }
  public int ItemId { get; set; }
  public int EntityId { get; set; }
  public int SourceId { get; set; }
  public int Status { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }

}
