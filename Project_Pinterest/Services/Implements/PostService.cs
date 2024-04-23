using Microsoft.EntityFrameworkCore;
using Project_Pinterest.DataContexts;
using Project_Pinterest.Entities;
using Project_Pinterest.Handler.HandleImage;
using Project_Pinterest.Handler.HandlePagination;
using Project_Pinterest.Payloads.Converters;
using Project_Pinterest.Payloads.DataRequests.PostRequests;
using Project_Pinterest.Payloads.DataResponses.DataPost;
using Project_Pinterest.Payloads.Responses;
using Project_Pinterest.Services.Interfaces;
using System.Net;

namespace Project_Pinterest.Services.Implements
{
    public class PostService : IPostService
    {
        public WebClient webClient = new WebClient();
        private readonly AppDbContext _context;
        private readonly ResponseObject<DataResponsePost> _responseObject;
        private readonly ResponseObject<DataResponseComment> _responeObjectComment;
        private readonly CommentConverter _commentConverter;
        private readonly PostConverter _converter;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostService(AppDbContext context, ResponseObject<DataResponsePost> responseObject, PostConverter converter, IHttpContextAccessor httpContextAccessor, ResponseObject<DataResponseComment> responseObjectComment, CommentConverter commentConverter)
        {
            _context = context;
            _responseObject = responseObject;
            _converter = converter;
            _httpContextAccessor = httpContextAccessor;
            _commentConverter = commentConverter;
            _responeObjectComment = responseObjectComment;
        }

