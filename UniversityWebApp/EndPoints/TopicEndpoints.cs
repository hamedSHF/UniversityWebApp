using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Model.RequestTypes;

namespace UniversityWebApp.EndPoints
{
    public static class TopicEndpoints
    {
        public static RouteGroupBuilder MapTopicEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/{major:required}", GetTopicsByMajor);
            builder.MapPost("/add", AddTopic);
            builder.MapPost("/delete", DeleteTopic);
            return builder;
        }

        public static async Task<Results<Ok<IEnumerable<string>>, BadRequest<string>>> GetTopicsByMajor(
            string major,
            IMajorRepository majorRepository)
        {
            var existed = await majorRepository.Exists(major);
            if(existed)
            {
                return TypedResults.Ok((await majorRepository.GetMajor(major, true))
                    .Topics.Select(x => x.Title));
            }
            return TypedResults.BadRequest($"{major} not founded");
        }
        public static async Task<Results<Created, BadRequest<string>>> AddTopic(
            [FromBody] TopicRequest topicRequest,
            IMajorRepository majorRepository,
            ICourseTopicRepository topicRepository)
        {
            var major = await majorRepository.GetMajor(topicRequest.MajorName);
            if(major != null)
            {
                await topicRepository.Add(CourseTopics.CreateTopic(
                    topicRequest.TopicName,
                    new List<Major> { major }));
                await topicRepository.SaveChanges();
                return TypedResults.Created();
            }
            else
            {
                return TypedResults.BadRequest($"{topicRequest.MajorName} not founded");
            }
        }
        public static async Task<Results<Ok, BadRequest<string>>> DeleteTopic(
            [FromBody] TopicRequest request,
            ICourseTopicRepository topicRepository,
            IMajorRepository majorRepository)
        {
            var major = await majorRepository.GetMajor(request.MajorName, true);
            if(major != null)
            {
                var topic = major.Topics.
                    FirstOrDefault(x => x.Title == request.TopicName);
                if(topic != null)
                {
                    major.Topics.Remove(topic);
                    majorRepository.Update(major);
                    await topicRepository.SaveChanges();
                    return TypedResults.Ok();
                }
                return TypedResults.BadRequest($"{request.TopicName} not founded");
            }
            else
            {
                return TypedResults.BadRequest($"{request.MajorName} not founded");
            }
        }
    }
}
