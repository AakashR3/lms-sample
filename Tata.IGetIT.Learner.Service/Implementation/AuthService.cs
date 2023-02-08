namespace Tata.IGetIT.Learner.Service.Implementation
{

    public class AuthService : IAuthService
    {
        private readonly IAuthRepo _authRepo;

        ILogger logger = LogManager.GetCurrentClassLogger();
        public AuthService(IAuthRepo authRepo)
        {
            if (authRepo == null)
            {
                new ArgumentNullException("AuthRepo cannot be null");
            }
            _authRepo = authRepo;
        }

        public bool UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            throw new NotImplementedException();
        }

        public int UserRegistration(SocialRegisteration register)
        {
            throw new NotImplementedException();
        }
    }
}