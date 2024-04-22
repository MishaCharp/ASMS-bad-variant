namespace ASMS.Database.Models
{
    public class User : BaseEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateOnly Birthday { get; set; }
        public string? RefreshToken { get; set; }
        public Role Role { get; set; }

        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is User)) return;

            User user = (User)entity;
            Login = user.Login;
            Password = user.Password;
            Birthday = user.Birthday;
            Role = user.Role;
            RefreshToken = user.RefreshToken;
        }
    }
}
