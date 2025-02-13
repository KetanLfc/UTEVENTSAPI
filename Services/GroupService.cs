using AutoMapper;
using UTEvents.Entities;
using UTEvents.IRepository;
using UTEvents.IService;
using UTEvents.Models;
using UTEvents.Requests;

namespace UTEvents.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupDto>> GetAllGroupsAsync()
        {
            var groups = await _groupRepository.GetAllGroupsAsync();
            return _mapper.Map<IEnumerable<GroupDto>>(groups);
        }

        public async Task<GroupDto?> GetGroupAsync(Guid groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            return group == null ? null : _mapper.Map<GroupDto>(group);
        }

        public async Task<GroupDto?> CreateGroupAsync(GroupRequest groupRequest)
        {
            var group = _mapper.Map<Group>(groupRequest);
            var created = await _groupRepository.CreateGroupAsync(group);
            return created ? _mapper.Map<GroupDto>(group) : null;
        }

        public async Task<bool> UpdateGroupAsync(Guid groupId, GroupRequest groupRequest)
        {
            var group = _mapper.Map<Group>(groupRequest);
            group.Id = groupId;
            return await _groupRepository.UpdateGroupAsync(group);
        }

        public async Task<bool> DeleteGroupAsync(Guid groupId)
        {
            return await _groupRepository.DeleteGroupAsync(groupId);
        }
    }
}
