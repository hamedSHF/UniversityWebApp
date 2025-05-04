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
            builder.MapPost("/delete/{topic:required}", DeleteTopic);
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
            [FromBody] AddTopicRequest topicRequest,
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
            string topic,
            ICourseTopicRepository topicRepository)
        {
            var fetchedTopic = await topicRepository.GetTopic(topic);
            if(fetchedTopic != null)
            {
                topicRepository.Delete(fetchedTopic);
                await topicRepository.SaveChanges();
                return TypedResults.Ok();
            }
            else
            {
                return TypedResults.BadRequest($"{topic} not founded");
            }
        }
    }
}
