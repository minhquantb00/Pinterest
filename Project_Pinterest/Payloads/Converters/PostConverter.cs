using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Payloads.DataResponses.DataPost;

namespace Project_Pinterest.Payloads.Converters
{
    public class PostConverter
    {
        private readonly AppDbContext _context;
        private readonly CommentConverter _commentConverter;
        private readonly LikeConverter _userLikePost;
        private readonly UserConverter _userConverter;
        public PostConverter(AppDbContext context, CommentConverter commentConverter, LikeConverter userLikePost, UserConverter userConverter)
        {
            _context = context;
            _commentConverter = commentConverter;
            _userLikePost = userLikePost;
            _userConverter = userConverter;
        }
        public DataResponsePost EntityToDTO(Post post)
        {
            var postItem = _context.posts.Include(x => x.User).Include(x => x.PostStatus).Include(x => x.UserLikePosts).Include(x => x.UserCommentPosts).AsNoTracking().SingleOrDefault(x => x.Id == post.Id);
            return new DataResponsePost
            {
                Id = post.Id,
                Title = post.Title,
                CreateAt = post.CreateAt,
                UpdateAt = post.UpdateAt,
                Description = post.Description,
                ImageUrl = post.ImageUrl,
                DataResponseUser = postItem.User == null ? null : _userConverter.EntityToDTO(postItem.User),
                NumberOfComments = post.NumberOfComments,
                NumberOfLikes = post.NumberOfLikes,
                PostStatusName = postItem.PostStatus.Name,
                DataResponseComments = postItem.UserCommentPosts.Select(x => _commentConverter.EntityToDTO(x)).AsQueryable(),
                DataResponseLikes = postItem.UserLikePosts.Select(x => _userLikePost.EntityToDTO(x)).AsQueryable()
            };
        }
    }
}
