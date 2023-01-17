using Sat.Recruitment.Business.BusinessModels;
using Sat.Recruitment.Business.Enums;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Models;
using Sat.Recruitment.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Sat.Recruitment.Business.Implementations
{
    public class UserBusiness : IUserBusiness
    {
		/// <summary>
		/// Add Gif To Money
		/// </summary>
		/// <param name="userType"></param>
		/// <param name="money"></param>
		/// <returns></returns>
		public decimal AddGifToMoney(string userType, decimal? money)
		{
			decimal? ret = money;
			try
			{
				if (userType.ToLower().Trim() == UserTypeEnum.Normal.GetStringValue().ToLower())
				{
					ret = AddPercentageToAmountNormalUserType(money);
				}
				if (userType.ToLower().Trim() == UserTypeEnum.SuperUser.GetStringValue().ToLower())
				{
					ret = AddPercentageToAmountSuperUserUserType(money);
				}
				if (userType.ToLower().Trim() == UserTypeEnum.Premium.GetStringValue().ToLower())
				{
					ret = AddPercentageToAmountPremiumUserType(money);
				}
			}
			catch (Exception)
			{

				throw;
			}
			return ret ?? 0;
		}

		public UserResponse CreateUser(User newUser)
        {
			var  ret = new UserResponse();
            try
            {
				
				if (IsDuplicatedNewUser(newUser))
				{

					
					ret.Messages = "The user is duplicated";
					ret.IsSuccess = false;
					

				}
				else
				{
					ret.User = new User();
					ret.User = newUser;

					ret.User.Money = AddGifToMoney(newUser.UserType, newUser.Money);
					ret.Messages= "User Created";
					ret.IsSuccess = true;

					
				}
			}
            catch (Exception)
            {

                throw;
            }
			return ret;
        }


		/// <summary>
		/// Chech if is Duplicated a New User
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public bool IsDuplicatedNewUser(User user)
		{
			bool ret = false;
			try
			{
				var lstUsersFromFile = UsersFromFile();
				if (IsDuplicated(user, lstUsersFromFile))
				{
					ret = true;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return ret;
		}

		private List<User> UsersFromFile()
		{
			List<User> lstRet = new List<User>();
			try
			{
				var reader = ReadUsersFromFile();
				if (reader != null)
				{
					while (reader.Peek() >= 0)
					{
						var line = reader.ReadLineAsync().Result;
						string[] arrLine = line.Split(',');
						if (arrLine != null)
						{
							var user = new User
							{
								Name = arrLine[0],
								Email = arrLine[1],
								Phone = arrLine[2],
								Address = arrLine[3],
								UserType = arrLine[4],
								Money = decimal.Parse(arrLine[5]),
							};
							lstRet.Add(user);
						}
					}
					reader.Close();
				}
			}
			catch (Exception ex)
			{

				throw;
			}
			return lstRet;
		}


		private StreamReader ReadUsersFromFile()
		{
			StreamReader reader = null;
			try
			{
				var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

				if (path != null)
				{
					FileStream fileStream = new FileStream(path, FileMode.Open);

					reader = new StreamReader(fileStream);


				}
				return reader;
			}
			catch (Exception)
			{

				throw;
			}

		}

		private string NormalizeEmail(User user)
		{

			string ret = string.Empty;
			try
			{
				if (user != null)
				{
					ret = user.Email;

					string pattern = @"\+[^@]*"; //remove +
					
					string replacement = "";

					var newEmail = Regex.Replace(ret, pattern, replacement);

					pattern = @"\.(?=[^\s@]*@)"; //remove . 

					newEmail= Regex.Replace(newEmail, pattern, replacement);

					ret = newEmail;

				}
			}
			catch (Exception)
			{

				throw;
			}
			return ret;
		}

		private bool IsDuplicated(User user, List<User> lstUsers)
		{
			bool isDuplicated = false;
			try
			{
				if (user != null && lstUsers.Count > 0)
				{
					user.Email = NormalizeEmail(user);

					var lst = lstUsers.Where(x => x.Email.ToLower().Trim() == user.Email.ToLower().Trim() || x.Phone.Trim() == user.Phone.Trim()).ToList();
					if (lst.Count > 0)
					{
						isDuplicated = true;
					}
					else
					{
						lst = lstUsers.Where(x => x.Name.ToLower().Trim() == user.Name.ToLower().Trim() && x.Address.ToLower().Trim() == user.Address.ToLower().Trim()).ToList();

						if (lst.Count > 0)
						{
							isDuplicated = true;
						}
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
			return isDuplicated;
		}

		/// <summary>
		/// Manual Mapper DTO to Entity
		/// </summary>
		/// <param name="userDTO"></param>
		/// <returns></returns>
		public User ManualMapper(UserDTO userDTO)
		{
			User user = new User();
			try
			{
				if (userDTO != null)
				{
					user.Name = userDTO.Name;
					user.Phone = userDTO.Phone;
					user.UserType = userDTO.UserType;
					user.Address = userDTO.Address;
					user.Email = userDTO.Email;
					user.Money = userDTO.Money;
				}
			}
			catch (Exception)
			{

				throw;
			}
			return user;
		}


		private decimal AddPercentageToAmountNormalUserType(decimal? money)
		{
			decimal? ret = money;
			try
			{
				if (money > 100)
				{
					ret = Calculation.AddPercentageToAmount(money ?? 0, 0.12);
				}
				if (money < 100)
				{
					if (money > 10)
					{
						ret = Calculation.AddPercentageToAmount(money ?? 0, 0.8);
					}
				}
			}
			catch (Exception)
			{

				throw;
			}
			return ret ?? 0;
		}


		private decimal AddPercentageToAmountSuperUserUserType(decimal? money)
		{
			decimal? ret = money;
			try
			{
				if (money > 100)
				{
					ret = Calculation.AddPercentageToAmount(money ?? 0, 0.20);
				}
			}
			catch (Exception)
			{

				throw;
			}
			return ret ?? 0;
		}

		private decimal AddPercentageToAmountPremiumUserType(decimal? money)
		{
			decimal? ret = money;
			try
			{
				if (money > 100)
				{
					ret = Calculation.AddPercentageToAmount(money ?? 0, 2);
				}
			}
			catch (Exception)
			{

				throw;
			}
			return ret ?? 0;
		}

		/// <summary>
		/// Check If the email is valid format
		/// </summary>
		/// <param name="emailaddress"></param>
		/// <returns></returns>
		public bool IsValidEmail(string emailaddress)
		{
			try
			{
				MailAddress m = new MailAddress(emailaddress);

				return true;
			}
			catch (FormatException)
			{
				return false;
			}
		}

		public ResultDTO ReturnResult(string messages, bool isSuccess)
		{
			ResultDTO result = new ResultDTO();
			try
			{
				result.IsSuccess = isSuccess;
				result.Messages = messages;
			}
			catch (Exception)
			{

				throw;
			}
			return result;
		}

	}
}
