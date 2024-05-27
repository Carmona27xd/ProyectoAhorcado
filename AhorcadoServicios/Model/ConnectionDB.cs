using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model
{
    public class ConnectionDB
    {
        public static SqlConnection getConnection()
        {
            string connectionString = "Data Source=CARMONA;Initial Catalog=ahorcado;Integrated Security=True";
            return new SqlConnection(connectionString);
        }
    }
}