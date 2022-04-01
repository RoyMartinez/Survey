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
    public class PollService: IPollService
    {
        private readonly IConfiguration _configuration;
        private readonly IPollRepository _PollRepository;

        public PollService
        (
            IConfiguration configuration,
            IPollRepository PollRepository
        )
        {
            _configuration = configuration;
            _PollRepository = PollRepository;
        }


        /// <summary>
        /// Get the question for the polls
        /// </summary>
        /// <param name="SurveyId"></param>
        /// <returns></returns>
        public IEnumerable<QuestionResponseDto> GetQuestions(int SurveyId)
        {
            try
            {
                var questions = _PollRepository.GetQuestions(SurveyId);
                if (questions == null) return null;
                var response = questions.Select(q => new QuestionResponseDto()
                {
                    Id = q.Id,
                    Name = q.Name,
                    Title = q.Title,
                    IsRequired = q.IsRequired,
                    Type = q.Type.ToString(),
                });
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Set the answer of the poll
        /// </summary>
        /// <param name="SurveyId"></param>
        /// <param name="Answers"></param>
        /// <returns></returns>
        public string SetAnswers(int SurveyId, List<AnswerRequestDto> Answers)
        {
            try
            {
                var response = _PollRepository.SetAnswers(SurveyId, Answers);
                return response;
            }
            catch (Exception ex)
            {
                return $"Error {ex.Message}";
            }
        }

        /// <summary>
        /// Get all the answer of the polls
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DocumentResponseDto> GetAllAnswers()
        {
            try
            {
                var documents = _PollRepository.GetSurveyResponses();
                if (documents == null) return null;

                return documents.Select(document => new DocumentResponseDto()
                {
                    Code = document.Code,
                    CreationTime = document.CreationTime,
                    Answers = document.Answers.Select(ans => new AnswerResponseDto()
                    {
                        QuestionId = ans.SurveyXQuestion.Question.Id,
                        QuestionName = ans.SurveyXQuestion.Question.Name,
                        QuestionTitle = ans.SurveyXQuestion.Question.Title,
                        AnswerId = ans.Id,
                        Response = ans.String != null ? ans.String : ans.Date != null ? ans.Date.ToString() : ans.Number != null ? ans.Number.ToString() : null,
                    }).AsEnumerable(),
                }).AsEnumerable();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Get the answers of a code Document
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public DocumentResponseDto GetAnswerByCode(string Code)
        {
            try
            {
                var document = _PollRepository.GetSurveyResponseByCode(Code);
                if (document == null) return null;
                var response = new DocumentResponseDto()
                {
                    Code = document.Code,
                    CreationTime = document.CreationTime,
                    Answers = document.Answers.Select(ans => new AnswerResponseDto() 
                    {
                        QuestionId = ans.SurveyXQuestion.Question.Id,
                        QuestionName = ans.SurveyXQuestion.Question.Name,
                        QuestionTitle = ans.SurveyXQuestion.Question.Title,
                        AnswerId = ans.Id,
                        Response = ans.String!= null?ans.String:ans.Date != null?ans.Date.ToString():ans.Number != null? ans.Number.ToString():null,
                    }).AsEnumerable(),
                };
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
