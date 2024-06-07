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
        public static MatchGame createMatch(MatchGame newMatch)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                dataContext.GetTable<MatchGame>().InsertOnSubmit(newMatch);
                dataContext.SubmitChanges();
                return newMatch;
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

        public static bool initMatchGame(int guestID, int matchID)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var matchToUpdate = dataContext.GetTable<MatchGame>().FirstOrDefault(mat => mat.MatchID == matchID);
                if (matchToUpdate != null)
                {
                    matchToUpdate.GuestID = guestID;
                    matchToUpdate.StatusMatchID = 4;
                    dataContext.SubmitChanges();
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            { 
                throw ex;
            }
        }

        public static bool leaveMatch(int matchID)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var matchToLeave = dataContext.GetTable<MatchGame>().FirstOrDefault(mat => mat.MatchID == matchID);
                if (matchToLeave != null)
                {
                    matchToLeave.StatusMatchID = 2;
                    dataContext.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static bool finishMatch(int matchID)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var matchToLeave = dataContext.GetTable<MatchGame>().FirstOrDefault(mat => mat.MatchID == matchID);
                if (matchToLeave != null)
                {
                    matchToLeave.StatusMatchID = 3;
                    dataContext.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public static bool updateCharBD(char letter, int matchID)
        {
            try
            {
                var connection = ConnectionDB.getConnection();
                connection.Open();
                DataContext dataContext = new DataContext(connection);
                var charUpdate = dataContext.GetTable<MatchGame>().FirstOrDefault(mat => mat.MatchID == matchID);
                if (charUpdate != null)
                {
                    charUpdate.SelectLetter = letter;
                    dataContext.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(SqlException ex)
            {
                throw ex;
            }
        }

        public static bool updateWinner(int playerID, int matchID)
        {
            try
            {
                using (var connection = ConnectionDB.getConnection())
                {
                    connection.Open();
                    DataContext dataContext = new DataContext(connection);
                    var updateWinner = dataContext.GetTable<MatchGame>().FirstOrDefault(mat => mat.MatchID == matchID);
                    if (updateWinner != null)
                    {
                        updateWinner.WinnerID = playerID;
                        dataContext.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static bool updateRemainingAttempts(int remainingAttempts, int matchID)
        {
            try
            {
                using (var connection = ConnectionDB.getConnection())
                {
                    connection.Open();
                    DataContext dataContext = new DataContext(connection);
                    var updateAttempts = dataContext.GetTable<MatchGame>().FirstOrDefault(mat => mat.MatchID == matchID);
                    if (updateAttempts != null)
                    {
                        updateAttempts.RemainingAttempts = remainingAttempts;
                        dataContext.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static int getMatchStatus(int matchID)
        {
            try
            {
                using (var connection = ConnectionDB.getConnection())
                {
                    connection.Open();
                    DataContext dataContext = new DataContext(connection);
                    var status = (from sta in dataContext.GetTable<MatchGame>()
                                  where sta.MatchID == matchID
                                  select sta.StatusMatchID).FirstOrDefault();
                    return status;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static string getGuestNickName(int playerID)
        {
            try
            {
                using (var connection = ConnectionDB.getConnection())
                {
                    connection.Open();
                    DataContext dataContext = new DataContext(connection);
                    var nickName = (from nic in dataContext.GetTable<Player>()
                                    where nic.PlayerID == playerID
                                    select nic.NickName).FirstOrDefault();
                    return nickName;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static bool isThereGuest(int matchID)
        {
            try
            {
                using (var connection = ConnectionDB.getConnection())
                {
                    connection.Open();
                    DataContext dataContext = new DataContext(connection);
                    var guest = (from gue in dataContext.GetTable<MatchGame>()
                                 where gue.MatchID == matchID && gue.GuestID != null
                                 select gue.GuestID).FirstOrDefault();
                    return guest != null; 
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static char? getGuestLetter(int matchID)
        {
            try
            {
                using (var connection = ConnectionDB.getConnection())
                {
                    connection.Open();
                    DataContext dataContext = new DataContext(connection);
                    var letter = (from lett in dataContext.GetTable<MatchGame>()
                                  where lett.MatchID == matchID && lett.GuestID != null
                                  select lett.SelectLetter).FirstOrDefault();
                    return letter; 
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public static int getRemainingAttempts(int matchID)
        {
            try
            {
                using ( var connection = ConnectionDB.getConnection())
                {
                    connection.Open();
                    DataContext dataContext = new DataContext(connection);
                    var attempts = (from at in dataContext.GetTable<MatchGame>()
                                    where at.MatchID == matchID
                                    select at.RemainingAttempts).FirstOrDefault();
                    return attempts;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
    