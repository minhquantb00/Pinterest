using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.Converters;
using Project_Pinterest.Payloads.DataRequests.CollectionRequests;
using Project_Pinterest.Payloads.DataResponses.DataPostCollection;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Services.Implements
{
    public class PostCollectionService : IPostCollectionService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponsePostCollection> _responseObject;
        private readonly PostCollectionConverter _converter;
        public PostCollectionService(AppDbContext context, PostCollectionConverter converter, ResponseObject<DataResponsePostCollection> responseObject)
        {
            _context = context;
            _converter = converter;
            _responseObject = responseObject;
        }
        public async Task<List<DataResponsePostCollection>> CreateListPostCollection(int userId, int collectionId, List<Request_CreatePostCollection> requests)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsLocked == false && x.IsActive == true);
            var collection = await _context.collections.SingleOrDefaultAsync(x => x.Id ==  collectionId);
            if(collection == null)
            {
                throw new ArgumentNullException("Không tìm thấy bộ sưu tập");
            }
            List<PostCollection> list = new List<PostCollection>();
            foreach (var item in requests)
            {
                PostCollection post = new PostCollection
                {
                    CollectionId = collectionId,
                    PostId = item.PostId
                };
                list.Add(post);
            }
            await _context.postCollections.AddRangeAsync(list);
            await _context.SaveChangesAsync();
            collection.PostCollections.ToList().AddRange(list);
            _context.Update(collection);
            await _context.SaveChangesAsync();
            return list.Select(x => _converter.EntityToDTO(x)).ToList();
        }

        public async Task<ResponseObject<DataResponsePostCollection>> CreatePostCollection(int userId, int collectionId, Request_CreatePostCollection request)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsLocked == false && x.IsActive == true);
            var collection = await _context.collections.SingleOrDefaultAsync(x => x.Id == collectionId);
            if (collection == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bộ sưu tập", null);
            }
            PostCollection post = new PostCollection
            {
                CollectionId = collectionId,
                PostId = request.PostId
            };
            await _context.postCollections.AddAsync(post); 
            await _context.SaveChangesAsync();
            collection.PostCollections.ToList().Add(post);
            _context.Update(collection);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Tạo dữ liệu thành công", _converter.EntityToDTO(post));
        }
    }
}
