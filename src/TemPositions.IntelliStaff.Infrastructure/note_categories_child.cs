using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class note_categories_child
{
  [Key]
  public int child_category_id { get; set; }
public string? category { get; set; }
public int master_category_id { get; set; }
public decimal base_point_value { get; set; }
public string? description { get; set; }
}