        public async Task<ResponseObject<DataResponseComment>> CreateComment(int userId, Request_CreateComment request)
        {
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == request.PostId && x.IsDeleted == false && x.IsActive == true);
            if(post is null)
            {
                return _responeObjectComment.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài viết", null);
            }
            var userComment = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsActive == true && x.IsLocked == false);
            UserCommentPost comment = new UserCommentPost
            {
                CreateAt = DateTime.Now,
                IsActive = true,
                Content = request.Comment,
                IsDeleted = false,
                NumberOfLikes = 0,
                PostId = request.PostId,
                UserId = userId,
            };
            await _context.userCommentPosts.AddAsync(comment);
            await _context.SaveChangesAsync();
            post.NumberOfComments = _context.userCommentPosts.Where(x => x.PostId == post.Id && x.IsDeleted == false).Count(x => x.PostId == post.Id);
            _context.posts.Update(post);
            await _context.SaveChangesAsync();
            return _responeObjectComment.ResponseSuccess("Tạo bình luận bài viết thành công", _commentConverter.EntityToDTO(comment)); 
        }

        public async Task<ResponseObject<DataResponsePost>> CreatePost(int userId, Request_CreatePost request)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsActive == true && x.IsLocked == false);
            var uploadImageTask = HandleUploadImage.Upfile(request.ImageUrl);
            Post post = new Post
            {
                CreateAt = DateTime.Now,
                IsActive = true,
                Description = request.Description,
                ImageUrl = "",
                IsDeleted = false,
                NumberOfComments = 0,
                NumberOfLikes = 0,
                PostStatusId = 1,
                Title = request.Title,
                UserId = userId
            };
            await _context.posts.AddAsync(post);
            await _context.SaveChangesAsync();
            try
            {
                post.ImageUrl = await uploadImageTask;
                await _context.SaveChangesAsync();
            }catch(TaskCanceledException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            return _responseObject.ResponseSuccess("Thêm ảnh thành công", _converter.EntityToDTO(post));
        }

        public async Task<string> DeleteComment(int userId, Request_DeleteComment request)
        {
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == request.PostId && x.IsDeleted == false && x.IsActive == true);
            if(post is null)
            {
                return "Không tìm thấy bài viết";
            }
            var comment = await _context.userCommentPosts.SingleOrDefaultAsync(x => x.Id == request.CommentId);
            if(comment == null)
            {
                return "Không tìm thấy bình luận này";
            }
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsLocked == false && x.IsActive == true);
            comment.IsDeleted = true;
            comment.IsActive = false;
            comment.RemoveAt = DateTime.Now;
            _context.userCommentPosts.Update(comment);
            await _context.SaveChangesAsync();
            post.NumberOfComments = _context.userCommentPosts.Where(x => x.PostId == post.Id && x.IsDeleted == false).Count(x => x.PostId == post.Id);
            _context.posts.Update(post);
            await _context.SaveChangesAsync();
            return "Xóa bình luận thành công";
        }

        public async Task<string> DeletePost(int userId, int postId)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsActive == true && x.IsLocked == false);
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == postId && x.IsDeleted == false && x.IsActive == true);
            var currentUser = _httpContextAccessor.HttpContext.User;

            if (!currentUser.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("Người dùng không được xác thực hoặc không được xác định");
            }

            if (!currentUser.IsInRole("Admin") || post.UserId != userId)
            {
                throw new UnauthorizedAccessException("Người dùng không có quyền sử dụng chức năng này");
            }
            if (post is null)
            {
                return "Bài đăng không tồn tại";
            }
            post.IsActive = false;
            post.RemoveAt = DateTime.Now;
            post.IsDeleted = true;
            _context.posts.Update(post);
            await _context.SaveChangesAsync();
            return "Xóa thành công!!!!!!";
        }

        public async Task<PageResult<DataResponsePost>> GetAllPost(int pageSize, int pageNumber)
        {
            var posts =  _context.posts
                                      .Include(x => x.UserLikePosts)
                                      .Include(x => x.UserCommentPosts)
                                      .AsNoTracking()
                                      .Where(x => x.IsActive == true && x.IsDeleted == false)
                                      .Select(post => _converter.EntityToDTO(post));


            var result = Pagination.GetPagedData(posts, pageSize, pageNumber);
            return result;
        }


        public async Task<PageResult<DataResponseComment>> GetCommentByUser(int userId, int pageSize, int pageNumber)
        {
            var userComments = await _context.userCommentPosts
                                             .Include(x => x.UserLikeCommentOfPosts)
                                             .AsNoTracking()
                                             .Where(x => x.UserId == userId && x.IsDeleted == false && x.IsActive == true)
                                             .ToListAsync();

            var commentDTOsTasks = userComments.Select( comment =>  _commentConverter.EntityToDTO(comment));

            var result = Pagination.GetPagedData(commentDTOsTasks.AsQueryable(), pageSize, pageNumber);
            return result;
        }


        public async Task<ResponseObject<DataResponsePost>> GetPostById(int postId)
        {
            var post = await _context.posts.Include(x => x.UserLikePosts).Include(x => x.UserCommentPosts).AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId && x.IsDeleted == false && x.IsActive == true);
            if(post == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài viết", null);
            }
            return _responseObject.ResponseSuccess("Lấy thông tin bài viết thành công", _converter.EntityToDTO(post));
        }

        public async Task<PageResult<DataResponsePost>> GetPostByUser(int userId, int pageSize, int pageNumber)
        {
            var userPosts = await _context.posts
                                          .Include(x => x.User)
                                          .Include(x => x.UserLikePosts)
                                          .Include(x => x.UserCommentPosts)
                                          .AsNoTracking()
                                          .Where(x => x.UserId == userId && x.IsDeleted == false && x.IsActive == true && x.User.IsActive == true)
                                          .ToListAsync();
            var postDTOsTasks = userPosts.Select(async post => _converter.EntityToDTO(post));
            var postDTOs = await Task.WhenAll(postDTOsTasks);

            var result = Pagination.GetPagedData(postDTOs.AsQueryable(), pageSize, pageNumber);
            return result;
        }


        public async Task<string> LikePost(int userId, Request_UserLikePost request)
        {
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == request.PostId && x.IsDeleted == false && x.IsActive == true);
            if(post == null)
            {
                return "Không tìm thấy bài viết";
            }
            var userLike = await _context.userLikePosts.SingleOrDefaultAsync(x => x.PostId == post.Id && x.UserId == userId);
            if(userLike != null)
            {
                _context.userLikePosts.Remove(userLike);
                _context.SaveChanges();
            }
            else
            {
                var userLikePost = new UserLikePost
                {
                    LikeTime = DateTime.Now,
                    PostId = post.Id,
                    Unlike = false,
                    UserId = userId
                };
                await _context.userLikePosts.AddAsync(userLikePost);
                await _context.SaveChangesAsync();
            }
            post.NumberOfLikes = _context.userLikePosts.Count(x => x.PostId == post.Id);
            _context.posts.Update(post);
            await _context.SaveChangesAsync();
            return "Đã like bài viết";
        }

        public async Task<ResponseObject<DataResponseComment>> UpdateComment(int commentId, int userId, Request_UpdateComment request)
        {
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == request.PostId && x.IsDeleted == false && x.IsActive == true);
            if(post is null)
            {
                return _responeObjectComment.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài viết", null);
            }
            var comment = await _context.userCommentPosts.SingleOrDefaultAsync(x => x.Id == commentId && x.IsDeleted == false && x.IsActive == true);
            if(comment == null)
            {
                return _responeObjectComment.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bình luận", null);
            }
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsActive == true && x.IsLocked == false);
            comment.UpdateAt = DateTime.Now;
            comment.PostId = request.PostId;
            comment.Content = request.Comment;
            _context.userCommentPosts.Update(comment);
            await _context.SaveChangesAsync();
            return _responeObjectComment.ResponseSuccess("Sửa bình luận thành công", _commentConverter.EntityToDTO(comment));

        }

        public async Task<ResponseObject<DataResponsePost>> UpdatePost(int userId, Request_UpdatePost request)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsLocked == false && x.IsActive == true);
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == request.PostId);
            if(post is null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Không tìm thấy bài đăng", null);
            }
            post.ImageUrl = await HandleUploadImage.UpdateFile(post.ImageUrl, request.ImageUrl);
            post.Title = request.Title;
            post.Description = request.Description;
            post.UpdateAt = DateTime.Now;
            _context.posts.Update(post);
            await _context.SaveChangesAsync();
            return _responseObject.ResponseSuccess("Cập nhậ thông tin bài đăng thành công", _converter.EntityToDTO(post));
        }

        public async Task<string> UserLikeComment(int userId, int commentId)
        {
            var comment = await _context.userCommentPosts.SingleOrDefaultAsync(x => x.Id == commentId && x.IsDeleted == false && x.IsActive == true);
            if(comment is null)
            {
                return "Không tìm thấy bài viết";
            }
            var userLike = await _context.userLikeCommentOfPosts.SingleOrDefaultAsync(x => x.UserId == userId && x.UserCommentPostId == comment.Id);
            if(userLike != null)
            {
                _context.userLikeCommentOfPosts.Remove(userLike);
                _context.SaveChanges();
            }
            else
            {
                var userLikeComment = new UserLikeCommentOfPost
                {
                    LikeTime = DateTime.Now,
                    Unlike = false,
                    UserId = userId,
                    UserCommentPostId = comment.Id,
                };
                await _context.userLikeCommentOfPosts.AddAsync(userLikeComment);
                await _context.SaveChangesAsync();
            }
            comment.NumberOfLikes = _context.userLikeCommentOfPosts.Count(x => x.UserCommentPostId == comment.Id);
            _context.userCommentPosts.Update(comment);
            await _context.SaveChangesAsync();
            return "Like bình luận thành công";
        }
        public async Task<ResponseObject<DataResponsePost>> SharePost(int userId, int postId)
        {
            var user = await _context.users.SingleOrDefaultAsync(x => x.Id == userId && x.IsActive == true && x.IsLocked == false);
            var originalPost = await _context.posts.SingleOrDefaultAsync(x => x.Id == postId && x.IsActive == true && x.IsDeleted == false);

            if (user == null || originalPost == null)
            {
                return _responseObject.ResponseError(StatusCodes.Status404NotFound, "Người dùng hoặc bài đăng không tồn tại", null);
            }

            Post sharedPost = new Post
            {
                CreateAt = DateTime.Now,
                IsActive = true,
                Description = originalPost.Description,
                ImageUrl = originalPost.ImageUrl,
                IsDeleted = false,
                NumberOfComments = 0,
                NumberOfLikes = 0,
                PostStatusId = 1,
                Title = originalPost.Title,
                UserId = userId
            };

            await _context.posts.AddAsync(sharedPost);
            await _context.SaveChangesAsync();

            return _responseObject.ResponseSuccess("Chia sẻ bài đăng thành công", _converter.EntityToDTO(sharedPost));
        }
        public async Task<string> DownloadImageForPost(int postId, string saveDirectory)
        {
            var post = await _context.posts.SingleOrDefaultAsync(x => x.Id == postId);
            if (post == null || string.IsNullOrEmpty(post.ImageUrl))
            {
                return "Không tìm thấy bài đăng hoặc không tìm thấy link ảnh.";
            }

            string fileName = Path.GetFileName(post.ImageUrl);
            string localPath = Path.Combine(saveDirectory, fileName);

            try
            {
                using (WebClient client = new WebClient())
                {
                    await client.DownloadFileTaskAsync(new Uri(post.ImageUrl), localPath);
                }
                return $"Download ảnh thành công";
            }
            catch (Exception ex)
            {
                return $"Đã xảy ra lỗi khi download ảnh: {ex.Message}";
            }
        }

        public async Task<PageResult<DataResponseComment>> GetCommentByPost(int postId, int pageSize, int pageNumber)
        {
            var query = _context.userCommentPosts.Include(x => x.UserLikeCommentOfPosts).AsNoTracking().Where(x => x.PostId == postId).Select(x => _commentConverter.EntityToDTO(x));
            var result = Pagination.GetPagedData(query, pageSize, pageNumber);
            return result;
        }

        public async Task<PageResult<DataResponsePost>> GetPostByTitle(string title, int pageSize, int pageNumber)
        {
            var query = _context.posts.Include(x => x.User).Include(x => x.UserCommentPosts).Include(x => x.UserLikePosts).AsNoTracking().Where(x => x.Title.Trim().ToLower().Contains(title.Trim().ToLower())).Select(x => _converter.EntityToDTO(x));
            var result = Pagination.GetPagedData(query, pageSize, pageNumber);
            return result;
        }
    }
}
