using Sat.Recruitment.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Enums
{
    public enum  UserTypeEnum
    {
		[StringValue("Normal")]
		Normal,
		[StringValue("SuperUser")]
		SuperUser,
		[StringValue("Premium")]
		Premium
	}
}
