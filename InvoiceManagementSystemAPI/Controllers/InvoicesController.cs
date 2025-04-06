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
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _dbInvoice;
        private readonly IMapper _mapper;
        private  APIResponse _response;
        

        public InvoicesController(IInvoiceRepository dbInvoice,IMapper mapper)
        {
            _dbInvoice = dbInvoice;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<APIResponse>> GetInvoices()
        {
            try
            {
                IEnumerable<Invoice> invoicesList = await _dbInvoice.GetAllAsync();
                _response.Result = _mapper.Map < List<InvoiceDto>>(invoicesList);
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

        [HttpGet("{id}", Name = "GetInvoice")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetInvoice(int id)
        {

            try
            {
                if (id == 0)
                {
                    return BadRequest("Invalid Id");
                }

                var invoice = await _dbInvoice.GetAsync(u => u.InvoiceId == id);
                if (invoice == null)
                {
                    return NotFound();
                }

                _response.Result = _mapper.Map<InvoiceDto>(invoice);
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

        public async Task<ActionResult<APIResponse>> CreateInvoice([FromBody] InvoiceCreateDto createDto)
        {
            try
            {
                if (await _dbInvoice.GetAsync(u =>
                        u.CollectionSlipNumber.ToLower() == createDto.CollectionSlipNumber.ToLower()) != null)

                {
                    ModelState.AddModelError("customError", "invoice with this collection slip Number already exists");
                    return BadRequest(ModelState);

                }

                if (createDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid Invoice");
                }

                Invoice invoice = _mapper.Map<Invoice>(createDto);
                await _dbInvoice.CreateAsync(invoice);
                await _dbInvoice.SaveAsync();
                _response.Result = invoice;
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetInvoice",new
                {
                    id=invoice.InvoiceId
                },_response);

            }
            catch (Exception e)
            {
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
            }

            return _response;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]

        public async Task<ActionResult<APIResponse>> UpdateInvoice(int id, [FromBody] InvoiceUpdateDto updateDto)
        {
            try
            {
                if (id == null || updateDto.InvoiceId != id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                Invoice invoice = _mapper.Map<Invoice>(updateDto);
                await _dbInvoice.UpdateAsync(invoice);
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

        [HttpPatch("{id}", Name = "UpdatePartialInvoice")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdatePartialInvoice(int id, JsonPatchDocument <InvoiceUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0)
            {
                return BadRequest();
            }

            var invoice = await _dbInvoice.GetAsync(u => u.InvoiceId == id, tracked: false);

            InvoiceUpdateDto invoiceDto = _mapper.Map<InvoiceUpdateDto>(invoice);
            patchDto.ApplyTo(invoiceDto, ModelState);
            Invoice model = _mapper.Map<Invoice>(invoiceDto);
            await _dbInvoice.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                
            }

            return NoContent();

        }

        [HttpDelete("{id}", Name = "DeleteInvoice")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> DeleteInvoice(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var invoice = await _dbInvoice.GetAsync(u => u.InvoiceId == id);

                if (invoice == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }

                await _dbInvoice.RemoveAsync(invoice);
                await _dbInvoice.SaveAsync();
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
