using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface IPollService
    {
        public IEnumerable<QuestionResponseDto> GetQuestions(int SurveyId);
        public string SetAnswers(int SurveyId,List<AnswerRequestDto> Answers);
        public DocumentResponseDto GetAnswerByCode(string Code);
        public IEnumerable<DocumentResponseDto> GetAllAnswers();
    }
}
