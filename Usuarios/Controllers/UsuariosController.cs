using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Usuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private string connection = @"Server=localhost; Database=usu; Uid=root; Password=root;";


        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Models.usuario> lst = null;
            using (var db = new MySqlConnection(connection))
            {

                var sql = "select usuarioPK,Nombre,Rut,Edad from usu.usuarios";

                lst = db.Query<Models.usuario>(sql);

            }

            return Ok(lst);
        }

        [HttpPost]
        public IActionResult Insert(Models.usuario model)
        {
            string result = "Usuario ingresado";
            using (var db = new MySqlConnection(connection))
            {
                var sql = "insert into usu.usuarios (Nombre, Rut, Edad) values (@Nombre, @Rut, @Edad)";
                db.Execute(sql, new { Nombre = model.Nombre, Rut = model.Rut, Edad = model.Edad });
            }
            return Ok(result);

        }

        [HttpPut]
        public IActionResult Edit(Models.usuario model)
        {
            string result = "Usuario editado";
            using (var db = new MySqlConnection(connection))
            {
                var sql = "update usu.usuarios set Nombre=@Nombre, Rut=@Rut, Edad=@Edad where usuarioPK=@usuarioPK";
                db.Execute(sql, new { usuarioPK = model.usuarioPK, Nombre = model.Nombre, Rut = model.Rut, Edad = model.Edad });
            }
            return Ok(result);

        }

        [HttpDelete]
        public IActionResult Delete(Models.usuario model)
        {
            string result = "Usuario eliminado";
            using (var db = new MySqlConnection(connection))
            {
                var sql = "delete from usu.usuarios where usuarioPK=@usuarioPK";
                db.Execute(sql, model);
            }
            return Ok(result);
        }
    }
}
