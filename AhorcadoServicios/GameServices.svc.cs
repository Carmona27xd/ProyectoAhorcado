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
        public bool createMatch(Match newMatch)
        {
            return MatchDTO.createMatch(newMatch);
        }

        public List<Match> getMatchList()
        {
            return MatchDTO.getMatchesAvaliables();
        }
    }
}
