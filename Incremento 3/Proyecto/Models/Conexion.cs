using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto.Models
{
    public class Conexion
    {
        public SqlConnection cad = new SqlConnection("Data Source=.;Initial Catalog=Edicas;Integrated Security=True");
        public void conectar()
        {

            cad.Open();
        }
        public void desconectar()
        {
            cad.Close();
        }
    }
}