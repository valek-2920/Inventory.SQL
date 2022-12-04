using Inventory.Domains.Models;
using Microsoft.AspNetCore.Mvc;
using Neo4jClient;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        readonly IGraphClient _client;

        public UsuariosController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        [Route("usuarios")]
        public async Task<IActionResult> Get()
        {
            var Usuarios = await _client.Cypher.Match("(n: Usuarios)")
                                                  .Return(n => n.As<Usuarios>()).ResultsAsync;

            return Ok(Usuarios);
        }

        [HttpGet]
        [Route("usuario")]
        public async Task<IActionResult> GetByCedula(int Cedula)
        {
            var Usuarios = await _client.Cypher.Match("(d:Usuarios)")
                                                  .Where((Usuarios d) => d.Cedula == Cedula)
                                                  .Return(d => d.As<Usuarios>()).ResultsAsync;

            return Ok(Usuarios.LastOrDefault());
        }

        [HttpPost]
        [Route("usuario")]
        public async Task<IActionResult> Create([FromBody] Usuarios dept)
        {
            await _client.Cypher.Create("(d:Usuarios $dept)")
                                .WithParam("dept", dept)
                                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut]
        [Route("usuario")]
        public async Task<IActionResult> Update([FromBody] Usuarios dept)
        {
            await _client.Cypher.Match("(d:Usuarios)")
                                .Where((Usuarios d) => d.Cedula == dept.Cedula)
                                .Set("d = $dept")
                                .WithParam("dept", dept)
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("usuario")]
        public async Task<IActionResult> Delete(int Cedula)
        {
            await _client.Cypher.Match("(d:Usuarios)")
                                 .Where((Usuarios d) => d.Cedula == Cedula)
                                 .Delete("d")
                                 .ExecuteWithoutResultsAsync();
            return Ok();
        }

    }
}
