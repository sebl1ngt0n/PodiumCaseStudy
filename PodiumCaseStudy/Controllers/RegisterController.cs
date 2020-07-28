using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PodiumCaseStudy.Models;
using PodiumCaseStudy.Services.User;

namespace PodiumCaseStudy.Controllers
{
    [ApiController, Route("[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegisterController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var userDto = _mapper.Map<UserDto>(registerModel);
            var userId = await _userService.AddUserAsync(userDto);

            return Ok(userId);
        }
    }
}
