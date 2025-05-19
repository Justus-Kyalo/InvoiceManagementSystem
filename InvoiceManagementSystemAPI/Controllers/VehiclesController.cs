using System.Net;
using AutoMapper;
using Azure;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVehicleRepository _dbVehicle;
        private APIResponse _response;

        public VehiclesController(IMapper mapper,IVehicleRepository dbVehicle)
        {
            _mapper = mapper;
            _dbVehicle = dbVehicle;
            _response = new ();

        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> AsyncGetVehicles()
        {
            try
            {
                IEnumerable<Vehicle> query = await _dbVehicle.GetAllAsync();
                _response.Result = query;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors.Add(e.ToString());
            }

            return _response;

        }

        [HttpGet("{id}", Name = "AsyncGetVehicle")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> AsyncGetVehicle(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid VehicleId");
                    
                }

                Vehicle query = await _dbVehicle.GetAsync(u => u.VehicleId == id);
                if (query == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                _response.Result = _mapper.Map<VehicleDto>(query);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }
        
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> AsyncCreateVehicle([FromBody] VehicleCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid Request Body");
                }

                var query = await _dbVehicle.GetAsync(u => u.VehicleRegistration == createDto.VehicleRegistration);
                if (query != null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("A Vehicle with this vehicleRegistration Already exists");
                }

                Vehicle vehicle = _mapper.Map<Vehicle>(createDto);
                await _dbVehicle.CreateAsync(vehicle);
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = vehicle;
                return CreatedAtRoute("AsyncGetVehicle",new {id=vehicle.VehicleId},_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> AsyncUpdateVehicle(int id, [FromBody] VehicleUpdateDto updateDto)
        {
            try
            {
                if (id == 0 || updateDto.VehicleId != id )
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest("Invalid Request");
                }

                Vehicle vehicle = _mapper.Map<Vehicle>(updateDto);
                vehicle.UpdatedAt=DateTime.Now;
                await _dbVehicle.Update(vehicle);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }

        [HttpPatch]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> AsyncPartialUpdateVehicle(int id,JsonPatchDocument <VehicleUpdateDto> patchDto )
        {
            try
            {
                if (id == 0 || patchDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest("Invalid Request");
                }

                var vehicle = await _dbVehicle.GetAsync(u => u.VehicleId == id, tracked: false);
                VehicleUpdateDto vehicleDto = _mapper.Map<VehicleUpdateDto>(vehicle);
                patchDto.ApplyTo(vehicleDto,ModelState);
                Vehicle model = _mapper.Map<Vehicle>(vehicleDto);
                await _dbVehicle.Update(model);
                if (!ModelState.IsValid) BadRequest(ModelState);
                return NoContent();


            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors.Add(e.ToString());
            }

            return _response;

        }

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> AsyncDeleteVehicle(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest("Invalid Request");
                }

                var vehicle = await _dbVehicle.GetAsync(u => u.VehicleId == id);
                if (vehicle == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                await _dbVehicle.RemoveAsync(vehicle);
                await _dbVehicle.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);


            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }
    }
    
    
}
