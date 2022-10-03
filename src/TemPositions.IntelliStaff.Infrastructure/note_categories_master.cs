using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class note_categories_master
{
  [Key]
  public int master_category_id { get; set; }
  public string? category { get; set; }
  public int is_active { get; set; }
  public string? master_category_type { get; set; }
}
