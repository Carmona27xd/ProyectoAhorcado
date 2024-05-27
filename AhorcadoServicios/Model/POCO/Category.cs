using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "categoria")]
    public class Category
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id_categoria { get; set; }
        [Column]
        public string categoria_espanol { get; set; }
        [Column]
        public string categoria_ingles { get; set; }
    }
}