using ID_model.DTOs;

namespace ID_service.Interfaces
{
    public interface ILoginService
    {
        Task<string> LoginClient(LoginDTO request);
        Task<string> LoginCompany(LoginDTO request);

    }
}
