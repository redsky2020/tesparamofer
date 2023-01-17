using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Business.BusinessModels;
using Sat.Recruitment.Business.Implementations;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Entities;
using Sat.Recruitment.Models;
using Sat.Recruitment.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly List<User> _users = new List<User>();
      
		private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {

           
			_userBusiness = userBusiness;
        }

		[HttpPost]
		[Route("create-user")]
		public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
		{
			
			string messages = string.Empty;
			

			var resultDto = new ResultDTO();

			try
			{

				//if (!ModelState.IsValid){ return BadRequest();} // I didn't use Model.IsValid because I guest that is necesary to use the method ValidateErrors() provide by this test

				Validations.ValidateErrors(userDto, ref messages);
                if (!string.IsNullOrEmpty(messages))
                {
                    return BadRequest(_userBusiness.ReturnResult(messages, false));
                }

                if (!_userBusiness.IsValidEmail(userDto.Email))
				{
					return BadRequest(_userBusiness.ReturnResult("Invalid Email", false));
				}

				Entities.User newUser = new User();
				
				newUser = _userBusiness.ManualMapper(userDto);

				var userResponse = new UserResponse();

				userResponse = _userBusiness.CreateUser(newUser);

				

				resultDto = _userBusiness.ReturnResult(userResponse.Messages, userResponse.IsSuccess);

				if (!userResponse.IsSuccess)
				{
					
					return BadRequest(resultDto);

				}
				else
				{
					return Ok(resultDto);
				}


			}
			catch(Exception ex)
            {
				
				return BadRequest(_userBusiness.ReturnResult(ex.Message, false));
			}
			

		}


	}
}
