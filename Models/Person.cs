using System.ComponentModel.DataAnnotations;

namespace Smart_OrdreMission.Models
{
    public class Person
    {
        public string Name { get; set; }

        public int MatriculeSAP { get; set; }

       
        public int Echelle { get; set; }

        [Display(Name = "something")]
        public string Fonction { get; set; }
        public string Affectation { get; set; }

        public string Note { get; set; }

        public Itinerary itinerary { get; set; } = new Itinerary();


    }
}
