using Domain.Dto;
using Infrastructure.Interface;
using Microsoft.Extensions.Configuration;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Service
{
    public class SurveyService : ISurveyService
    {
        private readonly IConfiguration _configuration;
        private readonly ISurveyRepository _SurveyRepository;

        public SurveyService
        (
            IConfiguration configuration,
            ISurveyRepository SurveyRepository
        )
        {
            _configuration = configuration;
            _SurveyRepository = SurveyRepository;
        }

        /// <summary>
        /// Service to get all Survey
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<SurveyResponseDto> GetSurveys()
        {
            try
            {
                var response = _SurveyRepository.GetSurveys();
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Service to get the survey By Id
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SurveyResponseDto GetSurveyById(int Id)
        {
            try
            {
                var response = _SurveyRepository.GetSurveyById(Id);
                return response != null ? response.First() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Service to get all Survey By Name
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<SurveyResponseDto> GetSurveysByName(string Name)
        {
            try
            {
                var response = _SurveyRepository.GetSurveyByName(Name);
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Service to Create a new Survey
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public string CreateSurvey(SurveyRequestDto Request,string Url)
        {
            try
            {
                var response = _SurveyRepository.CreateSurvey(Request,Url);
                if (response == null || response.StartsWith("Error")) return response;
                return response;
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }

        /// <summary>
        /// Service to Update a survey
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Request"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string UpdateSurvey(int Id, SurveyRequestDto Request)
        {
            try
            {
                var response = _SurveyRepository.UpdateSurvey(Id,Request);
                if (response == null || response.StartsWith("Error")) return response;
                return "200";
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }

        /// <summary>
        /// Service to Delete a survey
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string DeleteSurvey(int Id)
        {
            try
            {
                var response = _SurveyRepository.DeleteSurvey(Id);
                if (response == null || response.StartsWith("Error")) return response;
                return "200";
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }

        /// <summary>
        /// Service to restore a survey deleted
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string RestoreSurvey(int Id)
        {
            try
            {
                var response = _SurveyRepository.RestoreSurvey(Id);
                if (response == null || response.StartsWith("Error")) return response;
                return "200";
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }
    }
}
