using Data.DTOs.EntityDTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Entities;
namespace Services.Mappings
{
    public class TaskProfile:Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDTO, Data.Entities.Task>()
           .ForMember(src => src.Id, opt => opt.MapFrom(dto => dto.Id))
           .ForMember(src => src.Title, opt => opt.MapFrom(dto => dto.Title))
           .ForMember(src => src.IsCompleted, opt => opt.MapFrom(dto => dto.IsCompleted))
           .ForMember(src => src.DueDate, opt => opt.MapFrom(dto => dto.DueDate))
           .ForMember(src => src.Description, opt => opt.MapFrom(dto => dto.Description))
           .ReverseMap();
        }
    }
}
