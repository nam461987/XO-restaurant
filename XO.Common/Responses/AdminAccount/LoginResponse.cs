using XO.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace XO.Common.Responses.AdminAccount
{
    public class LoginResponse
    {
        public EResponseStatus Status { get; set; }

        public string AccessToken { get; set; }
        public string Displayname { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public int Type { get; set; }
        public string[] Permissions { get; set; }
    }
}
