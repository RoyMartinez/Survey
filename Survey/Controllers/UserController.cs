using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;

namespace Survey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {

        private readonly IUserService _User;
        public UserController
        (
            IUserService User
        )
        {
            _User = User;
        }

        /// <summary>
        /// Login to the system
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login([FromBody] LoginRequestDto Request)
        {
            var responseDto = new ApiResponseDto();
            if (!ModelState.IsValid)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "The model is invalid";
                responseDto.Response = ModelState;
                return BadRequest(responseDto);
            }
            try
            {
                var response = _User.Login(Request);

                if (response == null || response.StartsWith("Error"))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Make sure the UserName and Password is correct";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Login is succeffuly";
                responseDto.Response = response;
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
                responseDto.Response = null;
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Register to the system
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] RegisterRequestDto Request)
        {
            var responseDto = new ApiResponseDto();
            if (!ModelState.IsValid) 
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "The model is invalid";
                responseDto.Response = ModelState;
                return BadRequest(responseDto);
            }
            try
            {
                var response = _User.Register(Request);

                if (response == null || response.StartsWith("Error"))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Some went wrong please Sign Up Again";
                    responseDto.Response = response;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Register is succeffuly";
                responseDto.Response = response;
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = ex.Message;
                responseDto.Response = null;
                return BadRequest(ex.Message);
            }
        }
    }
}
