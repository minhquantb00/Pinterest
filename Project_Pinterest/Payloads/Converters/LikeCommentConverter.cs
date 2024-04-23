using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataPost;

namespace Project_Pinterest.Payloads.Converters
{
    public class LikeCommentConverter
    {
        private readonly AppDbContext _context;
        public LikeCommentConverter(AppDbContext context)
        {
            _context = context;
        }
        public DataResponseLikeComment EntityToDTO(UserLikeCommentOfPost userLike)
        {
            var userLikeItem = _context.userLikeCommentOfPosts.Include(x => x.User).AsNoTracking().SingleOrDefault(x => x.Id == userLike.Id);
            return new DataResponseLikeComment
            {
                Id = userLike.Id,
                FullName = userLikeItem.User.FullName,
                LikeTime = userLike.LikeTime,
                Unlike = userLike.Unlike
            };
        }
    }
}
