using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.Converters;
using Project_Pinterest.Payloads.DataRequests.CollectionRequests;
using Project_Pinterest.Payloads.DataResponses.DataCollection;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Services.Implements
{
    public class CollectionService : ICollectionService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponseCollection> _responseObject;
        private readonly CollectionConverter _converter;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CollectionService(AppDbContext context, ResponseObject<DataResponseCollection> responseObject, CollectionConverter converter, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _responseObject = responseObject;
            _converter = converter;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseObject<DataResponseCollection>> CreateCollection(int userId, Request_CreateCollection request)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsLocked == false && x.IsActive == true);
            Collection collection = new Collection
            {
                Name = request.Name,
                Title = request.Title,
                UserId = userId,
            };
            await _context.collections.AddAsync(collection);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Thêm bộ sưu tập thành công", _converter.EntityToDTO(collection));
        }

        public async Task<string> DeleteCollection(int userId, int collectionId)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsLocked == false && x.IsActive == true);
            var collection = await _context.collections.SingleOrDefaultAsync(x => x.Id == collectionId);
            if(collection == null)
            {
                return "Bộ sưu tập không tồn tại";
            }
            var currentUser = _httpContextAccessor.HttpContext.User;

            if (!currentUser.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Người dùng không được xác thực hoặc không được xác định");
            }

            if (!currentUser.IsInRole("Admin") || collection.UserId != userId)
            {
                throw new UnauthorizedAccessException("Người dùng không có quyền sử dụng chức năng này");
            }
            _context.collections.Remove(collection);
            await _context.SaveChangesAsync();
            return "Xóa bộ sưu tập thành công";
        }

        public async Task<PageResult<DataResponseCollection>> GetAllCollections(int pageSize, int pageNumber)
        {
            var query = _context.collections.Include(x => x.PostCollections).AsNoTracking().Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPagedData(query, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<DataResponseCollection>> GetCollectionById(int collectionId)
        {
            var collection = await _context.collections.SingleOrDefaultAsync(x => x.Id == collectionId);
            if(collection == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bộ sưu tập", null);
            }
            return _responseObject.ResponseSuccess("Lấy dữ liệu thành công", _converter.EntityToDTO(collection));
        }

        public async Task<PageResult<DataResponseCollection>> GetCollectionByName(string name, int pageSize, int pageNumber)
        {
            var query = _context.collections.Include(x => x.PostCollections).AsNoTracking().Where(x => x.Name.Trim().ToLower().Contains(name.Trim().ToLower())).Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPagedData(query, pageSize, pageNumber);
            return result;
        }

        public async Task<ResponseObject<DataResponseCollection>> UpdateCollection(int userId, Request_UpdateCollection request)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsLocked == false && x.IsActive == true );
            var collection = await _context.collections.SingleOrDefaultAsync(x => x.Id == request.CollectionId);
            if(collection == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bộ sưu tập", null);
            }
            collection.Title = request.Title;
            collection.Name = request.Name;
            _context.collections.Update(collection);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Cập nhật thông tin thành công", _converter.EntityToDTO(collection));
        }
    }
}
