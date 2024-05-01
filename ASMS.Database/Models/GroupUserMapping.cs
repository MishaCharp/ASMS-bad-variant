using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    /// <summary>
    /// Сущность "ГруппаПользователь" - подчеркивает ассоциацию между группой и пользователем
    /// </summary>
    public class GroupUserMapping : BaseEntity
    {
        /// <summary>
        /// Привязанная сущность группы, который присутсвует в данной ассоциации
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// ID сущности группы, который присутсвует в данной ассоциации
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// Привязанная сущность пользователя, который присутсвует в данной ассоциации (только User с Role.Name = "Student")
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// ID сущности пользователя, который присутсвует в данной ассоциации (только User с Role.Name = "Student")
        /// </summary>
        public int UserId { get; set; }

        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is GroupUserMapping)) return;

            GroupUserMapping gum = (GroupUserMapping)entity;
            Group = gum.Group;
            GroupId = gum.GroupId;
            User = gum.User;
            UserId = gum.UserId;
        }
    }
}
