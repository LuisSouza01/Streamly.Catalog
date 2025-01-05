namespace Streamly.Catalog.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
}