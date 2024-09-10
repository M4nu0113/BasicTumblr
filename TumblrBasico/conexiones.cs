using System.Data.SqlClient;
using System.Data;

namespace TumblrBasico.Conexion
{
    public class Usuarios
    {
        public int Id {get;set;}
        public required string Nombre {get;set;}
    }
    public class Publicaciones
    {
        public int Id {get;set;}
        public int Usuario {get;set;}
        public required string Contenido {get;set;}
        public DateTime Fecha {get; set;}
    }
    public class Conexion
    {
        private string cadenaConexion = "server=M4NU_HELPER\\DEV;database=TumblrDB;uid=sa;pwd=STEMgirls>>>;TrustServerCertificate=True;";
        public void ConexionBasica()
        {
            Console.WriteLine("Conectando a la base de datos...");
        }
        public void ObtenerUsuarios()
        {
            var sql_conexion = new SqlConnection(this.cadenaConexion);
            sql_conexion.Open();
            var consulta = "SELECT [Id],[Nombre] FROM Usuarios";
            var adaptador = new SqlDataAdapter(new SqlCommand(consulta, sql_conexion));
            var set_datos = new DataSet();
            adaptador.Fill(set_datos);

            var lista_usuarios = new List<Usuarios>();
            foreach(var elemento in set_datos.Tables[0].AsEnumerable())
            {
                lista_usuarios.Add(new Usuarios()
                {
                    Id = Convert.ToInt32(elemento.ItemArray[0].ToString()),
                    Nombre = elemento.ItemArray[1].ToString(),
                });
            }
            sql_conexion.Close();

            foreach(var usuario in lista_usuarios)
            {
                Console.WriteLine(usuario.Id.ToString()+'|'+ usuario.Nombre);
            }
        }
        public void AgregarUsuario()
        {
            var sql_conexion = new SqlConnection(this.cadenaConexion);
            sql_conexion.Open();

            var usuario = new Usuarios(){Nombre = "Johan"};
            var comando = new SqlCommand("INSERT INTO [Usuarios]"+"([Nombre]) VALUES ('" + usuario.Nombre + "')", sql_conexion);
            var resultado = comando.ExecuteNonQuery();
            Console.WriteLine("Fila Afectada: " + resultado);
            sql_conexion.Close();
        }
    }
}