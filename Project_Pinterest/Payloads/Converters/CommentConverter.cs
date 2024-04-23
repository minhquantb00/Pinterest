using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataPost;

namespace Project_Pinterest.Payloads.Converters
{
    public class CommentConverter
    {
        private readonly AppDbContext _context;
        private readonly LikeCommentConverter _likeCommentConverter;

        public CommentConverter(AppDbContext context, LikeCommentConverter likeCommentConverter)
        {
            _context = context;
            _likeCommentConverter = likeCommentConverter;
        }
        public DataResponseComment EntityToDTO(UserCommentPost post)
        {
            var userComment = _context.userCommentPosts.Include(x => x.User).Include(x => x.UserLikeCommentOfPosts).AsNoTracking().SingleOrDefault(x => x.Id == post.Id);
            return new DataResponseComment
            {
                Id = post.Id,
                CreateAt = DateTime.Now,
                Content = post.Content,
                FullName = userComment.User.FullName,
                NumberOfLikes = post.NumberOfLikes,
                DataResponseLikeComments = userComment.UserLikeCommentOfPosts.Select(x => _likeCommentConverter.EntityToDTO(x)).AsQueryable()
            };
        }
    }
}
