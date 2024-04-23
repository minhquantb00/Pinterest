namespace Project_Pinterest.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? RemoveAt { get; set; }
        public int UserId { get; set; }
        public int? NumberOfLikes { get; set; }
        public int? NumberOfComments { get; set; }
        public int PostStatusId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsActive { get; set; } = true;
        public virtual User? User { get; set; }
        public virtual PostStatus? PostStatus { get; set; }
        public virtual ICollection<UserLikePost>? UserLikePosts { get; set; }
        public virtual ICollection<UserCommentPost>? UserCommentPosts { get; set; }
    }
}
