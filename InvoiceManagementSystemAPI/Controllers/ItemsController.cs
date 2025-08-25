using System.ComponentModel.DataAnnotations;
using System.Net;
using AutoMapper;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _dbItems;
        private readonly IMapper _mapper;
        private APIResponse _response;
        public ItemsController(IItemRepository dbItems,IMapper mapper)
        {
            _dbItems = dbItems;
            _mapper = mapper;
            _response = new();

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]


        public async Task<ActionResult<APIResponse>> CreateItem([FromBody] ItemCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest("Invalid Item");

                }

                if ( await _dbItems.GetAsync(u => u.ItemName.ToLower() == createDto.ItemName.ToLower()) != null)
                {
                    ModelState.AddModelError("customError","Item with particular Name Already Exists");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(ModelState);
                }

                Item item = _mapper.Map<Item>(createDto);
                await _dbItems.CreateAsync(item);
                await _dbItems.SaveAsync();

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = item;

                return CreatedAtRoute("GetItem", new { id = item.ItemId }, _response);

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
        public async Task<ActionResult<APIResponse>> GetAllItems()
        {
            try
            {
                IEnumerable <Item> items =  await _dbItems.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<List<ItemDto>>(items);
                return Ok(_response);



            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }
        

        [HttpGet("{id}", Name = "GetItem")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<APIResponse>> GetItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var item = await _dbItems.GetAsync(u => u.ItemId == id);
                if (item == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound("Item Does not exist");
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _mapper.Map<ItemDto>(item);
                return Ok(_response);

            }
            catch (Exception e)
            {

                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<APIResponse>> UpdateItem(int id,[FromBody] ItemUpdateDto updateDto)
        {
            try
            {
                if (id == 0 || id != updateDto.ItemId)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var item = await _dbItems.GetAsync(u => u.ItemId == id);
                if (item == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                Item entity = _mapper.Map<Item>(updateDto);

                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = entity;
                return Ok(_response);


            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public async Task<ActionResult<APIResponse>> PartialUpdateItemAsync(int id, JsonPatchDocument<ItemUpdateDto> patchDto)
        {
            try
            {
                if (id == 0 || patchDto==null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                var item = await _dbItems.GetAsync(u => u.ItemId == id,tracked:false);
                if (item == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                ItemUpdateDto itemUpdateDto = _mapper.Map<ItemUpdateDto>(item);
                patchDto.ApplyTo(itemUpdateDto,ModelState);

                Item model = _mapper.Map<Item>(itemUpdateDto);
                await _dbItems.UpdateAsync(model);
                await _dbItems.SaveAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = model;
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();
                }

                return Ok(_response);


            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(e.ToString());
            }

            return _response;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]

        public async Task<ActionResult<APIResponse>> DeleteItemAsync(int id)
        {
            try
            {
                if(id==0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest();

                }
                var item= await _dbItems.GetAsync(u => u.ItemId == id);

                if (item == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound();
                }

                await _dbItems.RemoveAsync(item);
                await _dbItems.SaveAsync();

                _response.StatusCode = HttpStatusCode.NoContent;
                return NoContent();


            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.Errors.Add(e.ToString());
            }

            return _response;

        }
        
    }
}
