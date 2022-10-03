using MentalHealthApp.WebApp.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace MentalHealthApp.WebApp.Controllers
{
    public class ProspectController : BaseController
    {

        public ProspectController(
                              ControllerDependencies dependencies) : base(dependencies)
        {}

        [HttpGet]
        public IActionResult MedicationSearch()
        {
            return View("MedicationSearch");
        }

    }
}
