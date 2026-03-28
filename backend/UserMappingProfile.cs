using AutoMapper;
using models.Dtos.EmoteSeedDtos;
using models.Dtos.LogDtos;
using models.Dtos.PetDtos;
using models.Dtos.TaskDtos;
using models.Dtos.UserDtos;
using models.Entities; // Corrected namespace for UserRole

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<UserAppRelatedInfoDto, UserEntity>();
        CreateMap<UserEntity, UserTasks>();
        CreateMap<UserEmailDto, UserEntity>();
        CreateMap<UserEntity, PetNameDto>();
        CreateMap<TaskItemDto, TaskEntity>();
        CreateMap<TaskEntity, TaskItemDto>();
        CreateMap<PetEntity, PetInfoDto>();
        CreateMap<TaskEntity, TaskItemDto>();
        CreateMap<LogEntity, LogInfoDto>();
        CreateMap<EmoteSeedEntity, EmoteInfoDto>();
    }
}