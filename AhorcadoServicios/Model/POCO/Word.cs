using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "palabra")]
    public class Word
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id_palabra { get; set; }
        [Column]
        public int id_categoria { get; set; }
        [Column]
        public string palabra_espanol { get; set; }
        [Column]
        public string palabra_ingles { get; set; }
        [Column]
        public string pista_espanol { get; set; }
        [Column]
        public string pista_ingles { get; set; }

    }
}