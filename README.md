# Powerumc.AspNetCore.Hmac

## Configuration

```csharp
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        // Add Hmac HttpClient Factory
        services.AddHmacHttpClientFactory(options => options.SecretKey = Configuration["SecretKey"]);
    }
}
```

## Usage

### WebApi-1

```csharp
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
        using (var httpClient = _httpClientFactory.Create("data"))
        {
            httpClient.BaseAddress = new Uri("http://localhost:5112");

            var response = await httpClient.GetAsync("/tests/headers");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}
```

### WebApi-2
```csharp
[Route("tests")]
[ApiController]
public class TestsController : Controller
{
    [HttpGet("headers")]
    [TypeFilter(typeof(HmacAuthorization))]
    public ActionResult<IEnumerable<string>> GetHeaders()
    {
        return Request.Headers.Select(o => $"{o.Key}={o.Value}").ToList();
    }
}
```

### WebApi-1 Response
```
[
    "Host=localhost:5112",
    "X-Hmac-Hash=4mwXUe457nbYR4EdOm94/wcGLKkABKE/qJYjeN3y5YI=",
    "X-Hmac-Data=eyJ0aW1lIjoxNTMxMzEyNTAzMTQ0fQ=="
]
```