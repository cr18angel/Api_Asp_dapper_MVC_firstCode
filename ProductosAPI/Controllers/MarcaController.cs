using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ProductosAPI.Modelo;
using ProductosAPI.Modelo.DTOs;
using System.Reflection.Metadata.Ecma335;

namespace ProductosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaController
    {

        private readonly string connectionString;
        public MarcaController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet]

        public IReadOnlyList <Marca> GetMarcas()
        {
            using var connection = new SqlConnection(connectionString);
            var query = "Select * from marca";

            var result = connection.Query<Marca>(query);
            return result.ToList().AsReadOnly();
        }

        [HttpPost]

        public async Task<bool> AddMarca(AgregarMarca add)
        {
            using var connection = new SqlConnection(connectionString);

            var query = @"
            insert into marca (nombre, idPais) values (@nombre, @idPais)";

            var result = await connection.ExecuteAsync(query, add);
            return result > 0;
        }

        [HttpPut]

        public async Task<bool> ActualizarMarca(Marca brand)
        {
            using var connection = new SqlConnection(connectionString);

            var query = @"update marca set nombre = @nombre, idPais = @idPais where id=@id";

            var result = await connection.ExecuteAsync(query, brand);

            return result > 0;
        }
    
    }
}
