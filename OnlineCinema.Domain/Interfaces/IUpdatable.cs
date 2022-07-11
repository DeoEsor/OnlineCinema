namespace OnlineCinema.Domain.Interfaces;

public interface IUpdatable<T>
{
    T Update(T updated);
}