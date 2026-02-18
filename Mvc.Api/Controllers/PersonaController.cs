using DtoModel.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Persona;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PersonaController : ControllerBase
    {

        private readonly IPersonaBussnies _personaBussnies;

        public PersonaController(IPersonaBussnies personaBussnies)
        {
            _personaBussnies = personaBussnies;
        }


        [HttpGet]
        public async Task<ActionResult<List<PersonaDto>>> GetAll()
        {
            List<PersonaDto> list = await _personaBussnies.GetAll();

            return Ok(list);
        }


    }
}
