using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Model.RequestTypes;
using UniversityWebApp.Model.ResponseTypes;

namespace UniversityWebApp.EndPoints
{
    public static class MajorEndpoints
    {
        public static RouteGroupBuilder MapMajorEndpoints(this RouteGroupBuilder builder)
        {
            builder.MapPost("/", AddMajor);
            builder.MapGet("/", GetMajors);
            builder.MapPut("/", UpdateMajor);
            builder.MapDelete("/{majorName:required}", DeleteMajor);

            return builder;
        }
        public static async Task<Results<Created,BadRequest>> AddMajor(
            AddMajorRequest major, 
            [FromServices] IMajorRepository majorRepository)
        {
            await majorRepository.Add(new Model.Major { Title =  major.Title });
            await majorRepository.SaveChanges();
            return TypedResults.Created();
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
                    .Select(x => new MajorResponse(x.Title));
            }

            return TypedResults.Ok<IEnumerable<MajorResponse>>(majors);
        }
        public static async Task<Results<Ok,BadRequest<string>>> UpdateMajor(
            UpdateMajorRequest majorRequest,
            [FromServices] IMajorRepository majorRepository)
        {
            var oldMajor = await majorRepository.FindMajor(majorRequest.OldTitle);
            if (oldMajor is not null)
            {
                oldMajor.Title = majorRequest.NewTitle;
                majorRepository.Update(oldMajor);
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
            var major = await majorRepository.FindMajor(majorName);
            if (major is null)
                return TypedResults.BadRequest($"{majorName} is not founded!");
            majorRepository.Delete(major);
            await majorRepository.SaveChanges();
            return TypedResults.Ok();
        }
    }
}
