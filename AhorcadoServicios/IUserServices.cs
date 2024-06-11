using AhorcadoServicios.Model.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AhorcadoServicios
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IUserServices" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IUserServices
    {
        [OperationContract]
        Player logIn(string email, string password);
        [OperationContract]
        bool registerUser(Player newPlayer);
        [OperationContract]
        bool emailAlreadyRegistered(string email);
        [OperationContract]
        bool nicknameAlreadyRegistered(string nickname);
        [OperationContract]
        bool telephoneAlreadyExist(string telephone);
    }
}
