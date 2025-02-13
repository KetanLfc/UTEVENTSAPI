using AutoMapper;
using UTEvents.Entities;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Services
{
    public class EventCategoryService : IEventCategoryService
    {
        private readonly IEventCategoryRepository _eventCategoryRepository;
        private readonly IMapper _mapper;

        public EventCategoryService(IEventCategoryRepository eventCategoryRepository, IMapper mapper)
        {
            _eventCategoryRepository = eventCategoryRepository;
            _mapper = mapper;
        }

        public async Task<EventCategoryDto?> GetEventCategoryAsync(string categoryName)
        {
            var categoryEntity = await _eventCategoryRepository.GetEventCategoryAsync(categoryName);
            return categoryEntity == null ? null : _mapper.Map<EventCategoryDto>(categoryEntity);
        }

        public async Task<IEnumerable<EventCategoryDto>> GetAllEventCategoriesAsync()
        {
            var categories = await _eventCategoryRepository.GetAllEventCategoriesAsync();
            return _mapper.Map<IEnumerable<EventCategoryDto>>(categories);
        }

        public async Task<EventCategoryDto?> CreateEventCategoryAsync(EventCategoryDto eventCategoryDto)
        {
            var categoryEntity = _mapper.Map<EventCategory>(eventCategoryDto);
            var created = await _eventCategoryRepository.CreateEventCategoryAsync(categoryEntity);
            return created ? eventCategoryDto : null;
        }

        public async Task<bool> UpdateEventCategoryAsync(EventCategoryDto eventCategoryDto)
        {
            var categoryEntity = _mapper.Map<EventCategory>(eventCategoryDto);
            return await _eventCategoryRepository.UpdateEventCategoryAsync(categoryEntity);
        }

        public async Task<bool> DeleteEventCategoryAsync(string categoryName)
        {
            return await _eventCategoryRepository.DeleteEventCategoryAsync(categoryName);
        }
    }
}
