using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PodiumCaseStudy.Services.User
{
    public interface IUserService
    {
        Task<bool> EmailAvailableAsync(string emailAddress);

        Task<Guid> AddUserAsync(UserDto userDto);

        Task<IEnumerable<Context.User>> GetUsersAsync();
    }
}
