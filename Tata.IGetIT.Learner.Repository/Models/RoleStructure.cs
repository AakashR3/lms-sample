using System.ComponentModel.DataAnnotations;
using Tata.IGetIT.Learner.Repository.Constants;

namespace Tata.IGetIT.Learner.Repository.Models
{

    public class RolesStructureListData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public int IsPublic { get; set; }
        public int PathCount { get; set; }
        public string CustomAttribute1 { get; set; }
        public string CustomAttribute2 { get; set; }
        public string CustomAttribute3 { get; set; }
        public string CustomAttribute4 { get; set; }
        public string CustomAttribute5 { get; set; }
        public string ResponseJSON { get; set; }
    }

    public class RolesStructure
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int UserID { get; set; }
        public int IsPublic { get; set; }
        public int PathCount { get; set; }
        public string ResponseJSON { get; set; }
        public List<UT_RoleStructureLevelMap> RoleStructureLevelMapData { get; set; }
        //public IEnumerable<RoleCompetencyLPMap> RoleCompetencyLPMapData { get; set; }
    }

    public class AdminRoleStructure
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int Mode { get; set; }
        public string SearchText { get; set; }
    }

    public class LearningPathRoleMapping
    {
        public int PathID { get; set; }
        public string LearningPathName { get; set; }
    }

    public class RolesStructureGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<RolesStructureListData> RolesStructureListData { get; set; }
    }

    public class DeleteStructure : IValidatableObject
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

    public class RolesStructureParam
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PathCount { get; set; }
        public int IsPublic { get; set; }
        public string ResponseJSON { get; set; }
        // public string Attr1 { get; set; }
        // public string Attr2 { get; set; }
        // public string Attr3 { get; set; }
        // public string Attr4 { get; set; }
        // public string Attr5 { get; set; }
        public int UserID { get; set; }
        public List<UT_RoleStructureLevelMap> RoleStructureLevelMapData { get; set; }
        public List<UT_RoleCompetencyLevelMap> RoleCompetencyLevelMapData { get; set; }
        public List<UT_RoleCompetencyLPMap> RoleCompetencyLPMapData { get; set; }
    }

    public class UT_RoleStructureLevelMap
    {
        public int ID { get; set; }
        public int RolesStructureID { get; set; }
        public string RoleRefID { get; set; }
        public int RoleID { get; set; }
        public int Level { get; set; }
        public string ParentId { get; set; }
        public int IsEquivalent { get; set; }
        public int Status { get; set; }
        // public string Attr1 { get; set; }
        // public string Attr2 { get; set; }
        // public List<UT_RoleCompetencyLevelMap> RoleCompetencyLevelMapData { get; set; }
    }

    public class UT_RoleCompetencyLevelMap
    {
        public int ID { get; set; }
        public int RolesStructureID { get; set; }
        public int RolesStructureLevelMapID { get; set; }
        public int RoleID { get; set; }
        public int CompetencyID { get; set; }
        public int CompetencyLevelID { get; set; }
        //public List<UT_RoleCompetencyLPMap> RoleCompetencyLPMapData { get; set; }
    }

    public class UT_RoleCompetencyLPMap
    {
        public int ID { get; set; }
        public int RoleCompetencyLevelMapID { get; set; }
        public int CompetencyID { get; set; }
        public int PathID { get; set; }
    }

    public class RoleCompetencyLPMap
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int RoleStructureID { get; set; }
        public int RoleID { get; set; }
        public int CompetencyID { get; set; }
        public int LevelID { get; set; }
        public int LPID { get; set; }
    }

    public class ProcedureReturnParameters
    {
        public int ReturnValue { get; set; }
        public string ReturnMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorSeverity { get; set; }
        public string ErrorState { get; set; }
    }

    public class UserRoleMap
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int RoleId { get; set; }
        public int TargetRoleId { get; set; }
        public int RoleStructureId { get; set; }
    }

    public class UserRoleMapList
    {
        public int RoleMapId { get; set; }
        public int RoleId { get; set; }
        public int TargetRoleId { get; set; }
        public int RoleStructureId { get; set; }
    }

    public class UserCompetencyMap
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int CompetencyId { get; set; }
        public int CompetencyLevelId { get; set; }
        public int ExpYear { get; set; }
        public int ExpMonth { get; set; }
        public DateTime LastWorkedDate { get; set; }
    }

    public class UserCompetencyMapList
    {
        public int CompetencyMapId { get; set; }
        public int CompetencyId { get; set; }
        public int CompetencyLevelId { get; set; }
        public int ExpYear { get; set; }
        public int ExpMonth { get; set; }
        public DateTime LastWorkedDate { get; set; }
    }

    public class UserRoleCompetencyMapParam
    {
        public int ID { get; set; }
        public int RoleId { get; set; }
        public int TargetRoleId { get; set; }
        public int RoleStructureId { get; set; }
        public int UserID { get; set; }
        public List<UT_V2UserCompetencyMap> UserCompetencyMapData { get; set; }
    }

    public class UT_V2UserCompetencyMap
    {
        public int CompetencyMapId { get; set; }
        public int UserID { get; set; }
        public int CompetencyId { get; set; }
        public int CompetencyLevelId { get; set; }
        public int ExpYear { get; set; }
        public int ExpMonth { get; set; }
        public DateTime LastWorkedDate { get; set; }
    }

    public class UserRoleCompetency
    {
        public IEnumerable<UserRoleMap> UserRoleMap { get; set; }
        public IEnumerable<UserCompetencyMapList> UserCompetencyMapList { get; set; }
    }

}