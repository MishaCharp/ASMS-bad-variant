using ASMS.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Library.Models
{
    public class LoginRequest : RequestBase
    {
        public string? Login { get; set; }
        public string? Password { get; set; }

        public override bool IsCorrectRequest
        {
            get
            {
                bool result = true;
                if (string.IsNullOrEmpty(Login))
                {
                    AddErrorText("Логин не может быть пустым");
                    result = false;
                }
                if (string.IsNullOrEmpty(Password))
                {
                    AddErrorText("Пароль не может быть пустым");
                    result = false;
                }

                return result;
            }
        }
    }
}
