using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Url { get; set; }
        public ICollection<SurveyXQuestion> SurveysXQuestions { get; set; }
    }
}
