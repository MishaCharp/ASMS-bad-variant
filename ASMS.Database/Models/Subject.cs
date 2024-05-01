using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    /// <summary>
    /// Класс сущности "Предмет"
    /// </summary>
    public class Subject : BaseEntity
    {
        /// <summary>
        /// Название предмета
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание предмета
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Какие уроки ссылаются на этот предмет
        /// </summary>
        public List<Lesson> Lessons { get; set; }
        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is Subject)) return;

            Subject subject = (Subject)entity;
            Name = subject.Name;
            Description = subject.Description;
        }
    }
}
