using HttpClient_Esercitazione.Models;

namespace HttpClient_Esercitazione.Services
{
    public interface IReqresService
    {
        Task<string> GetListUsersAsStringResultAsync(CancellationToken token);
        Task<(ListResult<User>, string)> GetListUsersAsync(CancellationToken token);
        Task<string> CreateNewUserAsStringResultAsync(CancellationToken token);
        Task<(NewUserCreated, string)> CreateNewUserAsync(CancellationToken token);
        Task<string> GetSingleUserNotFoundAsync(CancellationToken token);
        Task<(SingleResource, string)> GetSingleResourceAsync(CancellationToken token);
        Task<string> GetDelayedResponseAsync(CancellationToken token);
        Task<string> DeleteUserAsync(CancellationToken token);
    }
}