using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Interface
{
    public interface ISurveyService
    {
        public List<SurveyResponseDto> GetSurveys();
        public SurveyResponseDto GetSurveyById(int Id);
        public List<SurveyResponseDto> GetSurveysByName(string Name);
        public string CreateSurvey(SurveyRequestDto Request,string Url);
        public string UpdateSurvey(int Id,SurveyRequestDto Request);
        public string DeleteSurvey(int Id);
        public string RestoreSurvey(int Id);
    }
}
