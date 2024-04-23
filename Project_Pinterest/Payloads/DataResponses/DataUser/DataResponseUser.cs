namespace Project_Pinterest.Payloads.DataResponses.DataUser
{
    public class DataResponseUser : DataResponseBase
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string UserStatusName { get; set; }
    }
}
