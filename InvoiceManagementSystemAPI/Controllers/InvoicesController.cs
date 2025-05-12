using System.Net;
using AutoMapper;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InvoicesController:ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IInvoiceRepository _dbInvoices;
    internal APIResponse _response;

    public InvoicesController(IMapper mapper, IInvoiceRepository dbInvoices)
    {
        _mapper = mapper;
        _dbInvoices = dbInvoices;
        _response = new APIResponse();

    }
    
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    [ProducesResponseType(400)]


    public async Task<ActionResult<APIResponse>> CreateInvoiceAsync([FromBody] InvoiceCreateDto createDto)
    {
        try
        {
            if (createDto == null)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest("Invalid Item");

            }

            if ( await _dbInvoices.GetAsync(u => u.JobNumber == createDto.JobNumber) != null)
            {
                ModelState.AddModelError("customError","Item with particular JobNumber  Already Exists");
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(ModelState);
            }

            Invoice invoice = _mapper.Map<Invoice>(createDto);
            await _dbInvoices.CreateAsync(invoice);
            await _dbInvoices.SaveAsync();

            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = invoice;

            return CreatedAtRoute("GetItem", new { id = invoice.InvoiceId }, _response);

        }
        catch (Exception e)
        {

            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Errors.Add(e.ToString());
        }

        return _response;
    }
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<APIResponse>> GetAllInvoices()
    {
        try
        {
            IEnumerable <Invoice> invoices =  await _dbInvoices.GetAllAsync();
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = _mapper.Map<List<InvoiceDto>>(invoices);
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