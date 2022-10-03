using MentalHealthApp.BusinessLogic.Implementation;
using MentalHealthApp.BusinessLogic.Implementation.Account;
using MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels;
using MentalHealthApp.BusinessLogic.Implementation.Appointments.ViewModels;
using MentalHealthApp.Entities.Enums;
using MentalHealthApp.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentalHealthApp.WebApp.Controllers
{
    public class DoctorAccountController : BaseController
    {
        private readonly ProgramareService _programareService;
        private readonly DoctorAccountService _doctorAccountService;
        public DoctorAccountController(ControllerDependencies dependencies,
                                       ProgramareService programareService,
                                       DoctorAccountService doctorAccountService) 
                                       : base(dependencies) 
        {
            _programareService = programareService;
            _doctorAccountService = doctorAccountService;
        }

        [Authorize(Roles = "Specialist")]
        [HttpGet]
        public IActionResult AwaitingAppointments()
        {
            var appointments =  _programareService.GetAllAwaitingAppointments();
            var model = new AppointmentsDoctorProfileVM()
            {


                AppointmentDoctorProfileVMs = appointments.Select(app => new AppointmentsDoctorProfileVM
                {
                    Id = app.Id,
                    DataProgramare = app.DataProgramare,
                    Pacient = app.Pacient,
                    StareProgramare = app.StareProgramare
                }).ToList()
            };
            return View("AwaitingAppointments", model);
        }

        [Authorize(Roles = "Specialist")]
        [HttpGet]
        public IActionResult OnlineAppointments()
        {
            var appointments = _programareService.GetAllOnlineAppointments();
            var model = new AcceptedAppointmentsVM()
            {


                AcceptedAppointmentsVMs = appointments.Select(app => new AcceptedAppointmentsVM
                {
                    Id = app.Id,
                    DataProgramare = app.DataProgramare,
                    Pacient = app.Pacient,
                    BirthDate = app.BirthDate,
                    Email = app.Email,
                    PhoneNumber = app.PhoneNumber,
                    Categorie = app.Categorie,
                    Asigurat = app.Asigurat,
                    StareProgramare = app.StareProgramare,
                    UserImage = app.UserImage
                    
                }).ToList()
            };
            return View("OnlineAppointments", model);
        }

        [Authorize(Roles = "Specialist")]
        [HttpGet]
        public IActionResult PhysicalAppointments()
        {
            var appointments = _programareService.GetAllPhysicalAppointments();
            var model = new AcceptedAppointmentsVM()
            {


                AcceptedAppointmentsVMs = appointments.Select(app => new AcceptedAppointmentsVM
                {
                    Id = app.Id,
                    DataProgramare = app.DataProgramare,
                    Pacient = app.Pacient,
                    BirthDate = app.BirthDate,
                    Email = app.Email,
                    PhoneNumber = app.PhoneNumber,
                    Categorie = app.Categorie,
                    Asigurat = app.Asigurat,
                    StareProgramare = app.StareProgramare

                }).ToList()
            };
            return View("PhysicalAppointments", model);
        }

        [Authorize(Roles = "Specialist")]
        [HttpGet]
        public IActionResult TodayAppointments()
        {
            var appointments = _programareService.GetTodayAppointments();
            var model = new AcceptedAppointmentsVM()
            {


                AcceptedAppointmentsVMs = appointments.Select(app => new AcceptedAppointmentsVM
                {
                    Id = app.Id,
                    DataProgramare = app.DataProgramare,
                    Pacient = app.Pacient,
                    BirthDate = app.BirthDate,
                    Email = app.Email,
                    PhoneNumber = app.PhoneNumber,
                    Categorie = app.Categorie,
                    Asigurat = app.Asigurat,
                    UserImage = app.UserImage

                }).ToList()
            };
            return View("TodayAppointments", model);
        }

        [Authorize(Roles = "Specialist")]
        [HttpPost]
        public IActionResult AcceptAppointment(int appointmentId, string stare)
        {
            var result = _programareService.EditStatus(appointmentId, stare);
            return Ok(stare);
        }

        [Authorize(Roles = "Specialist")]
        [HttpGet]
        public IActionResult NumberOfAppointments()
        {
            var appointments = _programareService.AwaitingAppoinments();
            return Ok(appointments);
        }

        [Authorize(Roles = "Specialist")]
        [HttpPost]
        public  IActionResult AddWorkProgram(DoctorWorkProgramVM model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("WorkProgram");
            }
            _doctorAccountService.AddWorkProgram(model);
            return RedirectToAction("WorkProgram");
        }

       
        [HttpGet]
        public IActionResult WorkProgram()
        {
            var lista = _doctorAccountService.GetProgram();
            var workDays = _doctorAccountService.GetWorkDays();
           
            var model = new DoctorWorkProgramVM()
            {
                DoctorWorkProgramVMs = lista.Select(dwp => new DoctorWorkProgramVM
                {
                    Id = dwp.Id,
                    SpecialistId = dwp.SpecialistId,
                    WorkDayId = dwp.WorkDayId,
                    WorkDay = dwp.WorkDay,
                    Start = dwp.Start,
                    End = dwp.End,
                    Notes = dwp.Notes

                }).ToList(),
                workDaysVMs = workDays.Select(wd => new WorkDaysVM
                {
                    Id = wd.Id,
                    Name = wd.Name
                }).ToList()
            };
            return View("WorkProgram", model);
        }
    }
}
