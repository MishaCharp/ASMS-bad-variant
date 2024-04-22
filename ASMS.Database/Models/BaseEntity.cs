using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Database.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public abstract void Update(BaseEntity entity);
    }
}
