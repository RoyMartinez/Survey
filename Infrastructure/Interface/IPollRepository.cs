using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Interface
{
    public interface IPollRepository : IBaseRepository<Answer>
    {
        public Document GetSurveyResponseByCode(string Code);
        public IQueryable<Document> GetSurveyResponses();
        public IEnumerable<Question> GetQuestions(int SurveyId);
        public string SetAnswers(int SurveyId, List<AnswerRequestDto> Answers);
    }
}
