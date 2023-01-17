using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sat.Recruitment.Models
{
    public class UserDTO
    {
		[Display(Name = "Name")]
		//[StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 3)]
		//[Required(ErrorMessage = "Name is required.")]
		public string Name { get; set; }

		[Display(Name = "Email")]
		//[StringLength(256, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 5)]
		//[Required(ErrorMessage = "Email is required.")]
		//[RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|([\w-]+\.)+[a-zA-Z]{2,5})$", ErrorMessage = "Email Is Not Valid")]

		public string Email { get; set; }

		[Display(Name = "Address")]
		//[StringLength(50, ErrorMessage = "{0} must be at least {2} characters long.", MinimumLength = 3)]
		//[Required(ErrorMessage = "Address is required.")]
		public string Address { get; set; }


		//[StringLength(15, ErrorMessage = "{0} must be at least {2} characters long and max {1} characters long.", MinimumLength = 10)]
		//[Required(ErrorMessage = "Phone is required.")]
		public string Phone { get; set; }

		[Display(Name = "UserType")]
		//[StringLength(15, ErrorMessage = "{0} must be at least {2} characters long and max {1} characters long.", MinimumLength = 6)]
		//[Required(ErrorMessage = "User Type is required.")]
		public string UserType { get; set; }

		[Display(Name = "Money")]
		public decimal? Money { get; set; }
	}
}
