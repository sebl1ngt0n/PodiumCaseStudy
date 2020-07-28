using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PodiumCaseStudy.Context;
using PodiumCaseStudy.Services.Mortgage.Model;

namespace PodiumCaseStudy.Services.Mortgage
{
    public class MortgageService : IMortgageService
    {
        private readonly PodiumCaseStudyContext _podiumCaseStudyContext;
        private readonly IMapper _mapper;

        public MortgageService(PodiumCaseStudyContext podiumCaseStudyContext, IMapper mapper)
        {
            _podiumCaseStudyContext = podiumCaseStudyContext;
            _mapper = mapper;
        }

        public async Task<List<MortgageProductDto>> GetProductsAsync(MortgageProductQueryDto mortgageProductQueryDto)
        {
            var availableProducts = new List<MortgageProductDto>();
            var userEntity = await _podiumCaseStudyContext.Users.FindAsync(mortgageProductQueryDto.UserId);

            if (userEntity != null)
            {
                var userAge = GetUserAge(userEntity.DateOfBirth);

                if (userAge >= 18)
                {
                    var loanToValue = (1 - ((double)mortgageProductQueryDto.Deposit / (double)mortgageProductQueryDto.PropertyValue)) * 100;

                    availableProducts = await _podiumCaseStudyContext.MortgageProducts
                        .Where(x => loanToValue < x.LoanToValue)
                        .ProjectTo<MortgageProductDto>(_mapper.ConfigurationProvider)
                        .ToListAsync();
                }
            }

            return availableProducts;
        }

        private int GetUserAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;

            if (dateOfBirth.Month > today.Month || (dateOfBirth.Month == today.Month && dateOfBirth.Day > today.Day))
            {
                --age;
            }

            return age;
        }
    }
}
