using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class DocumentResponseDto
    {
        public string Code { get; set; }
        public DateTime CreationTime {get; set; }
        public IEnumerable<AnswerResponseDto> Answers { get; set; }

    }
}
