using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class NoteCategoriesData
{
  public int master_category_id { get; set; }
  public string? category { get; set; }
  public int is_active { get; set; }
  public string? master_category_type { get; set; }

  public List<NoteCategoriesChild> note_categories_chaild { get; set; }
}
