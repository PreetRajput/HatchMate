using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MauiApp1.Services.Generated
{
    public class AuthHandler : DelegatingHandler
    {
        private const string AuthKey = "auth_token";
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await SecureStorage.GetAsync(AuthKey);

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

    }
}
