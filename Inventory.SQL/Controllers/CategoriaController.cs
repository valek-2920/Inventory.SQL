using Microsoft.AspNetCore.Mvc;
using Inventory.Domains.Models;
using Neo4jClient;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        readonly IGraphClient _client;

        public CategoriaController(IGraphClient client)
        {
            _client = client;
        }

        [HttpGet]
        [Route("categorias")]
        public async Task<IActionResult> Get()
        {
            var Categorias = await _client.Cypher.Match("(n: Categoria)")
                                                  .Return(n => n.As<Categoria>()).ResultsAsync;

            return Ok(Categorias);
        }

        [HttpGet]
        [Route("categoria")]
        public async Task<IActionResult> GetByCodigo(int Codigo)
        {
            var Categorias = await _client.Cypher.Match("(d:Categoria)")
                                                  .Where((Categoria d) => d.Codigo == Codigo)
                                                  .Return(d => d.As<Categoria>()).ResultsAsync;

            return Ok(Categorias.LastOrDefault());
        }

        [HttpPost]
        [Route("categoria")]
        public async Task<IActionResult> Create([FromBody] Categoria dept)
        {
            await _client.Cypher.Create("(d:Categoria $dept)")
                                .WithParam("dept", dept)
                                .ExecuteWithoutResultsAsync();

            return Ok();
        }

        [HttpPut]
        [Route("categoria")]
        public async Task<IActionResult> Update([FromBody] Categoria dept)
        {
            await _client.Cypher.Match("(d:Categoria)")
                                .Where((Categoria d) => d.Codigo == dept.Codigo)
                                .Set("d = $dept")
                                .WithParam("dept", dept)
                                .ExecuteWithoutResultsAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("categoria")]
        public async Task<IActionResult> Delete(int Codigo)
        {
            await _client.Cypher.Match("(d:Categoria)")
                                 .Where((Categoria d) => d.Codigo == Codigo)
                                 .Delete("d")
                                 .ExecuteWithoutResultsAsync();
            return Ok();
        }

    }
}
