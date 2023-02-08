using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Training
    {
        public int TrainingID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name cannot be null or empty")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description cannot be null or empty")]
        public string Description { get; set; }

        public int Category { get; set; }
        public int SubCategory { get; set; }

        [Required]
        public int CourseID { get; set; }
        public int NoOfSession { get; set; }
        public string Attendee { get; set; }
        public List<string> Attendees { get; set; }
        public string CreatedDate { get; set; } = String.Empty;
        public string ModifiedDate { get; set; } = String.Empty;
        public string IsPrivileged { get; set; } = String.Empty;

    }


    public class TrainingGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Training> TrainingData { get; set; }
    }
}
