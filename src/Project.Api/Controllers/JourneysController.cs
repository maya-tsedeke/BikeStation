
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Interfaces;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneysController : ControllerBase
    {

        private readonly IReaderWrapperRepository _wrapper;
        private readonly IJourneyListRepository _journey;
        public JourneysController(IReaderWrapperRepository bikeStation, IJourneyListRepository journey)
        {
            _wrapper = bikeStation;
            _journey = journey;
        }
        [HttpGet]
        public async Task<IActionResult> SearchJourney([FromQuery] StationSearchRequest request)
        {

            var data = await _journey.FilterRecord(request);
       
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dataWrapp));
          //  _logger.LogInfo($"Returned {dat} stations from City Bike Station database.");
            return Ok(data);
        }
        [HttpGet]
        [Route("Filter")]
        public ActionResult GetDeparture([FromQuery] ConditionParameters filter)
        {
            var data = _wrapper.BikeStation.GetAllBikeStation(filter);
                var dataWrapp = new
                {
                    data.TotalCount,
                    data.PageSize,
                    data.CurrentPage,
                    data.HasNext,
                    data.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dataWrapp));
            return Ok(data);
        }
  
    }
}