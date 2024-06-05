using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.DTO
{
    public class MatchGameDTO
    {
        public static bool createMatch(MatchGame newMatch)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                dataContext.GetTable<MatchGame>().InsertOnSubmit(newMatch);
                dataContext.SubmitChanges();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static List<MatchGame> getMatchesAvaliables()
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var matches = (from mat in dataContext.GetTable<MatchGame>()
                               where mat.StatusMatchID == 1
                               select mat).ToList();
                return matches;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}