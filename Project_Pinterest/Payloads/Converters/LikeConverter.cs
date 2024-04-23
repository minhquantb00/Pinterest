using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataPost;

namespace Project_Pinterest.Payloads.Converters
{
    public class LikeConverter
    {
        private readonly AppDbContext _context;
        public LikeConverter(AppDbContext context)
        {
            _context = context;
        }
        public DataResponseLike EntityToDTO(UserLikePost likePost)
        {
            var fullName = _context.users
                                         .Where(x => x.Id == likePost.UserId)
                                         .Select(x => x.FullName)
                                         .SingleOrDefault();

            return new DataResponseLike
            {
                Id = likePost.Id,
                FullName = fullName,
                LikeTime = likePost.LikeTime,
                Unlike = likePost.Unlike
            };
        }

    }
}
