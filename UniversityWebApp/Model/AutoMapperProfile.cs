using AutoMapper;
using UniversityWebApp.Model.DTOs;
namespace UniversityWebApp.Model
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddStudentDto, Student>();
            CreateMap<Student, StudentDto>();
        }
    }
}
