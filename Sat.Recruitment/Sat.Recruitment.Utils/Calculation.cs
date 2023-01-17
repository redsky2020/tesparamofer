using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Utils
{
    public static class Calculation
    {
		public static decimal AddPercentageToAmount(decimal amount, double percentage)
		{
			decimal ret = 0;
			try
			{
				var amountbypercentage = amount * Convert.ToDecimal(percentage);
				ret = amount + amountbypercentage;
			}
			catch (Exception)
			{

				throw;
			}
			return ret;
		}
	}
}
