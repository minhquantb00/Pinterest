using Microsoft.EntityFrameworkCore;
using Project_Pinterest.Constants;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Interfaces;

namespace Project_Pinterest.Services.Implements
{
    public class RelationshipService : IRelationshipService
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<Enums.Action> _responseObject;
        public RelationshipService(AppDbContext context, ResponseObject<Enums.Action> responseObject)
        {
            _context = context;
            _responseObject = responseObject;
        }
        public async Task<ResponseObject<Enums.Action>> Follow(int partnerId, int ownerId, string action)
        {
            if(partnerId == ownerId)
            {
                return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không thể follow chính mình", Enums.Action.NOTCONSTRAINT);
            }
            Relationship relationshipExisted = await _context.relationships.Where(x => x.Follower.Id == ownerId && x.Following.Id == partnerId).SingleOrDefaultAsync();
            User partner = await _context.users.SingleOrDefaultAsync(x => x.Id == partnerId && x.IsActive == true && x.IsLocked == false);
            if(partner == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy người dùng", Enums.Action.NOTCONSTRAINT);
            }
            User owner = await _context.users.SingleOrDefaultAsync(x => x.Id == ownerId && x.IsActive == true && x.IsLocked == false);
            if (owner == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy người dùng", Enums.Action.NOTCONSTRAINT);
            }

            Relationship relationship = new Relationship();

            if (action.Equals(Enums.Action.FOLLOW.ToString()) && relationshipExisted == null)
            {
                relationship.Follower = owner;
                relationship.Following = partner;
                await _context.relationships.AddAsync(relationship);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccess("Đã follow", Enums.Action.FOLLOW);
            }
            else if(action.Equals(Enums.Action.UNFOLLOW.ToString()) && relationshipExisted != null)
            {
                relationship = await _context.relationships.Where(x => x.Follower.Id == ownerId && x.Following.Id == partnerId).SingleOrDefaultAsync();
                _context.relationships.Remove(relationship);
                await _context.SaveChangesAsync();
                return _responseObject.ResponseSuccess("Hủy follow thành công", Enums.Action.UNFOLLOW);
            }
            return _responseObject.ResponseError(StatusCodes.Status400BadRequest, "Không hợp lệ", Enums.Action.NOTCONSTRAINT);

        }
    }
}
