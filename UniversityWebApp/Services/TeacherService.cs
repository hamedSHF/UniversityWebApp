using UniversityWebApp.Model;
using UniversityWebApp.Model.DTOs;
using UniversityWebApp.DataAccess.Interfaces;
using UniversityWebApp.Services.Registration;
using Common.Requests;

namespace UniversityWebApp.Services
{
    public class TeacherService : IRegistrationService<AddTeacherDto>
    {
        public ITeacherRepository teacherRepository;
        public HttpClient httpClient;
        public ILogger<TeacherService> teacherServiceLogger;
        public TeacherService(ITeacherRepository teacherRepository, 
            HttpClient httpClient,
            ILogger<TeacherService> teacherLogger)
        {
            this.teacherRepository = teacherRepository;
            this.httpClient = httpClient;
            this.teacherServiceLogger = teacherLogger;
        }
        public async Task<RegistrationResult> RegisterUser(AddTeacherDto teacherDto)
        {
            try
            {
                var exist = await teacherRepository.Exists(teacherDto.FirstName, teacherDto.LastName);
                if (exist)
                    return new RegistrationResult { Success = false, Content = $"{teacherDto.FirstName} {teacherDto.LastName} is already registered" };
                string username = UserNameGenerator.
                    GenerateTeacherUsername((await teacherRepository.CountAll()),
                    teacherDto.FirstName[0].ToString() + teacherDto.LastName[0]);
                var teacher = Teacher.CreateTeacher(username, teacherDto);
                var addedTeacher = await teacherRepository.Add(teacher);
                await teacherRepository.SaveChanges();
                var response = await httpClient.PostAsJsonAsync<CreateTeacherRequest>("create", new CreateTeacherRequest
                {
                    Firstname = teacherDto.FirstName,
                    Lastname = teacherDto.LastName,
                    Username = username,
                });
                if(!response.IsSuccessStatusCode)
                {
                    var result = await FileSaverService.SaveJson<string>(new Dictionary<string, string>
                    {
                        {nameof(addedTeacher.TeacherUserName), username},
                        {"TimeStamp",$"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}-" +
                    $"{DateTime.Now.Hour}-{DateTime.Now.Minute}-{DateTime.Now.Second}" }
                    }, FileInfoConstants.RabbitCachePath, $"{FileInfoConstants.TeacherRabbitCache}.json");
                    return result ? new RegistrationResult { Success = true} : 
                        new RegistrationResult { Success = false,Content = "An error in caching teacher information" };
                }
                return new RegistrationResult { Success = true, Content = "New teacher registered successfully" };
            }
            catch (Exception ex)
            {
                teacherServiceLogger.LogError(ex, "Error occured while adding teacher.");
                throw;
            }
        }
    }
}
