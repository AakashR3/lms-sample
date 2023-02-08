namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface IAuthService
    {
        public bool UpdatePassword(UpdatePasswordRequest updatePasswordRequest);

        public int UserRegistration(SocialRegisteration register);
    }
}
