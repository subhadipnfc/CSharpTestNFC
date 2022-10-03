using Ardalis.Specification;

namespace TemPositions.IntelliStaff.SharedKernel.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
