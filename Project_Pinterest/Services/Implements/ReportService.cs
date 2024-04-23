using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.Converters;
using Project_Pinterest.Payloads.DataRequests.ReportRequests;
using Project_Pinterest.Payloads.DataResponses.DataReport;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Services.Implements
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseReport> _responseObject;
        private readonly ReportConverter _converter;
        public ReportService(AppDbContext context, ResponseObject<DataResponseReport> responseObject, ReportConverter converter)
        {
            _context = context;
            _responseObject = responseObject;
            _converter = converter;
        }
        //Report Post
        public async Task<ResponseObject<DataResponseReport>> CreateReport(int userReportId, Request_CreateReport request)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userReportId && x.IsLocked == false && x.IsActive == true);
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == request.PostId && x.IsDeleted == false);
            if(post == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Bài viết không tồn tại", null);
            }
            Report report = new Report
            {
                CreateAt = DateTime.Now,
                PostId = request.PostId,
                Reason = request.Reason,
                ReportType = Constants.ReportType.Post,
                UserReportId = userReportId
            };
            await _context.reports.AddAsync(report);
            await _context.SaveChangesAsync();
            var numberOfReport = _context.reports.Count(x => x.PostId ==  request.PostId);
            if(numberOfReport >= 2)
            {
                post.IsActive = true;
                post.IsDeleted = true;
                _context.posts.Update(post);
                _context.SaveChanges();
            }

            return _responseObject.ResponseSuccess("Báo cáo bài viết thành công", _converter.EntityToDTO(report));
        }

        public async Task<ResponseObject<DataResponseReport>> CreateReportUser(int userReportId, Request_CreateReportUser request)
        {
            var userReport = await _context.users.SingleOrDefaultAsync(x => x.Id == userReportId && x.IsLocked == false && x.IsActive == true);
            var userReported = await _context.users.SingleOrDefaultAsync(x => x.Id == request.UserReportedId && x.IsLocked == false && x.IsActive == true);
            if(userReported == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy người dùng", null);
            }
            Report report = new Report
            {
                CreateAt = DateTime.Now,
                UserReportId = userReportId,
                ReportType = Constants.ReportType.User,
                Reason  = request.Reason,
                UserReportedId = request.UserReportedId,
            };
            await _context.reports.AddAsync(report);
            await _context.SaveChangesAsync();
            var numberOfReport = _context.reports.Count(x => x.UserReportedId == request.UserReportedId);
            if(numberOfReport >= 2)
            {
                userReported.IsLocked = true;
                _context.users.Update(userReported);
                _context.SaveChanges();
            }

            return _responseObject.ResponseSuccess("Báo cáo người dùng thành công", _converter.EntityToDTO(report));
        }

        public async Task<PageResult<DataResponseReport>> GetAllReportByPost(int postId, int pageSize, int pageNumber)
        {
            var query = _context.reports.Include(x => x.Post).AsNoTracking().Where(x => x.PostId == postId).Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPagedData(query, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<DataResponseReport>> GetAllReports(int pageSize, int pageNumber)
        {
            var query = _context.reports.Include(x => x.Post).AsNoTracking().Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPagedData(query, pageSize, pageNumber);
            return result;
        }
    }
}
