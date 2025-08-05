using System.ComponentModel.DataAnnotations;

namespace Smart_OrdreMission.Models
{
    public class Mission
    {
    
        Person person { get; set; }
        public DateTime DateOfPaperIsuue { get; set; } = DateTime.Now;

        public DateTime DateTimeOfNow { get; set; } = DateTime.Now;

        [Display(Name = "Date Début de Mission")]
        [DataType(DataType.Date)]
        public DateTime DateStartMission { get; set; } 

        [Display(Name = "Date Fin de Mission")]
        [DataType(DataType.Date)]
        public DateTime DateFinishMission { get; set; } 

        [Display(Name = "Heure de Départ")]
        [DataType(DataType.Time)]
        public TimeSpan ArrivingTime { get; set; }

        [Display(Name = "Heure de Retour")]
        [DataType(DataType.Time)]
        public TimeSpan LeavingTime { get; set; }

        public string MissionObject { get; set; } 

        public string ReferenceOrdreMission { get; set; }

        public Mission()
        {
            ArrivingTime = new TimeSpan(8, 0, 0); // 08:00 AM
            LeavingTime = new TimeSpan(17, 0, 0);  // 05:00 PM
        }
    }
}
