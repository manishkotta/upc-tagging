using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Entities
{
    public class AuthTokenDTO
    {
        public string AuthToken { get; set; }

        public string RoleName { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
