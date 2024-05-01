using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    /// <summary>
    /// Класс сущности "Урок"
    /// </summary>
    public class Lesson : BaseEntity
    {
        /// <summary>
        /// Дата проведения пары
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Номер пары по счёту
        /// </summary>
        public int OrderOfPair { get; set; }
        /// <summary>
        /// Привязанная сущность группы, к которому привязан урок
        /// </summary>
        public Group Group { get; set; }
        /// <summary>
        /// ID сущности группы, к которому привязан урок
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// Привязанная сущность предмета, к которому привязан урок
        /// </summary>
        public Subject Subject { get; set; }
        /// <summary>
        /// ID сущности предмета, к которому привязан урок
        /// </summary>
        public int SubjectId { get; set; }
        /// <summary>
        /// Тема урока
        /// </summary>
        public string? ThemeOfLesson { get; set; }
        /// <summary>
        /// Описание пройденного на уроке
        /// </summary>
        public string? DescriptionOfLesson { get; set; }

        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is Lesson)) return;

            Lesson lesson = (Lesson)entity;
            Date = lesson.Date;
            OrderOfPair = lesson.OrderOfPair;
            Group = lesson.Group;
            GroupId = lesson.GroupId;
            Subject = lesson.Subject;
            SubjectId = lesson.SubjectId;
            ThemeOfLesson = lesson.ThemeOfLesson;
            DescriptionOfLesson = lesson.DescriptionOfLesson;
        }
    }
}
