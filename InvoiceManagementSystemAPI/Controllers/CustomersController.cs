using System.Net;
using AutoMapper;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    
   
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _dbCustomer;
        private readonly IMapper _mapper;
        internal APIResponse _response;
        public CustomersController(ICustomerRepository dbCustomer,IMapper mapper)
        {
            _dbCustomer = dbCustomer;
            _mapper = mapper;
            _response = new();

        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<APIResponse>> GetCustomersAsync()
        {
            try
            {
                IEnumerable<Customer> query =  await _dbCustomer.GetAllAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<CustomerDto>>(query);
                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;

            }

            return _response;
        }
        [HttpGet("{id}",Name="GetCustomerAsync")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<APIResponse>> GetCustomerAsync(int  id)
        {
            try
            {
                if (id==0)
                {
                    return BadRequest("Invalid Id");

                }

                var customer = await _dbCustomer.GetAsync(u => u.CustomerId == id);
                if (customer==null)
                {
                    return NotFound();

                }

                _response.Result = _mapper.Map<CustomerDto>(customer);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<APIResponse>> CreateCustomerAsync([FromBody] CustomerCreateDto createDto)
        {
            try
            {
                if (await _dbCustomer.GetAsync(u => u.AccountNumber.ToLower() == createDto.AccountNumber.ToLower()) !=
                    null)
                {
                    ModelState.AddModelError("CustomError", "A Customer with this AccountNumber Already Exists");
                    return BadRequest(ModelState);

                }

                if (createDto == null)

                {
                    
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid Customer entry");
                }

                 var customer =_mapper.Map<Customer>(createDto);
                 await _dbCustomer.CreateAsync(customer);
                 await _dbCustomer.SaveAsync();

                 _response.StatusCode = HttpStatusCode.Created;
                 _response.Result = customer;
                 return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, _response);

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
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<APIResponse>> UpdateCustomerAsync([FromBody] CustomerUpdateDto updateDto,int id)
        {
            try
            {
                if (updateDto == null || updateDto.CustomerId != id)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var customer = _mapper.Map<Customer>(updateDto);
                await _dbCustomer.UpdateAsync(customer);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;
            }

            return _response;



        }
        
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        

        public async Task<ActionResult<APIResponse>> UpdatePartialCustomerAsync(int id, JsonPatchDocument <CustomerUpdateDto> patchDto)
        {
            try
            {
                if (patchDto== null ||  id==0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var customer = await _dbCustomer.GetAsync(u => u.CustomerId == id,tracked:false);

                CustomerUpdateDto customerUpdateDto = _mapper.Map<CustomerUpdateDto>(customer);
                
                patchDto.ApplyTo(customerUpdateDto,ModelState);

                Customer model = _mapper.Map<Customer>(customerUpdateDto);
                await _dbCustomer.UpdateAsync(model);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                
                }

                _response.StatusCode = HttpStatusCode.NoContent;

                return NoContent();
                
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(e.ToString());
                _response.IsSuccess = false;
            }

            return _response;



        }
        [HttpDelete("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        
        public async Task<ActionResult<APIResponse>> DeleteCustomerAsync(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var customer = await _dbCustomer.GetAsync(u => u.CustomerId == id);

                if (customer == null)
                {
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return NoContent();
                }

                await _dbCustomer.RemoveAsync(customer);
                await _dbCustomer.SaveAsync();
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
