using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using UniversityWebApp.Services.States;

namespace UniversityWebApp.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private const int timeOut = 5000;
        public async Task<Response> AuthorizeUserById(string userId, string address)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(timeOut);
                    HttpRequestMessage req = new HttpRequestMessage();
                    req.RequestUri = new Uri(address);
                    req.Content = new StringContent($"UserId={userId}");
                    req.Method = HttpMethod.Post;
                    req.Headers.Add("Accept", "application/json");
                    var response = await client.SendAsync(req);
                    if (response.IsSuccessStatusCode)
                    {
                        Response res = new Response()
                        {
                            Content = await response.Content.ReadAsStringAsync(),
                            State = ResponseState.Success
                        };
                    }
                    return new Response { State = ResponseState.Failure };
                }
            }
            catch
            {
                return new Response { State = ResponseState.Failure };
            }
        }
    }
}
