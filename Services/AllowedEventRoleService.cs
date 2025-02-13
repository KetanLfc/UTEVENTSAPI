using AutoMapper;
using UTEvents.Entities;
using UTEvents.Enums;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Services
{
    public class AllowedEventRoleService : IAllowedEventRoleService
    {
        private readonly IAllowedEventRoleRepository _repository;
        private readonly IMapper _mapper;

        public AllowedEventRoleService(IAllowedEventRoleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AllowedEventRoleDto>> GetAllowedRolesByEventIdAsync(Guid eventId)
        {
            var roles = await _repository.GetAllowedRolesByEventIdAsync(eventId);
            return _mapper.Map<IEnumerable<AllowedEventRoleDto>>(roles);
        }

        public async Task<bool> AddAllowedEventRoleAsync(AllowedEventRoleDto allowedEventRoleDto)
        {
            var role = _mapper.Map<AllowedEventRole>(allowedEventRoleDto);
            return await _repository.AddAllowedEventRoleAsync(role);
        }

        public async Task<bool> DeleteAllowedEventRoleAsync(Guid eventId, EventRole role)
        {
            return await _repository.DeleteAllowedEventRoleAsync(eventId, role);
        }
    }
}
