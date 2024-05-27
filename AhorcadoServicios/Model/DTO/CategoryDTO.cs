using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.DTO
{
    public class CategoryDTO
    {
        public static List<Category> getCategories()
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var categories = (from cat in dataContext.GetTable<Category>()
                                  select cat).ToList();
                return categories;

            }
            catch (SqlException ex)
            {
                throw ex; 
            }
        }
    }
}