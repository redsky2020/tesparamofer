using Sat.Recruitment.Business.BusinessModels;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sat.Recruitment.Business.Interfaces
{
	public interface IUserBusiness
	{
		decimal AddGifToMoney(string userType, decimal? money);

		bool IsDuplicatedNewUser(User user);

		User ManualMapper(UserDTO userDTO);

		ResultDTO ReturnResult(string messages, bool isSuccess);

		UserResponse CreateUser(User newUser);

		bool IsValidEmail(string emailaddress);



	}
}
