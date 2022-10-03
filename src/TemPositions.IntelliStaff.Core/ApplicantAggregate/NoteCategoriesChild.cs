using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
[Table("note_categories_child")]
public class NoteCategoriesChild
{
  [Key]
  [Column("child_category_id")]
  public int ChildCategoryId { get; set; }
public string? category { get; set; }
  [Column("master_category_id")]
  public int MasterCategoryId { get; set; }
  [Column("base_point_value")]
  public decimal BasePointValue { get; set; }
public string? description { get; set; }
}
