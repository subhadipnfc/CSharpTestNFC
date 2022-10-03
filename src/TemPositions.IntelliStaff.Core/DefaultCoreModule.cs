using Autofac;
using TemPositions.IntelliStaff.Core.Interfaces;
using TemPositions.IntelliStaff.Core.Services;

namespace TemPositions.IntelliStaff.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
