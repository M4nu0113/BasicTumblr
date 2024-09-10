# BasicTumblr

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace PruebaTumblr.Conexion
{
    public class ConexionET
    {
        private string cadena_conexion = "server=SALAK301-12;database=TumblrDB;Integrated Security=True;TrustServerCertificate=True;";

        public ConexionET()
        {
            Console.WriteLine("Conexion EF con la base de datos");
        }

        public void ObtenerUsuarios()
        {
            var conexion = new Conexion();
            conexion.StringConnection = this.cadena_conexion;
            var lista_usuarios=conexion.Usuarios.ToList();
            foreach (var usuario in lista_usuarios)
            {
                Console.WriteLine(usuario.Id.ToString()+'|'+ usuario.Nombre);
            }
        }
        public void GuardarUsuarios()
        {
            var conexion = new Conexion();
            conexion.StringConnection = this.cadena_conexion;

            var usuario = new Usuarios()
            {
                Nombre = "Prueba"
            };
            conexion.Usuarios.Add(usuario);
            conexion.SaveChanges();
        }
    }

    public partial class Conexion : DbContext
    {
        public string? StringConnection{get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConnection!, p =>{});
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        
    }
}
