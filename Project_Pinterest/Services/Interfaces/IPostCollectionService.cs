using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataRequests.CollectionRequests;
using Project_Pinterest.Payloads.DataResponses.DataPostCollection;
using Project_Pinterest.Payloads.Responses;

namespace Project_Pinterest.Services.Interfaces
{
    public interface IPostCollectionService
    {
        Task<ResponseObject<DataResponsePostCollection>> CreatePostCollection(int userId, int collectionId, Request_CreatePostCollection request);
        Task<List<DataResponsePostCollection>> CreateListPostCollection(int userId, int collectionId, List<Request_CreatePostCollection> requests);
    }
}
