﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemPositions.IntelliStaff.Core.ApplicantAggregate;
public class JobSubCategory
{
  [Key]
  public int Id  { get; set; }
  public string? SubCategory { get; set; }  
}
