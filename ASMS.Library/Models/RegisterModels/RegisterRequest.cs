using ASMS.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Library.Models.RegisterModels
{
    public class RegisterRequest : RequestBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Birthday { get; set; }
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
                if (string.IsNullOrEmpty(ConfirmPassword))
                {
                    AddErrorText("Повтор пароля не может быть пустым");
                    result = false;
                }
                if (!DateOnly.TryParse(Birthday, out DateOnly date) || date.Year < 1900)
                {
                    AddErrorText("Заполните дату рождения");
                    result = false;
                }
                if (Password != ConfirmPassword)
                {
                    AddErrorText("Пароли не совпадают");
                    result = false;
                }

                return result;
            }
        }
        public string GetErrorText() => ErrorText ?? "";
        public DateTime GetBirthday() => DateTime.Parse(Birthday);
    }
}
