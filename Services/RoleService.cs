using AutoMapper;
using UTEvents.Entities;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;

namespace UTEvents.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllRolesAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(roles);
        }

        public async Task<RoleDto?> GetRoleByIdAsync(Guid roleId)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
            return role == null ? null : _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> CreateRoleAsync(RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            var created = await _roleRepository.CreateRoleAsync(role);
            return created ? roleDto : null;
        }

        public async Task<bool> UpdateRoleAsync(Guid roleId, RoleDto roleDto)
        {
            var role = _mapper.Map<Role>(roleDto);
            role.Id = roleId;
            return await _roleRepository.UpdateRoleAsync(role);
        }

        public async Task<bool> DeleteRoleAsync(Guid roleId)
        {
            return await _roleRepository.DeleteRoleAsync(roleId);
        }
    }
}
