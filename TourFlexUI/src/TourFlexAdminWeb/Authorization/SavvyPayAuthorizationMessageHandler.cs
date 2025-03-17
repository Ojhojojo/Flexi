//using System.Net.Http.Headers;
//using SavvyPayLibrary.Services.Authentication;

//namespace SavvyPayWeb.Authorization;

//public class SavvyPayAuthorizationMessageHandler(
//    ICustomAccessTokenProvider tokenProvider)
//    : DelegatingHandler
//{
//    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//    {
//        var token = await tokenProvider.GetAccessTokenAsync();

//        if (!string.IsNullOrEmpty(token))
//        {
//            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
//        }

//        return await base.SendAsync(request, cancellationToken);
//    }
//}