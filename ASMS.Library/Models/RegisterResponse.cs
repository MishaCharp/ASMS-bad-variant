using ASMS.Library.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASMS.Library.Models
{
    public class RegisterResponse : ResponseBase
    {
        public RegisterResponse(bool isSuccess, string? responseText = null, string? errorText = null)
        {
            IsSuccess = isSuccess;
            ResponseText = responseText;
            ErrorText = errorText;
        }
    }
}
