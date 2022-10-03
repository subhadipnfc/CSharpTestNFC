using Ardalis.Specification;

namespace TemPositions.IntelliStaff.SharedKernel.Interfaces;

// from Ardalis.Specification
public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
