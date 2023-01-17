using Sat.Recruitment.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.BusinessModels
{
    public class UserResponse
    {
        public bool IsSuccess { get; set; }
        public string Messages { get; set; }

        public User User { get; set; }
    }
}
