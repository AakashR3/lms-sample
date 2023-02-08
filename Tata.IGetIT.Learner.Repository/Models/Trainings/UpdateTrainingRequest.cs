using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class UpdateTrainingRequest
    {
        [Required]
        public int TrainingID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name can not be null or empty")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Description can not be null or empty")]
        public string Description { get; set; }

        [Required]
        public int CourseID { get; set; }

        /// <summary>
        /// List of Attendees
        /// </summary>
        [Required]
        public List<string> Attendees { get; set; }

    }
}
