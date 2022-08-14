
using Microsoft.AspNetCore.Http;

namespace Project.ApplicationCore.DTOs
{
    public class CSVFileRequest
    {
        public IFormFile? File { get; set; }
    }
    public class CSVFileResponse 
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
