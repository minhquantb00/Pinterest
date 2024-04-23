namespace Project_Pinterest.Entities
{
    public class PostStatus : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Post>? Posts { get; set;}
    }
}
