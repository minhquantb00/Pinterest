using Project_Pinterest.Constants;
using Project_Pinterest.Payloads.Responses;

namespace Project_Pinterest.Services.Interfaces
{
    public interface IRelationshipService
    {
        Task<ResponseObject<Enums.Action>> Follow(int partnerId, int ownerId, string action);
    }
}
