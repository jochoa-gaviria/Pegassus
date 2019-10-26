using Newtonsoft.Json;
using Pegasssus.Common.Models;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pegasssus.Common.Services
{
    public class ApiService : IApiService
    {
        public Task<Response<object>> ChangePasswordAsync(string urlBase, string servicePrefix, string controller, ChangePasswordRequest changePasswordRequest, string tokenType, string accessToken)
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<bool> CheckConnection(string url)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return false;
            }

            return await CrossConnectivity.Current.IsRemoteReachable(url);
        }

        public Task<Response<InvitedResponse>> GetInvitedByEmailAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, string email)
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<Response<object>> GetListAsync<T>(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    tokenType,
                    accessToken);

                var url = $"{servicePrefix}{controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<object>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response<object>
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public Task<Response<OrganizerResponse>> GetOrganizerByEmailAsync(string urlBase, string servicePrefix, string controller, string tokenType, string accessToken, string email)
        {
            //TODO
            throw new NotImplementedException();
        }

        public async Task<Response<TokenResponse>> GetTokenAsync(string urlBase, string servicePrefix, string controller, TokenRequest request)
        {
            try
            {
                var requestString = JsonConvert.SerializeObject(request);
                var content = new StringContent(requestString, Encoding.UTF8, "application/json");
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase)
                };

                var url = $"{servicePrefix}{controller}";
                var response = await client.PostAsync(url, content);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<TokenResponse>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var token = JsonConvert.DeserializeObject<TokenResponse>(result);
                return new Response<TokenResponse>
                {
                    IsSuccess = true,
                    Result = token
                };
            }
            catch (Exception ex)
            {
                return new Response<TokenResponse>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<object>> GetUserTypesAsync<T>(string urlBase, string servicePrefix, string controller, string method)
        {
            try
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(urlBase),
                };


                var url = $"{servicePrefix}{controller}{method}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response<object>
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response<object>
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response<object>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public Task<Response<object>> PutAsync<T>(string urlBase, string servicePrefix, string controller, T model, string tokenType, string accessToken)
        {
            //TODO
            throw new NotImplementedException();
        }

        public Task<Response<object>> RecoverPasswordAsync(string urlBase, string servicePrefix, string controller, EmailRequest emailRequest)
        {
            //TODO
            throw new NotImplementedException();
        }

        public Task<Response<object>> RegisterUserAsync(string urlBase, string servicePrefix, string controller, UserRequest userRequest)
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
