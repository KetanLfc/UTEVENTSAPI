using AutoMapper;
using UTEvents.Entities;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<EventDto?> GetEventAsync(Guid eventId)
        {
            var eventEntity = await _eventRepository.GetEventAsync(eventId);
            return eventEntity == null ? null : _mapper.Map<EventDto>(eventEntity);
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync()
        {
            var eventEntities = await _eventRepository.GetAllEventsAsync();
            return _mapper.Map<IEnumerable<EventDto>>(eventEntities);
        }

        public async Task<EventDto?> CreateEventAsync(EventDto eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);
            var created = await _eventRepository.CreateEventAsync(eventEntity);

            return created ? _mapper.Map<EventDto>(eventEntity) : null;
        }

        public async Task<bool> UpdateEventAsync(EventDto eventDto)
        {
            var eventEntity = _mapper.Map<Event>(eventDto);
            return await _eventRepository.UpdateEventAsync(eventEntity);
        }

        public async Task<bool> DeleteEventAsync(Guid eventId)
        {
            return await _eventRepository.DeleteEventAsync(eventId);
        }
    }
}
