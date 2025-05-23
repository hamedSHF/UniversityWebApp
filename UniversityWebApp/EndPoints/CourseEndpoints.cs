using MassTransit.Initializers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Model.RequestTypes.CourseRequest;
using UniversityWebApp.Model.ResponseTypes;
using UniversityWebApp.Services.CodeGenerator;

namespace UniversityWebApp.EndPoints
{
    public static class CourseEndpoints
    {
        public static RouteGroupBuilder MapCourseEndpoints(this  RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetCourses);
            builder.MapPost("/", AddCourse);
            builder.MapDelete("/{courseId:int}", DeleteCourse);
            return builder;
        }
        public static async Task<Ok<IEnumerable<CourseResponse>>> GetCourses(
            ICourseRepository courseRepository)
        {
            return TypedResults.Ok((await courseRepository.GetAll()).Select(x => new CourseResponse
            {
                CourseCode = x.CourseCode,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Details = x.CourseDetails
            }));
        }
        public static async Task<Results<Created<int>, BadRequest<string>>> AddCourse(
            [FromBody] AddCourseRequest courseRequest,
            CourseCodeGenerator codeGenerator,
            ICourseTopicRepository topicRepository,
            ICourseRepository courseRepository,
            ITeacherRepository teacherRepository)
        {
            //TODO: Validate request
            var topic = await topicRepository.GetById((ushort)courseRequest.TopicId);
            var teacher = await teacherRepository.GetTeacherById(courseRequest.TeacherId.ToString());
            if(topic != null)
            {
                var count = await courseRepository.CountAll();
                var codeDict = new Dictionary<string, string>
                {
                    {"prefix", topic.Title.Take(2).ToString()},
                    {"body", DateTime.Now.Year.ToString() + count},
                };
                var addedCourse = await courseRepository.Add(Course.CreateCourse(
                    codeGenerator.GenerateCode(codeDict).GetCode(),
                    courseRequest.StartTime,
                    courseRequest.EndTime,
                    topic,
                    teacher,
                    courseRequest.Details
                    ));
                await courseRepository.SaveChanges();
                return TypedResults.Created(string.Empty ,addedCourse.CourseID);
            }
            return TypedResults.BadRequest($"Topic {courseRequest.TopicId} is not founded");
        }
        public static async Task<Results<Ok, BadRequest<string>>> DeleteCourse(
            int courseId,
            ICourseTopicRepository topicRepository,
            ICourseRepository courseRepository)
        {
            //TODO: Validate request
            var course = await courseRepository.GetById((ushort)courseId);
            if (course != null)
            {
                courseRepository.Delete(course);
                await courseRepository.SaveChanges();
                return TypedResults.Ok();
            }
            return TypedResults.BadRequest($"Course {courseId} not founded");
        }
    }
}
