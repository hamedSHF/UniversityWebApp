using System.ComponentModel.DataAnnotations;

namespace UniversityWebApp.Model.RequestTypes
{
    public class AddMajorRequest
    {
        [RegularExpression("^[a-zA-z]+$")]
        public string Title { get; set; }
    }
}
