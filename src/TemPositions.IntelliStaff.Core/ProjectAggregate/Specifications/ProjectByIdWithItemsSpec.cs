using Ardalis.Specification;
using TemPositions.IntelliStaff.Core.ProjectAggregate;

namespace TemPositions.IntelliStaff.Core.ProjectAggregate.Specifications;

public class ProjectByIdWithItemsSpec : Specification<Project>, ISingleResultSpecification
{
  public ProjectByIdWithItemsSpec(int projectId)
  {
    Query
        .Where(project => project.Id == projectId)
        .Include(project => project.Items);
  }
}
