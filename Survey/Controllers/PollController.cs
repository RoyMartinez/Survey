using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Collections.Generic;

namespace Poll.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PollController : Controller
    {
        private readonly IPollService _Poll;
        public PollController
        (
            IPollService Poll
        )
        {
            _Poll = Poll;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{Id}")]
        /// <summary>
        /// Get all the surveys
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public IActionResult GetQuestions([FromRoute] int Id)
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
                var response = _Poll.GetQuestions(Id);

                if (response == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant retrieve the survey please try again";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Get survey succesfully";
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

        [HttpPost]
        [AllowAnonymous]
        [Route("{Id}")]
        /// <summary>
        /// Set the response of the surveys
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public IActionResult SetAnswer([FromRoute] int Id,[FromBody] List<AnswerRequestDto> Answers)
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
                var response = _Poll.SetAnswers(Id, Answers);

                if (response == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant retrieve the survey please try again";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Answers saved succesfully the code is in the response";
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


        [HttpGet]
        [Authorize]
        [Route("Answers")]
        /// <summary>
        /// Get all the answer
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public IActionResult GetAllAnswer()
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
                var response = _Poll.GetAllAnswers();

                if (response == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant retrieve the answers please try again";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Get answers succesfully";
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


        [HttpGet]
        [Authorize]
        [Route("Answers/{Code}")]
        /// <summary>
        /// Get the answer of a document
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public IActionResult GetAnswerByCode([FromRoute]string Code)
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
                var response = _Poll.GetAnswerByCode(Code);

                if (response == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant retrieve the answers please try again";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Get answers succesfully";
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
