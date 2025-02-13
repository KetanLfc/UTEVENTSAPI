using AutoMapper;
using UTEvents.Entities;
using UTEvents.Enums;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Services
{
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _repository;
        private readonly IMapper _mapper;

        public UserEventService(IUserEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserEventDto>> GetUserEventsByUserIdAsync(Guid userId, RSVPStatus status)
        {
            var userEvents = await _repository.GetUserEventsByUserIdAsync(userId, status);
            return _mapper.Map<IEnumerable<UserEventDto>>(userEvents);
        }

        public async Task<IEnumerable<UserEventDto>> GetUserEventsByUserIdAsync(Guid userId)
        {
            var userEvents = await _repository.GetAllUserEventsByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<UserEventDto>>(userEvents);
        }

        public async Task<IEnumerable<UserEventDto>> GetAllUserEventsAsync()
        {
            var all = await _repository.GetAllUserEventsAsync();
            return _mapper.Map<IEnumerable<UserEventDto>>(all);
        }


        public async Task<bool> AddUserEventAsync(UserEventDto userEventDto)
        {
            var userEvent = _mapper.Map<UserEvent>(userEventDto);
            return await _repository.AddUserEventAsync(userEvent);
        }

        public async Task<bool> UpdateUserEventStatusAsync(Guid userId, Guid eventId, RSVPStatus status)
        {
            return await _repository.UpdateUserEventStatusAsync(userId, eventId, status);
        }
    }
}
