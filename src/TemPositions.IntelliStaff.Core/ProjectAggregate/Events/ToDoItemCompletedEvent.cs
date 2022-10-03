using TemPositions.IntelliStaff.Core.ProjectAggregate;
using TemPositions.IntelliStaff.SharedKernel;

namespace TemPositions.IntelliStaff.Core.ProjectAggregate.Events;

public class ToDoItemCompletedEvent : BaseDomainEvent
{
  public ToDoItem CompletedItem { get; set; }

  public ToDoItemCompletedEvent(ToDoItem completedItem)
  {
    CompletedItem = completedItem;
  }
}
