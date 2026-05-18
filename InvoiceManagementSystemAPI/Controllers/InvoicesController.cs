using System.Net;
using AutoMapper;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin,clerk")]
public class InvoicesController:ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IInvoiceRepository _dbInvoices;
    private readonly ICustomerRepository _dbCustomer;
    internal APIResponse _response;

    public InvoicesController(IMapper mapper, IInvoiceRepository dbInvoices,ICustomerRepository dbCustomer)
    {
        _mapper = mapper;
        _dbInvoices = dbInvoices;
        _dbCustomer = dbCustomer;
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
            IEnumerable <Invoice> invoices =  await _dbInvoices.GetAllAsync(tracked:false);
            List<InvoiceDto> invoiceDtos = new List<InvoiceDto>();
            InvoiceDto invoiceDto = new InvoiceDto();
            foreach (Invoice invoice in invoices)
            {
                invoiceDto = _mapper.Map<InvoiceDto>(invoice);
                var customer = await _dbCustomer.GetAsync(u => u.CustomerId == invoice.CustomerId);
                invoiceDto.CustomerName = customer.Name;
                invoiceDto.CustomerId = customer.CustomerId;
                invoiceDtos.Add(invoiceDto);
            }
            _response.StatusCode = HttpStatusCode.OK;
            _response.Result = invoiceDtos;
            return Ok(_response);



        }
        catch (Exception e)
        {
            _response.StatusCode = HttpStatusCode.InternalServerError;
            _response.Errors.Add(e.ToString());
        }

        return _response;
    }

}