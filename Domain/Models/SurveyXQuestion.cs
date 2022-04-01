using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class SurveyXQuestion
    {
        public int Id { get; set; }
        public int SurveyId { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Survey Survey { get; set; }

        public ICollection<Answer> Answers { get; set; } 
    }
}
