using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Business.Implementations;
using Sat.Recruitment.Business.Interfaces;
using Sat.Recruitment.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserTesting
    {
        private readonly UserController _userController;
        private readonly IUserBusiness _userService;


        public UserTesting()
        {
            _userService = new UserBusiness();
            _userController = new UserController(_userService);

        }
        [Fact]
        public async Task CreateUser_OkAsync()
        {
            var dto = new UserDTO();
            dto.Name = "Luis";
            dto.Email = "luis@marmol.com";
            dto.Phone = "+5491154762317";
            dto.Address = "Peru 24646";
            dto.UserType = "Normal";
            dto.Money = 1234;

            var result = await _userController.CreateUser(dto);
            var okResult = (IStatusCodeActionResult)result;
            
            Assert.Equal(200,okResult.StatusCode);
        }

        [Fact]
        public async Task CreateUser_UserCreatedAsync()
        {
            var dto = new UserDTO();
            dto.Name = "Luis";
            dto.Email = "luis@marmol.com";
            dto.Phone = "+5491154762317";
            dto.Address = "Peru 24646";
            dto.UserType = "Normal";
            dto.Money = 1234;

            var result = await _userController.CreateUser(dto);
            var okResult = (OkObjectResult)result;

            var resultDto=Assert.IsType<ResultDTO>(okResult.Value);

            Assert.True(resultDto.IsSuccess);
            Assert.Equal("User Created", resultDto.Messages);


        }



        [Fact]
        public async Task CreateUser_UserDuplicatedAsync()
        {
            var dto = new UserDTO();
            dto.Name = "Agustina";
            dto.Email = "Agustina@gmail.com";
            dto.Phone = "+534645213542";
            dto.Address = "Garay y Otra Calle";
            dto.UserType = "SuperUser";
            dto.Money = 112234;

            var result = await _userController.CreateUser(dto);
            var badResult = (BadRequestObjectResult)result;

            var resultDto = Assert.IsType<ResultDTO>(badResult.Value);

            Assert.False(resultDto.IsSuccess);
            Assert.Equal("The user is duplicated", resultDto.Messages);
        }

        [Fact]
        public async Task CreateUser_BadRequestAsync()
        {
            var dto = new UserDTO();
            dto.Name = "Agustina";
            dto.Email = "Agustina@gmail.com";
            dto.Phone = "+534645213542";
            dto.Address = "Garay y Otra Calle";
            dto.UserType = "SuperUser";
            dto.Money = 112234;

            var result = await _userController.CreateUser(dto);
            var okResult = (IStatusCodeActionResult)result;

            Assert.Equal(400, okResult.StatusCode);
        }


    }
}
