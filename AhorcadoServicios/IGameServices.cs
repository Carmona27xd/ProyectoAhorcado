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
        bool createMatch(Match createMatch);
        [OperationContract]
        List<Match> getMatchList();

    }
}
