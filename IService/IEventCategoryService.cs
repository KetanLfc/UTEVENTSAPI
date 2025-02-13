using UTEvents.Models;

namespace UTEvents.IService
{
    public interface IEventCategoryService
    {
        Task<EventCategoryDto?> GetEventCategoryAsync(string categoryName);
        Task<IEnumerable<EventCategoryDto>> GetAllEventCategoriesAsync();
        Task<EventCategoryDto?> CreateEventCategoryAsync(EventCategoryDto eventCategoryDto);
        Task<bool> UpdateEventCategoryAsync(EventCategoryDto eventCategoryDto);
        Task<bool> DeleteEventCategoryAsync(string categoryName);
    }
}
