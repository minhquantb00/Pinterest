namespace Project_Pinterest.Payloads.DataRequests.UserRequests
{
    public class Request_ChangePassword
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
