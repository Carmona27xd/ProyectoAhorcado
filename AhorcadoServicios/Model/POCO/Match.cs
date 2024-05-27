using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "partida")]
    public class Match
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id_partida { get; set; }
        [Column]
        public int? id_ganador { get; set; }
        [Column]
        public int? id_retador { get; set; }
        [Column]
        public int id_creador { get; set; }
        [Column]
        public int id_palabra { get; set; }
        [Column]
        public int estado_partida { get; set; }
        [Column]
        public string fecha_partida { get; set; }
        [Column]
        public char? letra_seleccionada { get; set; }
        [Column]
        public int intentos_restantes { get; set; }
        [Column]
        public string nickname_creador { get; set; }
        [Column]
        public string correo_creador { get; set; }

    }
}