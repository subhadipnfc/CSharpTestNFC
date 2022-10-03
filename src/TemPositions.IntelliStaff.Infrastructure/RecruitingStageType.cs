﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Infrastructure;
public class RecruitingStageType
{
  [Key]

  public int RecruitingStageTypeId { get; set; }
  public string? StageName { get; set; }
  public int Status { get; set; }
  public int CreatedBy { get; set; }
  public DateTime CreatedOn { get; set; }
  public int ModifiedBy { get; set; }
  public DateTime ModifiedOn { get; set; }
  public int TenantId { get; set; }
  public int DisplayOrder { get; set; }
}
