using Microsoft.AspNetCore.Mvc.RazorPages;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.ReportRequests;
using Project_Pinterest.Payloads.DataResponses.DataReport;
using Project_Pinterest.Payloads.Responses;

namespace Project_Pinterest.Services.Interfaces
{
    public interface IReportService
    {
        Task<ResponseObject<DataResponseReport>> CreateReport(int userReportId, Request_CreateReport request);
        Task<ResponseObject<DataResponseReport>> CreateReportUser(int userReportId, Request_CreateReportUser request);
        Task<PageResult<DataResponseReport>> GetAllReports(int pageSize, int pageNumber);
        Task<PageResult<DataResponseReport>> GetAllReportByPost(int postId, int pageSize, int pageNumber);
    }
}
