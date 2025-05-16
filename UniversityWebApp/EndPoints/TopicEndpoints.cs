using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Model.RequestTypes;
using UniversityWebApp.Model.ResponseTypes;

namespace UniversityWebApp.EndPoints
{
    public static class TopicEndpoints
    {
        public static RouteGroupBuilder MapTopicEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetTopics);
            builder.MapGet("/{major:required}", GetTopicsByMajor);
            builder.MapPost("/addTopicMajor", AddTopicMajor);
            builder.MapPost("/addTopic", AddTopic);
            builder.MapDelete("/deleteTopicMajor", DeleteTopicMajor);
            builder.MapDelete("/deleteTopic/{id:required}", DeleteTopic);
            builder.MapPut("/updateTopicMajor", UpdateTopicMajor);
            builder.MapPut("/updateTopic", UpdateTopic);
            return builder;
        }
        public static async Task<Ok<IEnumerable<TopicResponse>>> GetTopics(
            ICourseTopicRepository topicRepository)
        {
            return TypedResults.Ok((await topicRepository.GetAll()).Select(x => new TopicResponse
            {
                Id = x.TopicId,
                Title = x.Title
            }));
        }
        public static async Task<Results<Ok<IEnumerable<string>>, BadRequest<string>>> GetTopicsByMajor(
            string major,
            IMajorRepository majorRepository)
        {
            var existed = await majorRepository.Exists(major);
            if (existed)
            {
                return TypedResults.Ok((await majorRepository.GetMajor(major, true))
                    .Topics.Select(x => x.Title));
            }
            return TypedResults.BadRequest($"{major} not founded");
        }
        public static async Task<Results<Created, BadRequest<string>>> AddTopicMajor(
            [FromBody] TopicRequest topicRequest,
            IMajorRepository majorRepository,
            ICourseTopicRepository topicRepository)
        {
            var major = await majorRepository.GetMajor(topicRequest.MajorName);
            if (major != null)
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
        public static async Task<Results<Created<ushort>, BadRequest<string>>> AddTopic(
            [FromBody] string title,
            ICourseTopicRepository topicRepository)
        {
            if (string.IsNullOrWhiteSpace(title))
                return TypedResults.BadRequest("Title is not correct");
            var addedEntity = await topicRepository.Add(CourseTopics.CreateTopic(title));
            await topicRepository.SaveChanges();
            return TypedResults.Created(string.Empty, addedEntity.TopicId);
        }
        public static async Task<Results<Ok, BadRequest<string>>> DeleteTopicMajor(
            [FromBody] TopicRequest request,
            ICourseTopicRepository topicRepository,
            IMajorRepository majorRepository)
        {
            var major = await majorRepository.GetMajor(request.MajorName, true);
            if (major != null)
            {
                var topic = major.Topics.
                    FirstOrDefault(x => x.Title == request.TopicName);
                if (topic != null)
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
        public static async Task<Results<Ok, BadRequest<string>>> DeleteTopic(
            int id,
            ICourseTopicRepository topicRepository)
        {
            if (id < ushort.MaxValue && id > ushort.MinValue)
            {
                var entity = await topicRepository.GetTopic((ushort)id);
                if (entity != null)
                {
                    topicRepository.Delete(entity);
                    await topicRepository.SaveChanges();
                    return TypedResults.Ok();
                }
                return TypedResults.BadRequest("Topic not founded");
            }
            return TypedResults.BadRequest("Id is not valid");
        }
        public static async Task<Results<Ok, BadRequest<string>>> UpdateTopic(
            [FromBody] UpdateTopicRequest request,
            ICourseTopicRepository topicRepository)
        {
            if(request != null && request.Id > 0 && !string.IsNullOrEmpty(request.Name))
            {
                var topic = await topicRepository.GetTopic((ushort)request.Id);
                if(topic != null)
                {
                    topic.Title = request.Name;
                    topicRepository.Update(topic);
                    await topicRepository.SaveChanges();
                    return TypedResults.Ok();
                }
                return TypedResults.BadRequest($"Topic with id {request.Id} not founded");
            }
            return TypedResults.BadRequest("Wrong data recieved");
        }
        public static async Task<Results<Ok, BadRequest<string>>> UpdateTopicMajor(
            [FromBody] UpdateTopicMajorRequest request,
            IMajorRepository majorRepository,
            ICourseTopicRepository topicRepository)
        {
            var major = await majorRepository.GetMajor(request.MajorName);
            if (major != null)
            {
                var topic = await topicRepository.GetTopic((ushort)request.Id);
                if (topic != null)
                {
                    topicRepository.Delete(topic);
                    await topicRepository.Add(CourseTopics.CreateTopic(
                        request.Name,
                        new List<Major>
                    {
                        major
                    }));
                    await topicRepository.SaveChanges();
                    return TypedResults.Ok();
                }
                return TypedResults.BadRequest($"{request.Id} not founded");
            }
            else
            {
                return TypedResults.BadRequest($"{request.MajorName} not founded");
            }
        }
    }
}
