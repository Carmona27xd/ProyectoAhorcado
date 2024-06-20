using AhorcadoServicios.Model.DTO;
using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AhorcadoServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "GameServices" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione GameServices.svc o GameServices.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class GameServices : IGameServices
    {
        public MatchGame createMatch(MatchGame newMatch)
        {
            return MatchGameDTO.createMatch(newMatch);
        }

        public bool finishMatch(int matchID)
        {
            return MatchGameDTO.finishMatch(matchID);
        }

        public char? getGuestLetter(int matchID)
        {
            return MatchGameDTO.getGuestLetter(matchID);
        }

        public string getGuestNickName(int playerID)
        {
            return MatchGameDTO.getGuestNickName(playerID);
        }

        public List<MatchGame> getMatchList(int playerID)
        {
            return MatchGameDTO.getMatchesAvaliables(playerID);
        }

        public List<MatchGame> getMatchesPlayed(int playerID)
        {
            return MatchGameDTO.getMatchesPlayed(playerID);
        }

        public int getMatchStatus(int matchID)
        {
            return MatchGameDTO.getMatchStatus(matchID);
        }

        public int getRemainingAttempts(int matchID)
        {
            return MatchGameDTO.getRemainingAttempts(matchID);
        }

        public bool initMatchGame(int guestID, int matchID)
        {
            return MatchGameDTO.initMatchGame(guestID, matchID);
        }

        public bool isThereGuest(int matchID)
        {
            return MatchGameDTO.isThereGuest(matchID);
        }

        public bool leaveMatch(int matchID)
        {
            return MatchGameDTO.leaveMatch(matchID);
        }

        public bool updateCharBD(char letter, int matchID)
        {
            return MatchGameDTO.updateCharBD(letter, matchID);
        }

        public void updatePointsEarned(int playerID)
        {
            MatchGameDTO.updatePointsEarned(playerID);
        }

        public bool updateRemainingAttempts(int remainingAttempts, int matchID)
        {
            return MatchGameDTO.updateRemainingAttempts(remainingAttempts, matchID);
        }

        public bool updateWinner(int playerID, int matchID)
        {
            return MatchGameDTO.updateWinner(playerID, matchID);
        }

        public int updateNameWinner(int matchID, string nameWinner)
        {
            return MatchGameDTO.updateGameWinner(matchID, nameWinner);
        }
    }
}
