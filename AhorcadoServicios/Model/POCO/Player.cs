using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Web;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "Player")]
    public class Player
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int PlayerID { get; set; }
        [Column]
        public string Email { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public string NickName { get; set; }
        [Column]
        public string Names { get; set; }
        [Column]
        public string FirstSurname { get; set; }
        [Column]
        public string SecondSurname { get; set; }
        [Column]
        public string BirthDate { get; set; }
        [Column]
        public string PhoneNumber { get; set; }
        [Column]
        public int PointsEarned { get; set; }
    }
}