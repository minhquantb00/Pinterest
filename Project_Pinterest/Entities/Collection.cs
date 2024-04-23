namespace Project_Pinterest.Entities
{
    public class Collection : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PostCollection>? PostCollections { get; set; }
    }
}
