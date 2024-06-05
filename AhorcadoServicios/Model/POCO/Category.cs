using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "Category")]
    public class Category
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int CategoryID { get; set; }
        [Column]
        public string SpanishCategory { get; set; }
        [Column]
        public string EnglishCategory { get; set; }
    }
}