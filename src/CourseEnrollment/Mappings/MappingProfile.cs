using AutoMapper;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Models;

namespace CourseEnrollment.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();
        }
    }
}
