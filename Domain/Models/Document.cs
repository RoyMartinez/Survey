using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public  class Document
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string Code { get; set; }
        public ICollection<Answer> Answers { get; set; }

    }
}
