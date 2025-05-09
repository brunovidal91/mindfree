using Microsoft.AspNetCore.Components;
using MindFree.Interfaces;

namespace MindFree.Utils
{
    public class AddHeadersDelegatingHandler : DelegatingHandler
    {
        [Inject]
        private ICookie _cookie { get; set; }
        public AddHeadersDelegatingHandler() : base(new HttpClientHandler())
        {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", "Bearer "+_cookie.GetValue("app_token"));  // Add whatever headers you want here

            return base.SendAsync(request, cancellationToken);
        }
    }
}
