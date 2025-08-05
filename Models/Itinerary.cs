namespace Smart_OrdreMission.Models
{
    public class Itinerary
    {
        private string _itineraire;

        public string Itineraire
        {
            get => _itineraire;
            set
            {
                _itineraire = value;
                ParseItineraire(value);
            }
        }


        public string StartPoint { get; set; }
        public string MissionPoint { get; set; }
        public string ReturnPoint { get; set; }


        private void ParseItineraire(string itineraire)
        {
            if (!string.IsNullOrWhiteSpace(itineraire))
            {
                var parts = itineraire.Split('-', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 3)
                {
                    StartPoint = parts[0];
                    MissionPoint = parts[1];
                    ReturnPoint = parts[2];
                }
            }
        }
    }
}
