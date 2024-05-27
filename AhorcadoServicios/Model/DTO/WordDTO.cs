
using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.DTO
{
    public class WordDTO
    {
        public static List<Word> getWordsPerCategory(int category)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var words = (from wor in dataContext.GetTable<Word>()
                             where wor.id_categoria == category
                             select wor).ToList();
                return words;

            }
            catch (SqlException ex)
            {
                throw ex; 
            }
        }
    }
}