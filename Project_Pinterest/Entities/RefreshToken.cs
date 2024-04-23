namespace Project_Pinterest.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
