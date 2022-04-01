using Domain.Dto;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Interface
{
    public interface ISurveyRepository: IBaseRepository<Survey>
    {
        List<SurveyResponseDto> GetSurveyByName(string Name);
        List<SurveyResponseDto> GetSurveyById(int Id);
        List<SurveyResponseDto> GetSurveys();
        string CreateSurvey(SurveyRequestDto Survey,string Url);
        string UpdateSurvey(int Id, SurveyRequestDto Survey);
        string DeleteSurvey(int Id);
        string RestoreSurvey(int Id);
    }
}
