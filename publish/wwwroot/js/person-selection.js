
document.addEventListener("DOMContentLoaded", function () {
    const personSelect = document.getElementById("my-select");
    const echelleLabel = document.getElementById("EchelleLabel");
    const fonctionLabel = document.getElementById("FonctionLabel");
    const SAPMatricule = document.getElementById("SAPMatricule");
    const ItinerarySelect = document.getElementById("my-select-itineraire");

    let selectedPerson = null; // Declare a variable to hold the selected person

    personSelect.addEventListener("change", function () {
        const selectedName = this.value;
        selectedPerson =  persons.find(p => p.Name === selectedName);

        if (selectedPerson) {
            echelleLabel.value = selectedPerson.Echelle;

            //echelleLabel.textContent = selectedPerson.Echelle;
            fonctionLabel.value = selectedPerson.Fonction;
            SAPMatricule.value = selectedPerson.MatriculeSAP;
        } else {
            echelleLabel.value = '';
            fonctionLabel.value = '';
            SAPMatricule.value = '';
        }
    });

    //ItinerarySelect.addEventListener("change", function () {
    //    if (selectedPerson) {
    //        selectedPerson.itinerary = selectedPerson.itinerary || {};
    //        selectedPerson.itinerary.Itineraire = this.options[this.selectedIndex].text;
    //    }
    //    console.log(selectedPerson.itinerary.Itineraire); 

    //});

});
