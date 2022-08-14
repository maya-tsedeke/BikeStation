using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Interfaces;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdditionalController : ControllerBase
    {
       
        private readonly IReaderWrapperRepository _wrapper;
        private readonly IJourneyListRepository _journey;
       
        public AdditionalController(IReaderWrapperRepository bikeStation, IJourneyListRepository journey)
        {
        
            _wrapper = bikeStation;
            _journey = journey;
          
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStation()
        {

            var data = await _journey.Additional();
            return Ok(data);
        }
        [HttpPost]
        [Route("FilterByMonth")]
        public async Task<IActionResult> FilterByMonth([FromQuery]FilterByMonth request)
        {

            var data = await _journey.FilterByMonth(request);
            var dataWrapp = new
            {
                data.TotalPage,
                data.CurrentPage,
                data.HasNext,
                data.HasPrevious

            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dataWrapp));
            return Ok(data);
        }
    }
}
