using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.DTO
{
    public class MatchDTO
    {
        public static bool createMatch(Match newMatch)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                dataContext.GetTable<Match>().InsertOnSubmit(newMatch);
                dataContext.SubmitChanges();
                return true;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static List<Match> getMatchesAvaliables()
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var matches = (from mat in dataContext.GetTable<Match>()
                               where mat.estado_partida == 1
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