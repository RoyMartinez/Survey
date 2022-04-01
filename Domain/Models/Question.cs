using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public QuestionTypeEnum Type { get; set; }
        public ICollection<SurveyXQuestion> SurveysXQuestions { get; set; }

    }
}
