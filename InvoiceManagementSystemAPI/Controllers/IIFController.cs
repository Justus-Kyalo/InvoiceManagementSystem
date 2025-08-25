using System.Net;
using System.Text;
using AutoMapper;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using InvoiceManagementSystemAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IIFController : ControllerBase
    {
        private readonly IIIFBackupRepository _dbIIFBackup;
        private readonly ISlipDetailRepository _dbSlipDetail;
        private readonly IIIFGeneratorService _iifService;
        private readonly IMapper _mapper;
        internal APIResponse _response;
        public IIFController(IIIFBackupRepository dbIIFBackup, ISlipDetailRepository dbSlipDetail,IIIFGeneratorService iifService,IMapper mapper)
        {
            _dbIIFBackup = dbIIFBackup;
            _dbSlipDetail = dbSlipDetail;
            _iifService = iifService;
            _mapper = mapper;
            _response = new();

        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<APIResponse>> GetIIFBackups()
        {
            try
            {
                IEnumerable<IIFBackup> query = await _dbIIFBackup.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result =  _mapper.Map<List<IIFBackupDto>>(query);
                return Ok(_response);


            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(e.ToString());
            }

            return _response;

        }

        [HttpGet("{id}",Name = "GetIIFBackup")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<APIResponse>> GetIIFBackup(int id)
        {
            try
            {
                if (id == 0 || id == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid id");
                }

                var iifBackup = await _dbIIFBackup.GetAsync(u => u.IIFBackupId == id);
                if (iifBackup == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<IIFBackupDto>(iifBackup);

                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }
        
        
        [HttpPost("export")]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> CreateIIFBackup([FromBody] IIFBackupCreateDto createDto)
        {
            try
            {

                if (createDto.StartDate == null || createDto.EndDate == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid IIFBackup entry");
                }

                if (createDto.StartDate >= createDto.EndDate)
                {
                    return BadRequest(
                        new ProblemDetails
                        {
                            Title = "Invalid date range",
                            Detail = "End date must be after start date"
                        }
                    );

                }

                List<SlipDetail> DetailedSlips = new List<SlipDetail>();
                foreach (var customerId in createDto.Customers)
                {
                    var slips = await _dbSlipDetail.GetAllAsync(u =>
                        (u.SlipDate >= createDto.StartDate && u.SlipDate <= createDto.EndDate) &&
                        u.CustomerId == customerId && u.Status != "billed");
                    DetailedSlips.AddRange(slips);

                }
                
                    string iifContent = _iifService.GenerateIIFContent(DetailedSlips);
                    string FileName = $"Invoice_{createDto.StartDate:yyyyMMdd}_to_{createDto.EndDate:yyyyMMdd}.csv";
                    return File(Encoding.UTF8.GetBytes(iifContent), "text/csv", FileName);
                
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Title = "Server Error",
                    Detail = "An error occurred while generating the CSV file"
                });
            }

        }
        
    }
    
}
