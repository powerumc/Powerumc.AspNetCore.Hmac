using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Powerumc.AspNetCore.Hmac.Filters;

namespace SampleWebApi2.Controllers
{
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
}