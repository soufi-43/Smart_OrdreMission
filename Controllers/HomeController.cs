using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Smart_OrdreMission.Models;
using System.Diagnostics;


namespace Smart_OrdreMission.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;


        List<Person> people = new List<Person>
            {
                new Person { Name = "Alice", MatriculeSAP = 12345, Echelle = 10, Fonction = "Developer" , Affectation="SP2 DJAMAA" },
                new Person { Name = "Bob", MatriculeSAP = 67890, Echelle = 8, Fonction = "Manager",Affectation="SP2 DJAMAA" },
                new Person { Name = "Charlie", MatriculeSAP = 11121, Echelle = 12, Fonction = "Analyst",Affectation="SP2 DJAMAA" }
            };

        public HomeController(ILogger<HomeController> logger , IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var viewModel = new PersonFormViewModel
            {
                Persons = people
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SavePerson(PersonFormViewModel performModel)
        {
            if (!ModelState.IsValid)
            {
              
                return View("Indexx", performModel); // ✅ correct model type
            }

            // Save logic here...

            return RedirectToAction("Index");

        }

        [HttpPost]
        public IActionResult SavePersonn(Person selectedPerson)
        {
            if (!ModelState.IsValid)
            {
                var model = new PersonFormViewModel
                {
                    SelectedPerson = selectedPerson,
                    Persons = people // repopulate the dropdown list
                };

                return View("Indexx", model); // ✅ correct model type
            }

            // Save logic here...

            return RedirectToAction("Index");
        }





        [HttpPost]
        public IActionResult ExportMission(PersonFormViewModel model)
        {
            // Set EPPlus license
            ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization");

            // Load the Excel template from wwwroot/templates
            string templatePath = Path.Combine(_webHostEnvironment.WebRootPath, "templates", "OrdreTemplate.xlsm");

            using var package = new ExcelPackage(new FileInfo(templatePath));
            var ws = package.Workbook.Worksheets["MASQUE DE SAISIE"]; // Adjust the sheet name if necessary


            ws.Cells["E19:L35"].Clear();

            // Fill Excel fields from the model
            ws.Cells["B7"].Value = model.SelectedPerson.Name;
            ws.Cells["N8"].Value = model.SelectedPerson.MatriculeSAP;
            ws.Cells["B8"].Value = model.SelectedPerson.Fonction;
            ws.Cells["G9"].Value = model.SelectedPerson.Echelle;
            ws.Cells["j7"].Value = model.SelectedPerson.itinerary.Itineraire;

            ws.Cells["K9"].Value = model.mission.ReferenceOrdreMission;
            ws.Cells["M9"].Value = model.mission.DateOfPaperIsuue;
            ws.Cells["F10"].Value = model.mission.DateStartMission;
            ws.Cells["F11"].Value = model.mission.DateStartMission;
            ws.Cells["F12"].Value = model.mission.DateFinishMission;
            ws.Cells["F13"].Value = model.mission.DateFinishMission;
            ws.Cells["B15"].Value = model.mission.MissionObject;





            // Save to memory stream instead of disk
             var stream = new MemoryStream();
            package.SaveAs(stream);
            stream.Position = 0;

            // Return the file for download
            return File(
                stream,
                "application/vnd.ms-excel.sheet.macroEnabled.12",
                $"Mission_{model.SelectedPerson.Name}.xlsm"
            );
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
