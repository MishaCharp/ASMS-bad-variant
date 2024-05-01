using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    /// <summary>
    /// Класс сущности "Отделение"
    /// </summary>
    public class Department : BaseEntity
    {
        /// <summary>
        /// Название отделения
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Какие группы ссылаются на это отделение
        /// </summary>
        public List<Group> Groups { get; set; }

        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is Department)) return;

            Department department = (Department)entity;
            Name = department.Name;
        }
    }
}
