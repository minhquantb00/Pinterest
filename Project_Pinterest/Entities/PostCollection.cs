namespace Project_Pinterest.Entities
{
    public class PostCollection : BaseEntity
    {
        public int PostId { get; set; }
        public int CollectionId { get; set; }
        public virtual Post? Post { get; set; }
        public virtual Collection? Collection { get; set; }
    }
}
