using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Web;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "usuario")]
    public class User
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id_cuenta { get; set; }
        [Column]
        public string correo { get; set; }
        [Column]
        public string password { get; set; }
        [Column]
        public string nickname { get; set; }
        [Column]
        public string nombres { get; set; }
        [Column]
        public string apellido_paterno { get; set; }
        [Column]
        public string apellido_materno { get; set; }
        [Column]
        public string fecha_nacimiento { get; set; }
        [Column]
        public string telefono { get; set; }
        [Column]
        public int puntos_obtenidos { get; set; }
    }
}