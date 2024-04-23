namespace Project_Pinterest.Entities
{
    public class UserCommentPost : BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public int? NumberOfLikes { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public DateTime? RemoveAt { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public bool? IsActive { get; set; } = true;
        public virtual Post? Post { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<UserLikeCommentOfPost>? UserLikeCommentOfPosts { get; set; }
    }
}
