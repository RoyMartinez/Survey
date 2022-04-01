using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dto
{
    public class ApiResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Response { get; set; }
    }
}
