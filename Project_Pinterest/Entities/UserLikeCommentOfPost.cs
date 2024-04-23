namespace Project_Pinterest.Entities
{
    public class UserLikeCommentOfPost : BaseEntity
    {
        public int UserId { get; set; }
        public int UserCommentPostId { get; set; }
        public DateTime LikeTime { get; set; }
        public bool? Unlike { get; set; }
        public virtual User? User { get; set; }
        public virtual UserCommentPost? UserCommentPost { get; set; }
    }
}
