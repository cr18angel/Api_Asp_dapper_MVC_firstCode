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
    public class ProductoController
    {

        private readonly string connectionString;

        public ProductoController(IConfiguration config)
        {
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IReadOnlyList<Producto> GetProductos()
        {
            using var connection = new SqlConnection (connectionString);
            var query = "Select * from producto";

            var result = connection.Query<Producto>(query);
            return result.ToList().AsReadOnly();
        }

        [HttpPost]

        public async Task<bool> AddProducto(AgregarProducto add)
        {

            using var connection = new SqlConnection(connectionString);
            var query = @"insert into producto (nombre, precio, cantidad, idMarca) values (@nombre, @precio, @cantidad, @idMarca)";

            var result = await connection.ExecuteAsync(query, add);
            return result > 0;

        }

        [HttpPut]
        //no deberia poder cambiar la marca del producto, pero por agilidad del ejercicio lo dejo ahí

        public async Task<bool> ActualizarProducto(Producto product)
        {
            using var connection = new SqlConnection(connectionString);

            var query = @"update producto set nombre = @nombre, precio = @precio, cantidad = @cantidad, idMarca = @idMarca where id=@id";
            var result = await connection.ExecuteAsync(query, product);

            return result > 0;
        }

        [HttpDelete]

        public async Task<bool> EliminarProducto(int id)
        {
            using var connection = new SqlConnection(connectionString);

            var query = "delete from producto where id = @id";
            var result = await connection.ExecuteAsync(query, new {id = id});

            return result > 0;
        }



    }
}
