﻿using MentalHealthApp.BusinessLogic.Implementation;
using MentalHealthApp.BusinessLogic.Implementation.Account;
using MentalHealthApp.BusinessLogic.Implementation.Appointments.ViewModels;
using MentalHealthApp.Entities.Enums;
using MentalHealthApp.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentalHealthApp.WebApp.Controllers
{
    public class ProgramariController : BaseController
    {
        private readonly ProgramareService _programareService;
        private readonly DoctorAccountService _doctorAccountService;
     public ProgramariController(ControllerDependencies dependencies,
                                 ProgramareService programareService,
                                 DoctorAccountService doctorAccountService) : base(dependencies)
        { 
            _programareService = programareService;
            _doctorAccountService = doctorAccountService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Appointments()
        {

            var model = _programareService.GetAppointments();
            return View(model);
        }

        [Authorize(Roles = "Pacient, Specialist" )]
        [HttpPost]
        public IActionResult CreateAppointment(AppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Appointments", "Profile", new { id = model.SpecialistId });
            }
            var check =  _programareService.CheckDate(model.DataProgramare, model.SpecialistId);
            if(check == false)
            {
                return StatusCode(StatusCodes.Status409Conflict, "Error - Appointment time does not check doctor work program");
            }
            _programareService.AddDoctorAppointment(model);
            return RedirectToAction("Appointments", "Profile", new { id = model.SpecialistId });
        }



    }
}
