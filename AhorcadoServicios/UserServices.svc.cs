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
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "UserServices" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione UserServices.svc o UserServices.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class UserServices : IUserServices
    {
        public bool emailAlreadyRegistered(string email)
        {
            return UserDTO.emailAlreadyRegistered(email);
        }

        public Player logIn(string email, string password)
        {
            return UserDTO.logIn(email, password);
        }

        public bool nicknameAlreadyRegistered(string nickname)
        {
            return UserDTO.nicknameAlreadyRegistered(nickname);
        }

        public bool registerUser(Player newPlayer)
        {
            return UserDTO.registerUser(newPlayer);
        }

        public bool telephoneAlreadyExist(string telephone)
        {
            return UserDTO.telephoneAlreadyRegistered(telephone);
        }
    }
}
