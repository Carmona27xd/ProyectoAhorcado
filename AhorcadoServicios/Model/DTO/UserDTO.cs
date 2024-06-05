using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static AhorcadoServicios.Model.POCO.Player;

namespace AhorcadoServicios.Model.DTO
{
    public class UserDTO
    {
        public static Player logIn(string email, string password)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var user = (from us in dataContext.GetTable<Player>()
                            where us.Email == email && us.Password == password
                            select us).FirstOrDefault();
                return user;
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }
    }
}