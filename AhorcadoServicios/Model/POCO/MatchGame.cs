using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace AhorcadoServicios.Model.POCO
{
    [Table(Name = "Match")]
    public class MatchGame
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int MatchID { get; set; }
        [Column]
        public int? WinnerID { get; set; }
        [Column]
        public int? GuestID { get; set; }
        [Column]
        public int ChallengerID { get; set; }
        [Column]
        public int WordID { get; set; }
        [Column]
        public int StatusMatchID { get; set; }
        [Column]
        public string DateMatch { get; set; }
        [Column]
        public char? SelectLetter { get; set; }
        [Column]
        public int RemainingAttempts { get; set; }
        [Column]
        public string NickNameChallenger { get; set; }
        [Column]
        public string EmailChallenger { get; set; }
        [Column]
        public int MatchLanguage { get; set; }

    }
}