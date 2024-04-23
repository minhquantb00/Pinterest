namespace Project_Pinterest.Entities
{
    public class UserLikePost : BaseEntity
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime LikeTime { get; set; }
        public bool? Unlike { get; set; }
        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}
