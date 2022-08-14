using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.ApplicationCore.DTOs;
using Project.ApplicationCore.Exceptions;
using Project.ApplicationCore.Interfaces;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartureController : ControllerBase
    {


        private readonly IBikeStationRepository bikeStationRepository;
   

        public DepartureController(IBikeStationRepository bikeStationRepository)
        {
            this.bikeStationRepository = bikeStationRepository;
           
        }

        [HttpPost("Create")]
        public ActionResult Create(CreateBikeStationRequest request)
        {
            var station = this.bikeStationRepository.CreateBikeStation(request);
            return Ok(station);
        }
       /* [HttpPut("GetStation")]
        public ActionResult Get(StationSearchRequest request)
        {
            try
            {
                var station = this.bikeStationRepository.Get(request);
                return Ok(station);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }*/
        [HttpPut("Update/{id}")]
        public ActionResult Update(int id, UpdateBikeStationRequest request)
        {
            try
            {
                var station = this.bikeStationRepository.UpdateBikeStationt(id, request);
                return Ok(station);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                this.bikeStationRepository.DeleteBikeStationById(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
       /* [HttpDelete]
        [Route("DeleteRecord")]
        public async Task<IActionResult> DeleteRecord(DeleteRecordRequest request)
        {
            DeleteRecordResponse response = new DeleteRecordResponse();

            try
            {
                response = await bikeStationRepository.DeleteRecord(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }  

            return Ok(response);
        }*/
    }
}
