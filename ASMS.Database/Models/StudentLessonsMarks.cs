using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    /// <summary>
    /// Сущность "СтудентУрокОценка" - подчеркивает ассоциацию между предметом, оценкой за него и студентом, кому оценка была поставлена
    /// </summary>
    public class StudentLessonsMarks : BaseEntity
    {
        /// <summary>
        /// Привязанная сущность пользователя(студента), который присутсвует в данной ассоциации (только User с Role.Name = "Student")
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// ID сущности пользователя, который присутсвует в данной ассоциации (только User с Role.Name = "Student")
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Привязанная сущность урока, за который ставится оценка
        /// </summary>
        public Lesson Lesson { get; set; }
        /// <summary>
        /// ID сущности урока
        /// </summary>
        public int LessonId { get; set; }
        /// <summary>
        /// Привязанная сущность оценки
        /// </summary>
        public Mark Mark { get; set; }
        /// <summary>
        /// ID сущности оценки
        /// </summary>
        public int MarkId { get; set; }

        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is StudentLessonsMarks)) return;

            StudentLessonsMarks slm = (StudentLessonsMarks)entity;
            User = slm.User;
            UserId = slm.UserId;
            Lesson = slm.Lesson;
            LessonId = slm.LessonId;
            Mark = slm.Mark;
            MarkId = slm.MarkId;
        }
    }
}
