using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PodiumCaseStudy.Models;
using PodiumCaseStudy.Services.Mortgage;
using PodiumCaseStudy.Services.Mortgage.Model;

namespace PodiumCaseStudy.Controllers
{
    [ApiController, Route("[controller]")]
    public class MortgageController : ControllerBase
    {
        private readonly IMortgageService _mortgageService;
        private readonly IMapper _mapper;

        public MortgageController(IMortgageService mortgageService, IMapper mapper)
        {
            _mortgageService = mortgageService;
            _mapper = mapper;
        }

        [HttpGet("GetAvailableMortgages")]
        public async Task<ActionResult<IEnumerable<MortgageProduct>>> GetAvailableMortgages([FromQuery]MortgageProductQuery mortgageProductQuery)
        {
            var mortgageProductQueryDto = _mapper.Map<MortgageProductQueryDto>(mortgageProductQuery);
            var products = await _mortgageService.GetProductsAsync(mortgageProductQueryDto);

            return Ok(_mapper.Map<List<MortgageProduct>>(products));
        }
    }
}
