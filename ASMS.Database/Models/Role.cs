namespace ASMS.Database.Models
{
    /// <summary>
    /// Класс сущности "Роль"
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Какие пользовати ссылаются на эту роль
        /// </summary>
        public List<User> Users { get; set; }
        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is Role)) return;

            Role role = (Role)entity;
            Name = role.Name;
        }
    }
}
