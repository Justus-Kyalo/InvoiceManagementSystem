using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlipsController : ControllerBase
    {
        private readonly ISlipRepository _dbSlip;
        private readonly IMapper _mapper;
        private  APIResponse _response;
        

        public SlipsController(ISlipRepository dbSlip,IMapper mapper)
        {
            _dbSlip = dbSlip;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetSlipsAsync()
        {
            try
            {
                IEnumerable<Slip> slipssList = await _dbSlip.GetAllAsync();
                _response.Result = _mapper.Map < List<SlipDto>>(slipssList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;

            }

            return _response;
        }

        [HttpGet("{id}", Name = "GetSlipAsync")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetSlipAsync(int id)
        {

            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid Id");
                }

                var slip = await _dbSlip.GetAsync(u => u.SlipId == id);
                if (slip == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<SlipDto>(slip);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);


            }
            catch (Exception e)
            {
               _response.Errors.Add(e.ToString());
               _response.StatusCode = HttpStatusCode.BadRequest;
               _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<APIResponse>> CreateSlipAsync([FromBody] SlipCreateDto createDto)
        {
            try
            {
                if (await _dbSlip.GetAsync(u =>
                        u.SlipNumber.ToLower() == createDto.SlipNumber.ToLower()) != null)

                {
                    ModelState.AddModelError("customError", "slip with this collection slip Number already exists");
                    return BadRequest(ModelState);

                }

                if (createDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid slip");
                }
                createDto.createdDate=DateTime.Now;

                Slip slip = _mapper.Map<Slip>(createDto);
                await _dbSlip.CreateAsync(slip);
                await _dbSlip.SaveAsync();
                _response.Result = slip;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSlipAsync",new
                {
                    id=slip.SlipId
                },_response);

            }
            catch (Exception e)
            {
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _response;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]

        public async Task<ActionResult<APIResponse>> UpdateSlipAsync(int id, [FromBody] SlipUpdateDto updateDto)
        {
            try
            {
                if (id == 0 || updateDto.SlipId != id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                Slip slip = _mapper.Map<Slip>(updateDto);
                await _dbSlip.UpdateAsync(slip);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            return (_response);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdatePartialSlipAsync(int id, JsonPatchDocument <SlipUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var slip = await _dbSlip.GetAsync(u => u.SlipId == id, tracked: false);

            SlipUpdateDto slipDto = _mapper.Map<SlipUpdateDto>(slip);
            patchDto.ApplyTo(slipDto, ModelState);
            Slip model = _mapper.Map<Slip>(slipDto);
            await _dbSlip.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        
        public async Task<ActionResult<APIResponse>> DeleteSlipAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var slip = await _dbSlip.GetAsync(u => u.SlipId == id);

                if (slip == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }

                await _dbSlip.RemoveAsync(slip);
                await _dbSlip.SaveAsync();
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);


            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.Errors.Add(e.ToString());
                
            }

            return _response;
        }
        
         
        
    }
    
    
    
}
