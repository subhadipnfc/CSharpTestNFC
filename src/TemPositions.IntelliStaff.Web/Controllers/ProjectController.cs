﻿using TemPositions.IntelliStaff.Core.ProjectAggregate;
using TemPositions.IntelliStaff.Core.ProjectAggregate.Specifications;
using TemPositions.IntelliStaff.SharedKernel.Interfaces;
using TemPositions.IntelliStaff.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TemPositions.IntelliStaff.Web.Controllers;

[Route("[controller]")]
public class ProjectController : Controller
{
  private readonly IRepository<Project> _projectRepository;

  public ProjectController(IRepository<Project> projectRepository)
  {
    _projectRepository = projectRepository;
  }

  // GET project/{projectId?}
  [HttpGet("{projectId:int}")]
  public async Task<IActionResult> Index(int projectId = 1)
  {
    var spec = new ProjectByIdWithItemsSpec(projectId);
    var project = await _projectRepository.GetBySpecAsync(spec);
    if (project == null)
    {
      return NotFound();
    }

    var dto = new ProjectViewModel
    {
      Id = project.Id,
      Name = project.Name,
      Items = project.Items
                    .Select(item => ToDoItemViewModel.FromToDoItem(item))
                    .ToList()
    };
    return View(dto);
  }
}
