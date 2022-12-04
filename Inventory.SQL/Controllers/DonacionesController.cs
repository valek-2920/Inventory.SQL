using Microsoft.AspNetCore.Mvc;
using Inventory.Domains.Models;
using Neo4jClient;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonacionesController : ControllerBase
    {
        readonly IGraphClient _client;

        public DonacionesController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        [Route("donaciones")]
        public async Task<IActionResult> Get()
        {
            var Donaciones = await _client.Cypher.Match("(n: Donacion)")
                                                  .Return(n => n.As<Donaciones>()).ResultsAsync;

            return Ok(Donaciones);
        }

        [HttpGet]
        [Route("donacion")]
        public async Task<IActionResult> GetByCodigo(int Codigo)
        {
            var Donaciones = await _client.Cypher.Match("(d:Donacion)")
                                                  .Where((Donaciones c) => c.Codigo == Codigo)
                                                  .Return(d => d.As<Donaciones>()).ResultsAsync;

            return Ok(Donaciones.LastOrDefault());
        }

        [HttpPost]
        [Route("donacion")]
        public async Task<IActionResult> Create([FromBody] Donaciones dept)
        {
            await _client.Cypher.Create("(d:Donacion $dept)")
                                .WithParam("dept", dept)
                                .ExecuteWithoutResultsAsync();

            await _client.Cypher.Match("(a:Activos)")
                    .Where((Activos a) => a.Codigo == dept.Codigo)
                    .Match("(c:Donaciones)")
                    .Where((Donaciones c) => c.Codigo == dept.Activos)
                    .Merge("(a)-[r:DONACIONES]->(c)")
                    .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut]
        [Route("donacion")]
        public async Task<IActionResult> Update([FromBody] Donaciones dept)
        {
            await _client.Cypher.Match("(d:Donacion)")
                                .Where((Donaciones d) => d.Codigo == dept.Codigo)
                                .Set("d = $dept")
                                .WithParam("dept", dept)
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("donacion")]
        public async Task<IActionResult> Delete(int Codigo)
        {
            await _client.Cypher.Match("(d:Donacion)")
                                 .Where((Donaciones c) => c.Codigo == Codigo)
                                 .Delete("c")
                                 .ExecuteWithoutResultsAsync();
            return Ok();
        }

    }
}
