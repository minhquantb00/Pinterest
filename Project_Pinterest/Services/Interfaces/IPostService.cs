using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.PostRequests;
using Project_Pinterest.Payloads.DataResponses.DataPost;
using Project_Pinterest.Payloads.Responses;

namespace Project_Pinterest.Services.Interfaces
{
    public interface IPostService
    {
        Task<ResponseObject<DataResponsePost>> CreatePost(int userId, Request_CreatePost request);
        Task<ResponseObject<DataResponsePost>> UpdatePost(int userId, Request_UpdatePost request);
        Task<string> DeletePost(int userId, int postId);
        Task<PageResult<DataResponsePost>> GetAllPost(int pageSize, int pageNumber);
        Task<PageResult<DataResponsePost>> GetPostByUser(int userId, int pageSize, int pageNumber);
        Task<ResponseObject<DataResponsePost>> GetPostById(int postId);
        Task<string> LikePost(int userId, Request_UserLikePost request);
        Task<ResponseObject<DataResponseComment>> CreateComment(int userId, Request_CreateComment request);
        Task<ResponseObject<DataResponseComment>> UpdateComment(int commentId, int userId, Request_UpdateComment request);
        Task<string> DeleteComment(int userId, Request_DeleteComment request);
        Task<PageResult<DataResponseComment>> GetCommentByUser(int userId, int pageSize, int pageNumber);
        Task<string> UserLikeComment(int userId, int commentId);
        Task<ResponseObject<DataResponsePost>> SharePost(int userId, int postId);
        Task<string> DownloadImageForPost(int postId, string saveDirectory);
        Task<PageResult<DataResponseComment>> GetCommentByPost(int postId, int pageSize, int pageNumber);
        Task<PageResult<DataResponsePost>> GetPostByTitle(string title, int pageSize, int pageNumber);
    }
}
