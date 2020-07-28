using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PodiumCaseStudy.Context;

namespace PodiumCaseStudy.Services.User
{
    public class UserService : IUserService
    {
        private readonly PodiumCaseStudyContext _podiumCaseStudyContext;
        private readonly IMapper _mapper;

        public UserService(PodiumCaseStudyContext podiumCaseStudyContext, IMapper mapper)
        {
            _podiumCaseStudyContext = podiumCaseStudyContext;
            _mapper = mapper;
        }

        public async Task<bool> EmailAvailableAsync(string emailAddress)
        {
            return !await _podiumCaseStudyContext.Users.AnyAsync(x => x.Email == emailAddress);
        }

        public async Task<Guid> AddUserAsync(UserDto userDto)
        {
            var userEntity = _mapper.Map<Context.User>(userDto);
            await _podiumCaseStudyContext.Users.AddAsync(userEntity);
            await _podiumCaseStudyContext.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<IEnumerable<Context.User>> GetUsersAsync()
        {
            return await _podiumCaseStudyContext.Users.ToArrayAsync();
        }
    }
}
