using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Exceptions;
using Project.ApplicationCore.Interfaces;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataimportController : ControllerBase
    {
        private readonly ICSVDataImportRepository importRepository;
        private readonly ILoggerManager _logger;
        public DataimportController(ICSVDataImportRepository importRepository, ILoggerManager logger)
        {
            this.importRepository = importRepository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> ImportFromCSVfileAsync([FromForm]CSVFileRequest request) 
        {
            CSVFileResponse response = new CSVFileResponse();
            string path = "Upload/" + request.File.FileName;
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.CreateNew))
                {
                    await request.File.CopyToAsync(stream);
                }
                response = this.importRepository.ImportBikeStationList(request, path);
                string[] files = Directory.GetFiles("Upload/");
                foreach (string file in files)
                {
                    System.IO.File.Delete(file);
                    Response.Headers.Add("X-Import", $"{file.Count()} travel history imported to the database and {file} is deleted from the Upload folder.");
                   
                }
                
                
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            return Ok(response);
        }
    }
}
