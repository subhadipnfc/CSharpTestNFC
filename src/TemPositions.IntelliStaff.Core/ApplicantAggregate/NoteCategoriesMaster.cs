using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
[Table("note_categories_master")]
public class NoteCategoriesMaster
{
  [Key]
  [Column("master_category_id")]
  public int MasterCategoryId { get; set; }
  public string? category { get; set; }
  [Column("is_active")]
  public int IsActive { get; set; }
  [Column("master_category_type")]
  public string? MasterCategoryType { get; set; }
}
