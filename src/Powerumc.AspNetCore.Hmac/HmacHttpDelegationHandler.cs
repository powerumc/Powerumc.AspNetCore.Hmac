﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Powerumc.AspNetCore.Hmac.Extensions;

namespace Powerumc.AspNetCore.Hmac
{
    public class HmacHttpDelegationHandler : DelegatingHandler
    {
        private readonly string _secretKey;
        private readonly string _data;

        public HmacHttpDelegationHandler(string secretKey, string data)
        {
            Guard.ThrowIfNullOrWhitespace(secretKey, nameof(secretKey));
            Guard.ThrowIfNullOrWhitespace(data, nameof(data));
            
            _secretKey = secretKey;
            _data = data;
            InnerHandler = new HttpClientHandler();
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var encryped = _data.EncryptAsHmacHash(_secretKey.ToBytes(), _data.ToBytes());
            var encoded = Convert.ToBase64String(encryped);
            
            request.Headers.TryAddWithoutValidation("X-Hmac-Hash", encoded);
            request.Headers.TryAddWithoutValidation("X-Hmac-Data", _data.EncodeBase64());
            
            return base.SendAsync(request, cancellationToken);
        }
    }
}