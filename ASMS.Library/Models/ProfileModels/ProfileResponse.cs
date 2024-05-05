using ASMS.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Library.Models.ProfileModels
{
    public class ProfileResponse : ResponseBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? Birthday { get; set; }
        public ProfileResponse(bool isSuccess, string? responseText = null, string? errorText = null, string login = null, string password = null, DateTime? birthday = null)
        {
            IsSuccess = isSuccess;
            Login = login;
            Password = password;
            Birthday = birthday;
            ErrorText = errorText;
            ResponseText = responseText;
        }
    }
}
