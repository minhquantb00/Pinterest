using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.DataRequests.CollectionRequests;
using Project_Pinterest.Payloads.DataResponses.DataCollection;
using Project_Pinterest.Payloads.Responses;

namespace Project_Pinterest.Services.Interfaces
{
    public interface ICollectionService
    {
        Task<ResponseObject<DataResponseCollection>> CreateCollection(int userId, Request_CreateCollection request);
        Task<ResponseObject<DataResponseCollection>> UpdateCollection(int userId, Request_UpdateCollection request);
        Task<string> DeleteCollection(int userId,int collectionId);
        Task<PageResult<DataResponseCollection>> GetAllCollections(int pageSize, int pageNumber);
        Task<ResponseObject<DataResponseCollection>> GetCollectionById(int collectionId);
        Task<PageResult<DataResponseCollection>> GetCollectionByName(string name, int pageSize, int pageNumber);
    }
}
