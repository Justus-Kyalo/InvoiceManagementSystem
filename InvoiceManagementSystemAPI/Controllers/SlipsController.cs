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
        private readonly ISlipDetailRepository _dbslipDetailRepository;
        

        public SlipsController(ISlipRepository dbSlip,IMapper mapper,ISlipDetailRepository slipDetailRepository)
        {
            _dbSlip = dbSlip;
            _mapper = mapper;
            _dbslipDetailRepository = slipDetailRepository;
            _response = new();
        }
        [HttpPost("/SlipsCollection")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetCustomerSlipsAsync([FromBody] SlipDetailDto slipDetailDto)
        {
            try
            {
                IEnumerable<SlipDetail> detailedSlips = await _dbslipDetailRepository.GetAllAsync(u =>
                    u.CustomerId == slipDetailDto.CustomerId &&
                    u.SlipDate >= slipDetailDto.StartDate &&
                    u.SlipDate <= slipDetailDto.EndDate
                );
                _response.Result = detailedSlips;
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
        
        [HttpGet("/SlipsCollection")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetDetailedSlipsAsyn()
        {
            try
            {
                IEnumerable<SlipDetail> detailedSlips = await _dbslipDetailRepository.GetAllAsync();
                _response.Result = detailedSlips;
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

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetSlipsAsync()
        {
            try
            {
                IEnumerable<Slip> slipsList = await _dbSlip.GetAllAsync(includeProperties:u=>u.SlipItems);
                _response.Result = _mapper.Map < List<SlipDto>>(slipsList);
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

                var slip = await _dbSlip.GetAsync(u => u.SlipId == id,includeProperties:u=>u.SlipItems);
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
                        u.SlipNumber == createDto.SlipNumber) != null)

                {
                    ModelState.AddModelError("customError", "slip with this collection slip Number already exists");
                    return BadRequest(ModelState);

                }

                if (createDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid slip");
                }

                Slip slip = _mapper.Map<Slip>(createDto);
                await _dbSlip.CreateAsync(slip);
                // await _dbSlip.SaveAsync();
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
                slip.UpdatedAt=DateTime.Now;
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

            var slip = await _dbSlip.GetAsync(u => u.SlipId == id, tracked: false,includeProperties:u=>u.SlipItems);

            SlipUpdateDto slipDto = _mapper.Map<SlipUpdateDto>(slip);
            patchDto.ApplyTo(slipDto, ModelState);
            Slip model = _mapper.Map<Slip>(slipDto);
            model.UpdatedAt = DateTime.Now;
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
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        [ProducesResponseType(200)]


        
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
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NoContent();
                }

                await _dbSlip.RemoveAsync(slip);
                await _dbSlip.SaveAsync();
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
