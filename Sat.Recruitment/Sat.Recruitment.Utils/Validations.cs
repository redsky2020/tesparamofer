using Sat.Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Utils
{
	public class Validations
	{
		public static void ValidateErrors(UserDTO userDTO, ref string errors)
		{
			try
			{
				if (string.IsNullOrEmpty(userDTO.Name))
					//Validate if Name is null
					errors = "The name is required";
				if (string.IsNullOrEmpty(userDTO.Email))
					//Validate if Email is null
					errors = errors + " The email is required";
				if (string.IsNullOrEmpty(userDTO.Address))
					//Validate if Address is null
					errors = errors + " The address is required";
				if (string.IsNullOrEmpty(userDTO.Phone))
					//Validate if Phone is null
					errors = errors + " The phone is required";
			}
			catch (Exception)
			{

				throw;
			}

		}
	}
}
