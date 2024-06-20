using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AhorcadoServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IGameServices" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IGameServices
    {
        [OperationContract]
        MatchGame createMatch(MatchGame createMatch);
        [OperationContract]
        List<MatchGame> getMatchList(int playerID);
        [OperationContract]
        List<MatchGame> getMatchesPlayed(int playerID);
        [OperationContract]
        bool initMatchGame(int guestID, int matchID);
        [OperationContract]
        bool leaveMatch(int matchID);
        [OperationContract]
        bool finishMatch(int matchID);
        [OperationContract]
        bool updateCharBD(char letter, int matchID);
        [OperationContract]
        int getMatchStatus(int matchID);
        [OperationContract]
        bool updateRemainingAttempts(int remainingAttempts, int matchID);
        [OperationContract]
        bool updateWinner(int playerID, int matchID);
        [OperationContract]
        string getGuestNickName(int playerID);
        [OperationContract]
        bool isThereGuest(int matchID);
        [OperationContract]
        char? getGuestLetter(int matchID);
        [OperationContract]
        int getRemainingAttempts(int matchID);
        [OperationContract]
        void updatePointsEarned(int playerID);
        [OperationContract]
        int updateNameWinner(int matchID, string nameWinner);
    }
}
