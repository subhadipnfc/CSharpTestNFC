using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class CandInput
{
  public int ApplicantId { get; set; }
  public int Candid  { get; set; }
  public string? FirstName { get; set; } 
  public string? LastName { get; set; }
  public string? ssn { get; set; } = "000-00-0000";
  public int DivId { get; set; }
  public byte Office { get; set; }
  public int IndependentContractor { get; set; } = 1;
  public DateTime? DateEnter { get; set; } = DateTime.Now;
  public string? Address { get; set; }
  public string? City { get; set; }
  public string? STATE { get; set; }
  public string? Zip { get; set; }
  public string? Email { get; set; }
  public string? Beep { get; set; }
  public string? HomePhone { get; set; }
  public int WorkingListId { get; set; }
  public string? NotesText { get; set; }
  public int ItemId { get; set; }
public int EntityId { get; set; }
public int SourceId { get; set; }
  public int Status { get; set; } = 1;
  public int TenantId { get; set; } = 1;
  public bool InsertNotes { get; set; } = false;
  public int EntityTypeId { get; set; }
  public int UserId { get; set; }
  public int OrderId { get; set; }

  public int RecruitingStageTypeId { get; set; }

  public int RecruitingSubStageTypeId { get; set; }
  public string? RecruitingStageName { get; set; }
}
