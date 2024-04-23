namespace Project_Pinterest.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public int? RoleId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string? AvatarUrl { get; set; }
        public bool? IsLocked { get; set; } = false;
        public int UserStatusId { get; set; }
        public bool? IsActive { get; set; }
        public virtual Role? Role { get; set; }
        public virtual UserStatus? UserStatus { get; set; }
        public virtual ICollection<Collection>? Collections { get; set; }
        public virtual ICollection<ConfirmEmail>? ConfirmEmails { get; set;}
        public virtual ICollection<Post>? Posts { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }
        public virtual ICollection<UserLikePost>? UserLikePosts { get; set; }
        public virtual ICollection<UserCommentPost>? UserCommentPosts { get; set; }
        public virtual ICollection<UserLikeCommentOfPost>? UserLikeCommentOfPosts { get;set; }
    }
}
