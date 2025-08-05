namespace Smart_OrdreMission.Models
{
    public class PersonFormViewModel
    {
        public Person SelectedPerson { get; set; } = new Person();

        public List<Person> Persons { get; set; } = new List<Person>();

        public Mission mission { get; set; } = new Mission();
    }
}
