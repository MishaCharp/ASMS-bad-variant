using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Абстрактный метод обновления сущности
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Update(BaseEntity entity);
    }
}
