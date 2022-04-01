using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using System;
using System.Threading.Tasks;

namespace Survey.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SurveyController : Controller
    {
        private readonly ISurveyService _Survey;
        public SurveyController
        (
            ISurveyService Survey
        )
        {
            _Survey = Survey;
        }

        [HttpGet]
        [Authorize]
        /// <summary>
        /// Get all the surveys
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public IActionResult GetAllSurveys()
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
                var response = _Survey.GetSurveys();

                if (response == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant retrieve the survey please try again";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Get surveys succesfully";
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
        [Route("ByName/{Name}")]
        /// <summary>
        /// Get the Surveys by Name
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSurveysByName([FromRoute]string Name)
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
                var response = _Survey.GetSurveysByName(Name);

                if (response == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant retrieve the survey please try again";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Get surveys succesfully";
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
        [Route("ById/{Id}")]
        /// <summary>
        /// Get the Surveys by Name
        /// </summary>
        /// <returns></returns>
        public IActionResult GetSurveysById([FromRoute] int Id)
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
                var response = _Survey.GetSurveyById(Id);

                if (response == null)
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant retrieve the survey please try again";
                    responseDto.Response = null;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Get surveys succesfully";
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
        [Authorize]
        /// <summary>
        /// Create the Survey
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateSurvey([FromBody] SurveyRequestDto Requests)
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
                var Url = $"{HttpContext.Request.Host.Host}:{HttpContext.Request.Host.Port}";
                var response = _Survey.CreateSurvey(Requests,Url);

                if (response == null || response.StartsWith("Error"))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant create the survey please try again";
                    responseDto.Response = Response;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Create survey succesfully";
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

        [HttpPut]
        [Authorize]
        [Route("{Id}")]
        /// <summary>
        /// Update the survey
        /// </summary>
        /// <returns></returns>
        public IActionResult UpdateSurvey([FromRoute] int Id,[FromBody] SurveyRequestDto Requests)
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
                var response = _Survey.UpdateSurvey(Id,Requests);

                if (response == null || response.StartsWith("Error"))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant update the survey please try again";
                    responseDto.Response = Response;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Update survey succesfully";
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

        [HttpDelete]
        [Authorize]
        [Route("{Id}")]
        /// <summary>
        /// Delete the survey
        /// </summary>
        /// <returns></returns>
        public IActionResult DeleteSurvey([FromRoute] int Id)
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
                var response = _Survey.DeleteSurvey(Id);

                if (response == null || response.StartsWith("Error"))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant delete the survey please try again";
                    responseDto.Response = Response;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Delete survey succesfully";
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

        [HttpPut]
        [Authorize]
        [Route("{Id}/Restore")]
        /// <summary>
        /// Restore the survey
        /// </summary>
        /// <returns></returns>
        public IActionResult RestoreSurvey([FromRoute] int Id)
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
                var response = _Survey.RestoreSurvey(Id);

                if (response == null || response.StartsWith("Error"))
                {
                    responseDto.IsSuccess = false;
                    responseDto.Message = "We cant restore the survey please try again";
                    responseDto.Response = Response;
                    return BadRequest();
                }

                responseDto.IsSuccess = true;
                responseDto.Message = "Restore survey succesfully";
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
