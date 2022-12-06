using Microsoft.AspNetCore.Mvc;
using Inventory.Domains.Models;
using Neo4jClient;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ActivosController : ControllerBase
    {
        readonly IGraphClient _client;

        public ActivosController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        [Route("activos")]
        public async Task<IActionResult> Get()
        {
            var Activos = await _client.Cypher.Match("(n: Activos)")
                                                  .Return(n => n.As<Activos>()).ResultsAsync;

            return Ok(Activos);
        }

        [HttpGet]
        [Route("activo")]
        public async Task<IActionResult> GetByCodigo(int Codigo)
        {
            var Activos = await _client.Cypher.Match("(d:Activos)")
                                                  .Where((Activos d) => d.Codigo == Codigo)
                                                  .Return(d => d.As<Activos>()).ResultsAsync;

            return Ok(Activos.LastOrDefault());
        }

        [HttpPost]
        [Route("activo")]
        public async Task<IActionResult> Create([FromBody] Activos acts)
        {

            await _client.Cypher.Create("(d:Activos $acts)")
                         .WithParam("acts", acts)
                         .ExecuteWithoutResultsAsync();

            await _client.Cypher.Match("(a:Activos)")
                                .Where((Activos a) => a.Codigo == acts.Codigo)
                                .Match("(c:Categoria)")
                                .Where((Categoria c) => c.Codigo == acts.CategoriaId)
                                .Merge("(a)-[r:CATEGORIAS]->(c)")
                                .ExecuteWithoutResultsAsync();


            return Ok();

        }

        [HttpPut]
        [Route("activo")]
        public async Task<IActionResult> Update([FromBody] Activos acts)
        {
            await _client.Cypher.Match("(d:Activos)")
                                .Where((Activos d) => d.Codigo == acts.Codigo)
                                .Set("d = acts")
                                .WithParam("acts", acts)
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("activo")]
        public async Task<IActionResult> Delete(int Codigo)
        {


            await _client.Cypher.Match("(d:Activos)")
                                 .Where((Activos d) => d.Codigo == Codigo)
                                 .DetachDelete("d")
                                 .ExecuteWithoutResultsAsync();
            return Ok();
        }

    }
}
