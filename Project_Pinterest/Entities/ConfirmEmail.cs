namespace Project_Pinterest.Entities
{
    public class ConfirmEmail : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string ConfirmCode { get; set; }
        public bool Confirmed { get; set; } = false;
    }
}
