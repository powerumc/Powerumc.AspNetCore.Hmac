using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Powerumc.AspNetCore.Hmac;

namespace SampleWebApi1.Controllers
{
    [Route("tests")]
    [ApiController]
    public class TestsController : Controller
    {
        private readonly IHmacHttpClientFactory _httpClientFactory;

        public TestsController(IHmacHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            using (var httpClient = _httpClientFactory.Create("some_secretKey", "data"))
            {
                httpClient.BaseAddress = new Uri("http://localhost:5112");

                var response = await httpClient.GetAsync("/tests/headers");
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}