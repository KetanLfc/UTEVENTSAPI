using AutoMapper;
using UTEvents.Entities;
using UTEvents.Models;
using UTEvents.Requests;

namespace UTEvents.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Event Mapping
            CreateMap<Event, EventDto>().ReverseMap();
            CreateMap<EventRequest, Event>();

            // EventCategory Mapping
            CreateMap<EventCategory, EventCategoryDto>().ReverseMap();

            // User Mapping
            CreateMap<User, UserDto>()
      .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.EmailConfirmed))

    // We already have this for RoleName:
    .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.RoleName))

    // Then let ReverseMap handle the reverse direction
    .ReverseMap();


            CreateMap<UserRequest, User>();

            // Group Mapping
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<GroupRequest, Group>();

            CreateMap<UserRequest, User>()
    .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.GroupId));


            // Location Mapping
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<LocationRequest, Location>();

            // Role Mapping
            CreateMap<Role, RoleDto>().ReverseMap();

            // UserEvent Mapping
            CreateMap<UserEvent, UserEventDto>().ReverseMap();

            // AllowedEventRole Mapping
            CreateMap<AllowedEventRole, AllowedEventRoleDto>().ReverseMap();
        }
    }
}
