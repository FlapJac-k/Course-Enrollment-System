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

            // Services
            services.AddScoped<IStudentService, StudentService>();

            return services;
        }
    }
}
