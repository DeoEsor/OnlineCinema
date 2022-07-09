namespace OnlineCinema.Domain.Person.Interfaces;

public interface IPersons
{
    Task Add(Person person);
    Task<IReadOnlyList<Person>> GetListAsync();
    Task<Person> ChangeAsync(Person person);
}