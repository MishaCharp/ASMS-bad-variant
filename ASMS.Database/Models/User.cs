namespace ASMS.Database.Models
{
    /// <summary>
    /// Класс сущности "Пользователь"
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль пользователя
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Дата рождения пользователя
        /// </summary>
        public DateTime Birthday { get; set; }
        public string? RefreshToken { get; set; }
        /// <summary>
        /// Привязанная сущность роли, к которой привязан пользователь
        /// </summary>
        public Role Role { get; set; }
        /// <summary>
        /// ID сущности роли, к которой привязан пользователь
        /// </summary>
        public int RoleId { get; set; }

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
