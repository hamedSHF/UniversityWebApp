using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model;
using UniversityWebApp.Model.RequestTypes;
using UniversityWebApp.Model.ResponseTypes;

namespace UniversityWebApp.EndPoints
{
    public static class MajorEndpoints
    {
        public static RouteGroupBuilder MapMajorEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapPost("/add", AddMajor);
            builder.MapGet("/", GetMajors);
            builder.MapPut("/", UpdateMajor);
            builder.MapDelete("/{majorName:required}", DeleteMajor);
            builder.MapGet("/search/{query:required}", SearchMajor);

            return builder;
        }
        public static async Task<Results<Created,BadRequest>> AddMajor(
            AddMajorRequest major, 
            [FromServices] IMajorRepository majorRepository)
        {
            try
            {
                await majorRepository.Add(new Model.Major { Title = major.Title });
                await majorRepository.SaveChanges();
                return TypedResults.Created();
            }
            catch
            {
                return TypedResults.BadRequest();
            }
        }
        public static async Task<Results<Ok<IEnumerable<MajorResponse>>, BadRequest>> GetMajors(
            [FromQuery(Name ="topics")] bool? includeTopics,
            [FromServices] IMajorRepository majorRepository)
        {
            IEnumerable<MajorResponse> majors;
            if (includeTopics.HasValue && includeTopics.Value)
            {
                majors = (await majorRepository.GetAllWithTopics())
                    .Select(x => new MajorResponse
                    (x.Title,
                    x.Topics.Select(z => z.Title).ToList()));
            }
            else
            {
                majors = (await majorRepository.GetAll())
                    .Select(x => new MajorResponse(x.Title))
                    .OrderBy(x => x.title);
            }

            return TypedResults.Ok<IEnumerable<MajorResponse>>(majors);
        }
        public static async Task<Results<Ok,BadRequest<string>>> UpdateMajor(
            UpdateMajorRequest majorRequest,
            [FromServices] IMajorRepository majorRepository)
        {
            var oldMajor = await majorRepository.GetMajor(majorRequest.OldTitle);
            if (oldMajor is not null)
            {
                var updatedMajor = Major.CreateMajor(majorRequest.NewTitle,
                    oldMajor.Students.ToList(),
                    oldMajor.Topics.ToList());
                majorRepository.Delete(oldMajor);
                await majorRepository.Add(updatedMajor);
                await majorRepository.SaveChanges();
                return TypedResults.Ok();
            }
            else
            {
                return TypedResults.BadRequest("Major not found");
            }
        }
        public static async Task<Results<Ok,BadRequest<string>>> DeleteMajor(
            string majorName,
            [FromServices] IMajorRepository majorRepository)
        {
            var major = await majorRepository.GetMajor(majorName);
            if (major is null)
                return TypedResults.BadRequest($"{majorName} is not founded!");
            majorRepository.Delete(major);
            await majorRepository.SaveChanges();
            return TypedResults.Ok();
        }

        public static async Task<IEnumerable<string>> SearchMajor(
            string query,
            IMajorRepository majorRepository)
        {
            var results = await majorRepository.GetAll();
            var majors = results.Where(x => x.Title.Contains(query)).Select(x => x.Title);
            return majors;
        }
    }
}
