using ASMS.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Library.Models
{
    public class LoginResponse : ResponseBase
    {
        public string? Token { get; set; }
        public LoginResponse(bool isSuccess, string? responseText = null, string? errorText = null, string? token = null)
        {
            IsSuccess = isSuccess;
            ResponseText = responseText;
            ErrorText = errorText;
            Token = token;
        }
    }
}
