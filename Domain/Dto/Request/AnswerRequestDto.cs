using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class AnswerRequestDto
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
