using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class SurveyResponseDto
    {
        public bool IsDeleted { get; set; }
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<QuestionResponseDto> Questions { get; set; }
    }
}
