using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly HttpClient _httpClient;

        public PeopleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Person>> GetPeople()
        {
            var response = await _httpClient.GetAsync("/api/GetPeople");
            var people = await response.Content.ReadFromJsonAsync<List<Person>>();
            return people;
        }

        public async Task<Person> GetPerson(int id)
        {
            var response = await _httpClient.GetAsync($"/api/people/{id}");
            var person = await response.Content.ReadFromJsonAsync<Person>();
            return person;
        }

        public async Task<Person> AddPerson(Person person)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/AddPerson", person);
            var createdPerson = await response.Content.ReadFromJsonAsync<Person>();
            return createdPerson;
        }
    }
}
