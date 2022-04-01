using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public int SurveyXQuestionId { get; set; }
        public string? String { get; set; }
        public long? Number { get; set; }
        public DateTime? Date { get; set; }

        public virtual Document Document { get; set; }
        public virtual SurveyXQuestion SurveyXQuestion { get; set;}

    }
}
