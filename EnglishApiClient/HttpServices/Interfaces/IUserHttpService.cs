using EnglishApiClient.Dtos.Auth;
using EnglishApiClient.Dtos.Entity;
using EnglishApiClient.Infrastructure.RequestFeatures;

namespace EnglishApiClient.HttpServices.Interfaces
{
    public interface IUserHttpService
    {
        Task<PagingResponse<User>> GetAll(PaginationParameters parameters);
        Task<User> GetById(string id);
        Task<bool> Delete(string id);
        Task<ICollection<UserRole>> GetUserRoles();
        Task<bool> Create(UserCreateModel entity);
        Task<bool> SetNewPassword(SetPasswordModel passwordModel);
        Task<bool> Edit(User entity);
    }
}