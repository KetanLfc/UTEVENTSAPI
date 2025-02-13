using Microsoft.EntityFrameworkCore;
using UTEvents.Context;
using UTEvents.Entities;
using UTEvents.IRepository;

namespace UTEvents.Repositories
{
    public class EventCategoryRepository : IEventCategoryRepository
    {
        private readonly UTEventsContext _context;

        public EventCategoryRepository(UTEventsContext context)
        {
            _context = context;
        }

        public async Task<EventCategory?> GetEventCategoryAsync(string categoryName)
        {
            return await _context.EventCategories.FindAsync(categoryName);
        }

        public async Task<IEnumerable<EventCategory>> GetAllEventCategoriesAsync()
        {
            return await _context.EventCategories.ToListAsync();
        }

        public async Task<bool> CreateEventCategoryAsync(EventCategory newCategory)
        {
            _context.EventCategories.Add(newCategory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEventCategoryAsync(EventCategory updatedCategory)
        {
            _context.Entry(updatedCategory).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEventCategoryAsync(string categoryName)
        {
            var categoryToDelete = await _context.EventCategories.FindAsync(categoryName);
            if (categoryToDelete == null) return false;

            _context.EventCategories.Remove(categoryToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
