using System.ComponentModel.DataAnnotations;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class ReturnResponse<T>
    {
        [Required]
        public string Message { get; set; }
        public string Output { get; set; }
        public T Data { get; set; }
    }
}
