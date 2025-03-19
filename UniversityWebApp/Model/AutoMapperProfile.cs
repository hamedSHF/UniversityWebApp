using AutoMapper;
using UniversityWebApp.Model.DTOs;
using UniversityWebApp.ViewModels;
namespace UniversityWebApp.Model
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DetailedStudentViewModel, Student>();
        }
    }
}
