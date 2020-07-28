using System.Collections.Generic;
using System.Threading.Tasks;
using PodiumCaseStudy.Services.Mortgage.Model;

namespace PodiumCaseStudy.Services.Mortgage
{
    public interface IMortgageService
    {
        Task<List<MortgageProductDto>> GetProductsAsync(MortgageProductQueryDto mortgageProductQueryDto);
    }
}
