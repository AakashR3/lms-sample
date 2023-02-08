using System.ComponentModel.DataAnnotations;
using Tata.IGetIT.Learner.Repository.Constants;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class CompetencyListData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int CompetencyTypeId { get; set; }
        public string CompetencyTypeName { get; set; }
        public int IsPublic { get; set; }
        public int IsMandatory { get; set; }
        public int AccountID { get; set; }
    }

    public class Competency
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int CompetencyTypeId { get; set; }
        public string CompetencyName { get; set; }
        public int IsPublic { get; set; }
        public int IsMandatory { get; set; }
    }

    public class AdminCompetency
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int Mode { get; set; }    
        public int CompetencyType {get; set; }    
    }

    public class CompetencyGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<CompetencyListData> CompetencyListData { get; set; }
    }

    public class DeleteCompetency : IValidatableObject
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();
            if (ID <= 0)
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_ID));
            }
            // if (string.IsNullOrEmpty(CompetencyID))
            // {
            //     validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_COMPETENCYID));
            // }

            return validationErrors;
        }
    }

    public class CompetencyLevel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompetencyId { get; set; }
        public int Status { get; set; }
        public string Remarks { get; set; }
        public int UserID { get; set; }
    }

    public class CompetencyType
    {
        public int ID { get; set; }
        public string CompetencyTypeName { get; set; }        
    }
}