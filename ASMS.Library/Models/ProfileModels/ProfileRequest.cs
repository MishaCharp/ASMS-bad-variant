using ASMS.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Library.Models.ProfileModels
{
    public class ProfileRequest : RequestBase
    {
        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public override bool IsCorrectRequest
        {
            get
            {
                ErrorText = "";
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
                if (Birthday.Year <= 1900)
                {
                    AddErrorText("Введите настоящую дату рождения");
                    result = false;
                }

                return result;
            }
        }
    }
}
