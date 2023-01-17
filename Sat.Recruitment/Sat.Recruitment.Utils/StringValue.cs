using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Utils
{
	public class StringValue : Attribute
	{
		public string Value { get; private set; }

		public StringValue(string value)
		{
			Value = value;
		}
	}
}
