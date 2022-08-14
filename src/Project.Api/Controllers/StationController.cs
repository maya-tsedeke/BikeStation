using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Interfaces;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StationController : ControllerBase
    {
        private readonly IJourneyListRepository _journey;
        public StationController(IJourneyListRepository journey)
        {
            _journey = journey;
      
        }

        [HttpPost]
        [Route("Search")]
        public async Task<IActionResult>GetAllStation([FromQuery] GetStationRequest request)
        {
            GetStationResponse response = new GetStationResponse();

            try
            {
                response = await _journey.GetAllStation(request);
                var dataWrapp = new
                {
                    request.SearchKey,
                    response.TotalSearch,
                    response.CurrentPage,
                    response.TotalPage, 
                    response.HasPrevious,
                    response.HasNext,
                    request.PageNumber,
                    request.PageSize
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(dataWrapp));

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
    }
}
