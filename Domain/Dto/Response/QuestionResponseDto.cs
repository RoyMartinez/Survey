using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public bool IsRequired { get; set; }
        public string Type { get; set; }
    }
}
