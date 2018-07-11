using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Powerumc.AspNetCore.Hmac.Extensions;

namespace Powerumc.AspNetCore.Hmac.Filters
{
    public class HmacAuthorization : ActionFilterAttribute
    {
        private readonly IConfiguration _configuration;

        public HmacAuthorization(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var secretKey = _configuration["SecretKey"];
            
            if (string.IsNullOrWhiteSpace(secretKey))
                throw new Exception("SecretKey configuration is not set.");
            
            var hash = request.Headers["X-Hmac-Hash"].ToString();
            var data = request.Headers["X-Hmac-Data"].ToString().DecodeBase64();

            if (string.IsNullOrWhiteSpace(hash))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (string.IsNullOrWhiteSpace(data))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var encrypted = data.EncryptAsHmacHash(secretKey.ToBytes(), data.ToBytes());
            var encoded = Convert.ToBase64String(encrypted);

            if (hash != encoded)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            base.OnActionExecuting(context);
        }
    }
}