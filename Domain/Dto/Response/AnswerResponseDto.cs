using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class AnswerResponseDto
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionName { get; set; }
        public int AnswerId { get; set; }
        public string Response { get; set; }
    }
}
