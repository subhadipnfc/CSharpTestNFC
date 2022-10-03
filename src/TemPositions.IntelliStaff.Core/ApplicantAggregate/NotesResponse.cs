using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class NotesResponse
{
  public int notesId { get; set; }
  public int createdBy { get; set; }
  public int entityTypeId { get; set; }
  public DateTime createdOn { get; set; }
  public int categoryId { get; set; }
  public string? notesText { get; set; }
  public string? UserName { get; set; }
}
