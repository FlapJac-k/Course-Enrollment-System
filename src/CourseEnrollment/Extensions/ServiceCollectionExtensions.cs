using CourseEnrollment.Business.Interfaces;
using CourseEnrollment.Business.Services;
using CourseEnrollment.Data.Interfaces;
using CourseEnrollment.Data.Repositories;

namespace CourseEnrollment.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services)
        {
            // Repositories
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();


            // Services
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService, CourseService>();

            return services;
        }
    }
}
