using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataUser;

namespace Project_Pinterest.Payloads.Converters
{
    public class UserConverter
    {
        private readonly AppDbContext _context;
        public UserConverter(AppDbContext context)
        {
            _context = context;
        }
        public DataResponseUser EntityToDTO(User user)
        {
            if (user == null || user.Id == null)
            {
                throw new ArgumentNullException("User is null or User.Id is null");
            }

            var userItem = _context.users
                .Include(x => x.Role)
                .Include(x => x.UserStatus)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == user.Id);

            if (userItem == null)
            {
                return null;
            }

            return new DataResponseUser
            {
                Id = user.Id,
                AvatarUrl = user.AvatarUrl,
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FullName = user.FullName,
                RoleName = userItem.Role?.Name,
                UserName = user.UserName,
                UserStatusName = userItem.UserStatus?.Name


            };
        }
    }
}
