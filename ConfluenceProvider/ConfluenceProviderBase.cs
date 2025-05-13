using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace PurplePete.ConfluenceProvider
{
    public abstract class ConfluenceProviderBase : ISupportsAuthentication
    {
        public string? BearerToken { get; set; }       

        [SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "CancellationToken is part of the NSwag API spec")]
        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(BearerToken))
                throw new HttpRequestException("Authorization Header is required and 'BearerToken' is not set");
            HttpRequestMessage msg = new HttpRequestMessage();
            msg.Headers.Authorization = new AuthenticationHeaderValue("Bearer", BearerToken);
            return Task.FromResult(msg);
        }
    }
}