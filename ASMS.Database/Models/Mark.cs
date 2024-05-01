using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    /// <summary>
    /// Класс сущности "Оценка"
    /// </summary>
    public class Mark : BaseEntity
    {
        /// <summary>
        /// Название оценки (1,2,3,4,5,Н)
        /// </summary>
        public string MarkText { get; set; }
        /// <summary>
        /// Расшифровка оценки
        /// </summary>
        public string DecodingMark { get; set; }

        public override void Update(BaseEntity entity)
        {
            if (entity == null) return;
            if (!(entity is Mark)) return;

            Mark mark = (Mark)entity;
            MarkText = mark.MarkText;
            DecodingMark = mark.DecodingMark;
        }
    }
}
