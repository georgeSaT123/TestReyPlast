using ApiUsuarios.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace ApiUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly string? connection;
        public UsuarioController(IConfiguration configuration)
        {
            connection = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet]
        public IEnumerable<Usuario> GetAll()
        {
            List<Usuario> usuarios = new ();
            using (SqlConnection sqlConnection = new(connection))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new("SP_MostrarUsuario", sqlConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario usuario = new Usuario {
                                Id = Convert.ToInt32(reader["ID"]),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString()
                            };
                            usuarios.Add(usuario);
                        }
                    }
                }
            }
            return usuarios;
        }

        [HttpPost]
        public void Create([FromBody] Usuario usuario)
        {
            using (SqlConnection sqlConnection = new(connection))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new("SP_InsertarUsuario", sqlConnection))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", usuario.Id);
                    cmd.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
