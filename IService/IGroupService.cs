using UTEvents.Models;
using UTEvents.Requests;

namespace UTEvents.IService
{
    public interface IGroupService
    {
       Task<IEnumerable<GroupDto>> GetAllGroupsAsync();
        Task<GroupDto?> GetGroupAsync(Guid groupId);
        Task<GroupDto?> CreateGroupAsync(GroupRequest groupRequest);
        Task<bool> UpdateGroupAsync(Guid groupId, GroupRequest groupRequest);
        Task<bool> DeleteGroupAsync(Guid groupId);
    }
}
