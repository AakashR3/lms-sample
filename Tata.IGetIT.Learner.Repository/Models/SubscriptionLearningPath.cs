using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class SubscriptionLearningPath
    {
        public int PathID { get; set; }
        public string Name { get; set; }
        public int CoursesCount { get; set; }
        public int AssessmentsCount { get; set; }
        public int AccountID { get; set; }
        public string Description { get; set; }
        public int SubcategoryID { get; set; }
        public int TypeID { get; set; }
    }
}
