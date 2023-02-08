using System.ComponentModel.DataAnnotations;
using Tata.IGetIT.Learner.Repository.Constants;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class RolesListData
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public int TotalExpMonths { get; set; }
        public string ExpLevel { get; set; }
    }

    public class Role
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string RoleName { get; set; }
        public int TotalExpMonths { get; set; }
        public int IsPublic { get; set; }
        public int Status { get; set; }
    }

    public class AdminRole
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int Mode { get; set; }
    }

    public class RolesGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<RolesListData> RolesListData { get; set; }
    }

    public class DeleteRole : IValidatableObject
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
            // if (string.IsNullOrEmpty(RoleID))
            // {
            //     validationErrors.Add(new ValidationResult(LearnerAppConstants.INVALID_ROLEID));
            // }

            return validationErrors;
        }
    }

    public class MultipleRoles
    {
        public string ReturnMessage { get; set; }
        public int ID { get; set; }
        public int UserID { get; set; }
        public string RoleName { get; set; }
        public int TotalExpMonths { get; set; }
        public int IsPublic { get; set; }
        public int Status { get; set; }
        public int Success { get; set; }
    }

    public class PublicRoles
    {
        public int FailedCount { get; set; }
        public int SuccessCount { get; set; }
        public List<MultipleRoles> Role { get; set; }
    }
}