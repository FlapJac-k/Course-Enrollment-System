using AutoMapper;
using CourseEnrollment.Data.Entities;
using CourseEnrollment.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CourseEnrollment.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentViewModel>();
            CreateMap<StudentViewModel, Student>();

            CreateMap<Course, CourseViewModel>()
             .ForMember(dest => dest.AvailableSlots,
                        opt => opt.MapFrom(src => src.MaximumCapacity - src.Enrollments.Count))
             .ForMember(dest => dest.EnrollmentCount,
                        opt => opt.MapFrom(src => src.Enrollments.Count));

            CreateMap<CourseViewModel, Course>()
                .ForMember(dest => dest.Enrollments, opt => opt.Ignore());

            CreateMap<StudentCourseEnrollment, EnrollmentViewModel>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.FullName))
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dest => dest.AvailableSlots, opt => opt.Ignore());

            CreateMap<Student, SelectListItem>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.FullName} ({src.Email})"));

            // Course → SelectListItem
            CreateMap<Course, SelectListItem>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => $"{src.Title} (Available: {src.MaximumCapacity - src.Enrollments.Count})"));
        }
    }
}
