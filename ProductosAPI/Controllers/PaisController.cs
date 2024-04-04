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
    public class PaisController : ControllerBase
    {


        private readonly string connectionString;
        public PaisController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IReadOnlyList<Pais> GetTramo()
        {
            using var connection = new SqlConnection(connectionString);
            var query = "Select * from pais";


            var result = connection.Query<Pais>(query);
            return result.ToList().AsReadOnly();
        }

        [HttpPost]
        public async Task<bool> AddPais(AgregarPais add)
        {
            using var connection = new SqlConnection(connectionString);

            var query = @"
       
            INSERT INTO pais  (nombre) values (@nombre)";

            var darianMira = query;


            var result = await connection.ExecuteAsync(query, add);
            return result > 0;

        }

        [HttpPut]

        public async Task<bool> ActualizarPais(Pais contri)
        {
            using var connection = new SqlConnection(connectionString);

            var query = @"UPDATE pais set nombre = @nombre where id=@id";

            var result = await connection.ExecuteAsync(query, contri);

            return result > 0;
        }

        [HttpDelete]

        public async Task<bool> EliminarPais(int id)
        {
            using var connection = new SqlConnection(connectionString);

            var query = "delete from pais where id=@id";
            var result = await connection.ExecuteAsync(query, new { id = id });

            return result > 0;
        }



    }
}
