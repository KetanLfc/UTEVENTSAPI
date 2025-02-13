using UTEvents.Entities;

namespace UTEvents.IRepository
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetAllGroupsAsync();
        Task<Group?> GetGroupByIdAsync(Guid groupId);
        Task<bool> CreateGroupAsync(Group group);
        Task<bool> UpdateGroupAsync(Group group);
        Task<bool> DeleteGroupAsync(Guid groupId);
    }
}
