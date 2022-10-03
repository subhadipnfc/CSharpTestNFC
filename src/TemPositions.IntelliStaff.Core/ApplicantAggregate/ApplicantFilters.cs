using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class ApplicantFilters
{
  public string? LabelName { get; set; }
  public int PriorityOrder { get; set; }

  public bool IsDefaultSearchFilter { get; set; }

  public int? DivisionId { get; set; }

}
