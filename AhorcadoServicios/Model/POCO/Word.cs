using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "Word")]
    public class Word
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int WordID { get; set; }
        [Column]
        public int CategoryID { get; set; }
        [Column]
        public string SpanishWord { get; set; }
        [Column]
        public string EnglishWord { get; set; }
        [Column]
        public string SpanishClue { get; set; }
        [Column]
        public string EnglishClue { get; set; }

    }
}