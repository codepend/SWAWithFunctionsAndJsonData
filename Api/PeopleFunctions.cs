using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Api.Services;
using SharedLibrary;

namespace Api
{
    public class PeopleFunctions
    {
        private readonly IBlobService _blobService;

        public PeopleFunctions(IBlobService blobService)
        {
            _blobService = blobService;
        }

        private readonly string _containerName = "people";
        private readonly string _fileName = "people.json";

        [FunctionName("GetPeople")]
        public async Task<IActionResult> GetPeople(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var people = await _blobService.GetJsonListFromBlob<Person>(_fileName, _containerName);

            return new OkObjectResult(people);

        }

        [FunctionName("AddPerson")]
        public async Task<IActionResult> AddPerson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var person = JsonConvert.DeserializeObject<Person>(body);

            var people = await _blobService.GetJsonListFromBlob<Person>(_fileName, _containerName);
            people.Add(person);

            await _blobService.WriteListAsJsonToBlob(_fileName, _containerName, people);

            return new OkObjectResult(person);

        }
    }
}
