using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using UniversityWebApp.Model.DTOs;

namespace UniversityWebApp.Model
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string TeacherUserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly StartAt { get; set; }
        public DateOnly EndAt { get; set; }
        public string Degree { get; set; }
        public ICollection<Course> Courses { get; private set; } = new List<Course>();

        public static Teacher CreateTeacher(string userName, AddTeacherDto dto) =>
            new Teacher
            {
                TeacherUserName = userName,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                StartAt = dto.StartAt,
                EndAt = dto.EndAt,
                Degree = dto.Degree
            };
        public string GetFullName() =>  FirstName + LastName;
    }
}
