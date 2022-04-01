using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class SurveyRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionRequestDto> Questions { get; set; }
    }
}
