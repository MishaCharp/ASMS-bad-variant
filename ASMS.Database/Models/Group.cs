using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    /// <summary>
    /// Класс сущности "Группа"
    /// </summary>
    public class Group : BaseEntity
    {
        /// <summary>
        /// Название группы
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Привязанная сущность отделения, к которому привязана группа
        /// </summary>
        public Department Department {  get; set; }
        /// <summary>
        /// ID сущности отделения, к которому привязана группа
        /// </summary>
        public int DepartmentId {  get; set; }

        /// <summary>
        /// Какие уроки ссылаются на эту группу
        /// </summary>
        public List<Lesson> Lessons { get; set; }
        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is Group)) return;

            Group group = (Group)entity;
            Name = group.Name;
            DepartmentId = group.DepartmentId;
            Department = group.Department;
        }
    }
}
