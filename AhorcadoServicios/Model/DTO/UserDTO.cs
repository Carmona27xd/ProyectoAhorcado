using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using static AhorcadoServicios.Model.POCO.User;

namespace AhorcadoServicios.Model.DTO
{
    public class UserDTO
    {
        public static User logIn(string email, string password)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var user = (from us in dataContext.GetTable<User>()
                            where us.correo == email && us.password == password
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