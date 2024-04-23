namespace Project_Pinterest.Entities
{
    public class Role : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
