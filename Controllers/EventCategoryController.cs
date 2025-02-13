using Microsoft.AspNetCore.Mvc;
using UTEvents.Models;
using UTEvents.IService;
using Microsoft.AspNetCore.Authorization;

namespace UTEvents.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoriesController : ControllerBase
    {
        private readonly IEventCategoryService _eventCategoryService;

        public EventCategoriesController(IEventCategoryService eventCategoryService)
        {
            _eventCategoryService = eventCategoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventCategoryDto>>> GetAllEventCategories()
        {
            return Ok(await _eventCategoryService.GetAllEventCategoriesAsync());
        }

        [HttpGet("{categoryName}")]
        public async Task<ActionResult<EventCategoryDto>> GetEventCategory(string categoryName)
        {
            var categoryDto = await _eventCategoryService.GetEventCategoryAsync(categoryName);
            return categoryDto == null ? NotFound() : Ok(categoryDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<EventCategoryDto>> CreateEventCategory([FromBody] EventCategoryDto eventCategoryDto)
        {
            var createdCategory = await _eventCategoryService.CreateEventCategoryAsync(eventCategoryDto);
            return createdCategory == null ? BadRequest() : Ok(createdCategory);
        }

        [HttpPut("{categoryName}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateEventCategory(string categoryName, [FromBody] EventCategoryDto eventCategoryDto)
        {
            eventCategoryDto = eventCategoryDto with { CategoryName = categoryName };
            var updated = await _eventCategoryService.UpdateEventCategoryAsync(eventCategoryDto);
            return updated ? NoContent() : BadRequest();
        }

        [HttpDelete("{categoryName}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteEventCategory(string categoryName)
        {
            var deleted = await _eventCategoryService.DeleteEventCategoryAsync(categoryName);
            return deleted ? NoContent() : BadRequest();
        }
    }
}
