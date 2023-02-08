namespace Tata.IGetIT.Learner.Service.Implementation
{
    public class HeroSectionService : IHeroSectionService
    {
        private readonly IHeroSectionRepo _heroSectionRepo;
        public HeroSectionService(IHeroSectionRepo heroSectionRepo)
        {
            if (heroSectionRepo == null)
            {
                new ArgumentNullException(LearnerAppConstants.USEREPO_NULL);
            }
            _heroSectionRepo = heroSectionRepo;

        }

        public async Task<CareerPath> CareerPath(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                CareerPath currentRole = new()
                {
                    Message = "Current path is test"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.CareerPath(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
                */
            }
            catch
            {
                throw;
            }
        }

        public async Task<CurrentLevel> CurrentLevel(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                CurrentLevel currentRole = new()
                {
                    Message = "Current level is test"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.CurrentLevel(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
                */
            }
            catch
            {
                throw;
            }
        }

        public async Task<CurrentRole> CurrentRole(int UserID, List<string> ErrorsMessages)
        {
            try
            {

                CurrentRole currentRole = new()
                {
                    Message = "Current role is test"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.CurrentRole(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                
                return result;
                */
            }
            catch
            {
                throw;
            }
        }

        public async Task<Skillset> Skillset(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                Skillset currentRole = new()
                {
                    Message = "Current skillset is test"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.Skillset(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
                */
            }
            catch
            {
                throw;
            }
        }

        public async Task<TargetRoles> TargetRole(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                TargetRoles currentRole = new()
                {
                    Message = "Current target role is test"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.TargetRole(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
                */
            }
            catch
            {
                throw;
            }
        }

        public async Task<TargetRoleCareerPath> TargetRoleCareerPath(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                TargetRoleCareerPath currentRole = new()
                {
                    Message = "Current target role path is test"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.TargetRoleCareerPath(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
                */
            }
            catch
            {
                throw;
            }
        }

        public async Task<TargetRoleCareerPathPercentage> TargetRoleCareerPathPercentage(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                TargetRoleCareerPathPercentage currentRole = new()
                {
                    Message = "80%"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.TargetRoleCareerPathPercentage(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
                */
            }
            catch
            {
                throw;
            }
        }

        public async Task<TargetRoleCurrentLevel> TargetRoleCurrentLevel(int UserID, List<string> ErrorsMessages)
        {
            try
            {
                TargetRoleCurrentLevel currentRole = new()
                {
                    Message = "Current [test] TargetRoleCurrentLevel is test"
                };
                return currentRole;
                /*
                var result = await _heroSectionRepo.TargetRoleCurrentLevel(UserID);
                if (result == null)
                    ErrorsMessages.Add(LearnerAppConstants.INVALID_REQUEST);
                return result;
                */
            }
            catch
            {
                throw;
            }
        }
    }
}
