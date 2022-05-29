using SharedLibrary;

namespace Client.Services
{
    public interface IPeopleService
    {
        Task<Person> AddPerson(Person person);
        Task<List<Person>> GetPeople();
        Task<Person> GetPerson(int id);
    }
}