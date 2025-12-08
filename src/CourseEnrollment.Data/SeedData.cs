using CourseEnrollment.Data.Entities;

namespace CourseEnrollment.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Students.Any() || context.Courses.Any())
            {
                return; 
            }

            var students = new Student[]
            {
                new Student
                {
                    FullName = "Ahmed Mohamed",
                    Email = "ahmed.mohamed@example.com",
                    Birthdate = new DateTime(2000, 5, 15),
                    NationalId = "12345678901234",
                    PhoneNumber = "01234567890",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Student
                {
                    FullName = "Fatima Ali",
                    Email = "fatima.ali@example.com",
                    Birthdate = new DateTime(2001, 8, 22),
                    NationalId = "23456789012345",
                    PhoneNumber = "01123456789",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Student
                {
                    FullName = "Mohamed Hassan",
                    Email = "mohamed.hassan@example.com",
                    Birthdate = new DateTime(1999, 3, 10),
                    NationalId = "34567890123456",
                    PhoneNumber = "01012345678",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Student
                {
                    FullName = "Sara Ibrahim",
                    Email = "sara.ibrahim@example.com",
                    Birthdate = new DateTime(2002, 11, 5),
                    NationalId = "45678901234567",
                    PhoneNumber = "01512345678",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Student
                {
                    FullName = "Omar Khaled",
                    Email = "omar.khaled@example.com",
                    Birthdate = new DateTime(2000, 7, 18),
                    NationalId = "56789012345678",
                    PhoneNumber = null,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Students.AddRange(students);
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course
                {
                    Title = "Introduction to Programming",
                    Description = "Learn the fundamentals of programming with C#",
                    MaximumCapacity = 5,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Web Development with ASP.NET",
                    Description = "Build modern web applications using ASP.NET MVC",
                    MaximumCapacity = 12,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Database Design",
                    Description = "Learn how to design and implement databases",
                    MaximumCapacity = 20,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "Entity Framework Core",
                    Description = "Master Entity Framework Core for data access",
                    MaximumCapacity = 15,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                },
                new Course
                {
                    Title = "JavaScript Fundamentals",
                    Description = "Learn JavaScript and jQuery for dynamic web pages",
                    MaximumCapacity = 3,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            context.Courses.AddRange(courses);
            context.SaveChanges();

            var enrollments = new StudentCourseEnrollment[]
            {
                new StudentCourseEnrollment
                {
                    StudentId = students[0].Id,
                    CourseId = courses[0].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    UpdatedAt = DateTime.UtcNow.AddDays(-10)
                },
                new StudentCourseEnrollment
                {
                    StudentId = students[0].Id,
                    CourseId = courses[1].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-8),
                    UpdatedAt = DateTime.UtcNow.AddDays(-8)
                },
                new StudentCourseEnrollment
                {
                    StudentId = students[1].Id,
                    CourseId = courses[0].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-7),
                    UpdatedAt = DateTime.UtcNow.AddDays(-7)
                },
                new StudentCourseEnrollment
                {
                    StudentId = students[1].Id,
                    CourseId = courses[2].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    UpdatedAt = DateTime.UtcNow.AddDays(-5)
                },
                new StudentCourseEnrollment
                {
                    StudentId = students[2].Id,
                    CourseId = courses[1].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-3),
                    UpdatedAt = DateTime.UtcNow.AddDays(-3)
                },
                new StudentCourseEnrollment
                {
                    StudentId = students[3].Id,
                    CourseId = courses[3].Id,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    UpdatedAt = DateTime.UtcNow.AddDays(-2)
                }
            };

            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();
        }
    }
}
