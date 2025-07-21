using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model.ResponseTypes;

namespace UniversityWebApp.EndPoints
{
    public static class TeacherEndpoints
    {
        public static RouteGroupBuilder MapTeacherEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetTeachers);

            return builder;
        }
        public static async Task<Results<Ok<List<TeacherResponse>>,BadRequest>> GetTeachers([FromServices] ITeacherRepository teacherRepository,
            [FromQuery] bool includeCourses = false,
            [FromQuery] bool includePrivateInfo = false)
        {
            var teachers = await teacherRepository.GetAll(includeCourses);
            if (teachers.Count() == 0)
                return TypedResults.BadRequest();
            if (includePrivateInfo)
            {
                return TypedResults.Ok(teachers.Select(x => new TeacherResponse
                {
                    Name = x.GetFullName(),
                    SelectedProperties = new Dictionary<string, object>
                    {
                        { nameof(x.BirthDate), x.BirthDate },
                        { nameof(x.StartAt), x.StartAt },
                        { nameof(x.Degree), x.Degree },
                        { nameof(x.EndAt), x.EndAt },
                    }
                }).ToList());
            }
            return TypedResults.Ok(teachers.Select(x => new TeacherResponse
            {
                Name = x.GetFullName(),
                SelectedProperties = new Dictionary<string, object>
                {
                    {nameof(x.Courses), x.Courses }
                }
            }).ToList());
        }
    }
}
