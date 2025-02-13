using UTEvents.Entities;

namespace UTEvents.IRepository
{
    public interface IEventCategoryRepository
    {
        Task<EventCategory?> GetEventCategoryAsync(string categoryName);
        Task<IEnumerable<EventCategory>> GetAllEventCategoriesAsync();
        Task<bool> CreateEventCategoryAsync(EventCategory newCategory);
        Task<bool> UpdateEventCategoryAsync(EventCategory updatedCategory);
        Task<bool> DeleteEventCategoryAsync(string categoryName);
    }
}
