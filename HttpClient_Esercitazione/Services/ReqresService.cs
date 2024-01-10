using HttpClient_Esercitazione.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace HttpClient_Esercitazione.Services
{
    public class ReqresService : IReqresService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ReqresService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<string> GetListUsersAsStringResultAsync(CancellationToken token = default)
        {
            var httpClient = _httpClientFactory.CreateClient("reqres");

            try
            {
                using (var response = await httpClient.GetAsync("users?page=2", token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        return response.StatusCode.ToString();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Errore di richiesta HTTP: {ex.Message}";
            }
            catch (TaskCanceledException)
            {
                return "Operazione annullata dall'utente";
            }
        }

        public async Task<(ListResult<User>, string)> GetListUsersAsync(CancellationToken token = default)
        {
            var httpClient = _httpClientFactory.CreateClient("reqres");

            var strResult = string.Empty;

            try
            {
                using (var response = await httpClient.GetAsync("users?page=2", token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var streamResult = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            var users = JsonSerializer.Deserialize<ListResult<User>>(streamResult, _jsonSerializerOptions);
                            strResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            return users != null ? (users, strResult) : (new ListResult<User>(), strResult);
                        }     
                    }
                    else
                    {
                        strResult = response.StatusCode.ToString();
                        return (new ListResult<User>(), strResult);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return (new ListResult<User>(), $"Errore di richiesta HTTP: {ex.Message}");
            }
            catch (TaskCanceledException)
            {
                strResult = "Operazione annullata";
                return (new ListResult<User>(), strResult);
            } 
        }

        public async Task<string> CreateNewUserAsStringResultAsync(CancellationToken token = default)
        {
            var newUser = new NewUser
            {
                Name = "Mario",
                Job = "Software developer"
            };

            try
            {
                var httpClient = _httpClientFactory.CreateClient("reqres");

                using (var response = await httpClient.PostAsJsonAsync("users?", newUser, token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        return response.StatusCode.ToString();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Errore di richiesta HTTP: {ex.Message}";
            }
            catch (TaskCanceledException)
            {
                return "Operazione annullata";
            }
        }

        public async Task<(NewUserCreated?, string)> CreateNewUserAsync(CancellationToken token = default)
        {
            var newUser = new NewUser
            {
                Name = "Mario",
                Job = "Software developer"
            };

            try
            {
                var httpClient = _httpClientFactory.CreateClient("reqres");

                using(var response = await httpClient.PostAsJsonAsync("users", newUser, token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using(var streamResponse = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            var newUserCreated = JsonSerializer.Deserialize<NewUserCreated>(streamResponse, _jsonSerializerOptions);
                            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            return (newUserCreated, content);
                        }
                    }
                    else
                    {
                        return (null, response.StatusCode.ToString());
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return (null, $"Errore di richiesta HTTP: {ex.Message}");
            }
            catch (TaskCanceledException)
            {
                return (null, "Operazione annullata");
            }
        }

        public async Task<string> GetSingleUserNotFoundAsync(CancellationToken token = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("reqres");

                using (var response = await httpClient.GetAsync("users/23", token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        return response.StatusCode.ToString();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Errore di richiesta HTTP: {ex.Message}";
            }
            catch (TaskCanceledException)
            {
                return "Operazione annullata";
            }
        }

        public async Task<(SingleResource?, string)> GetSingleResourceAsync(CancellationToken token = default)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("reqres");

                using(var response = await httpClient.GetAsync("unknown/2", token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using(var streamResponse = response.Content.ReadAsStream())
                        {
                            var singleResource = JsonSerializer.Deserialize<SingleResource>(streamResponse, _jsonSerializerOptions);
                            var stringResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                            return(singleResource, stringResponse);
                        }
                    }
                    else
                    {
                        return (null, response.StatusCode.ToString());  
                    }
                }
            }
            catch(HttpRequestException ex)
            {
                return (null, $"Errore di richiesta HTTP: {ex.Message}");
            }
            catch (TaskCanceledException)
            {
                return (null, "Operazione annullata");
            }
        }

        public async Task<string> GetDelayedResponseAsync(CancellationToken token = default)
        {
            var httpClient = _httpClientFactory.CreateClient("reqres");

            try
            {
                using (var response = await httpClient.GetAsync("users?delay=4", token).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    }
                    else
                    {
                        return response.StatusCode.ToString();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Errore di richiesta HTTP: {ex.Message}";
            }
            catch (TaskCanceledException)
            {
                return "Operazione annullata";
            } 
        }

        public async Task<string> DeleteUserAsync(CancellationToken token = default)
        {
            var httpClient = _httpClientFactory.CreateClient("reqres");

            try
            {
                using(var request = new HttpRequestMessage(HttpMethod.Delete, "users/2"))
                {
                    using(var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
                    {
                        if (response.IsSuccessStatusCode) 
                        {
                            return "OK"; //204: no content
                        }
                        else
                        {
                            return response.StatusCode.ToString();
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return $"Errore di richiesta HTTP: {ex.Message}";
            }
            catch (TaskCanceledException)
            {
                return "Operazione annullata";
            }
        }
    }
}
