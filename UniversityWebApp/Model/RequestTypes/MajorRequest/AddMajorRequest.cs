using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model.RequestTypes.MajorRequest
{
    public class AddMajorRequest
    {
        [RegularExpression("^[a-zA-z]+$")]
        public string Title { get; set; }
    }
}
