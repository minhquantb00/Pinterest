using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.UserRequests;
using Project_Pinterest.Payloads.DataResponses.DataUser;
using Project_Pinterest.Payloads.Responses;

namespace Project_Pinterest.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseObject<DataResponseUser>> UpdateUser(int userId, Request_UpdateUserInfor request);
        Task<string> DeleteUser(int userId);
        Task<PageResult<DataResponseUser>> GetAllUsers(int pageSize, int pageNumber);
        Task<PageResult<DataResponseUser>> GetUserByName(string? name, int pageSize, int pageNumber);
        Task<string> LockAccount(int adminId, int userLockedId);
        Task<string> UnLockAccount(int adminId, int userUnLockedId);
        Task<string> ChangeDecentralization(Request_ChangeDecentralization request);
        Task<ResponseObject<DataResponseUser>> GetUserById(int userId);
        Task<DataResponseUserInformation> GetUserInformation(int userId);
        Task<PageResult<DataResponseUser>> GetUserByRole(int roleId, int pageSize, int pageNumber);
    }
}
