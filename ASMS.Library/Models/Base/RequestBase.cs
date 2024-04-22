using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ASMS.Library.Models.Base
{
    public abstract class RequestBase
    {
        public abstract bool IsCorrectRequest { get; }
        internal string? ErrorText { get; set; }
        public void AddErrorText(string errorText) => ErrorText += errorText + '\n';
    }
}
