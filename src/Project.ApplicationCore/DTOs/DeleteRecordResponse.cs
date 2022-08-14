using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ApplicationCore.DTOs
{
    public class DeleteRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
    public class DeleteRecordRequest
    {
        public int Id { get; set; }
    }
}
