using AutoMapper;
using MentalHealthApp.BusinessLogic.Implementation;
using MentalHealthApp.BusinessLogic.Implementation.Account;
using MentalHealthApp.BusinessLogic.Implementation.Account.Validations;
using MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels;
using MentalHealthApp.BusinessLogic.Implementation.Appointments.ViewModels;
using MentalHealthApp.Common.DTOs;
using MentalHealthApp.DataAccess;
using MentalHealthApp.Entities.Enums;
using MentalHealthApp.WebApp.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MentalHealthApp.WebApp.Controllers
{
    public class ProfileController : BaseController
    {
        private readonly UserProfileService _userProfileService;
        private readonly DoctorAccountService _doctorAccountService;
        private readonly ProgramareService _programareService;
        private readonly IMapper _mapper;
        public ProfileController(ControllerDependencies dependencies,
                                UserProfileService userProfileService,
                                DoctorAccountService doctorAccountService,
                                IMapper mapper,
                                ProgramareService programareService
                                )
                                : base(dependencies)
        {
            _userProfileService = userProfileService;
            _doctorAccountService = doctorAccountService;
            _mapper = mapper;
            _programareService = programareService;
        }

        [HttpGet]
        public async  Task<IActionResult> UserProfile()
        {
            var profile =  await _userProfileService.GetUserByEmail(CurrentUser.Email);
            return View(profile);
    
        }

        [HttpPost]
        public IActionResult EditUserProfile(UserVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userDto = _mapper.Map<EditUserDto>(model);
            _userProfileService.EditUserInfo(userDto);
            return RedirectToAction("UserProfile", model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUserProfile(string email)
        {
            var model = await _userProfileService.GetUserByEmail(email);
            return View("EditUserProfile",model);
        }

        [HttpPatch]
        public  IActionResult ChangeProfilePhoto(IFormFile userImage)
        {
            var model =  _userProfileService.ChangeProfilePhoto(userImage);
            return Ok(userImage);
        }
        [HttpGet]
        //[Authorize(Roles = "Specialist")]
        public async Task<IActionResult> DoctorProfile()
        {
            var profile = await _doctorAccountService.GetDoctorByEmail(CurrentUser.Email);
            return View("DoctorAccountProfile", profile);

        }
        //[HttpGet]
        //public  IActionResult DoctorsProfile()
        //{
        //    var profile = _doctorAccountService.GetDoctorsInfo();
        //    return View("DoctorsProfile", profile);

        //}
        [HttpGet]
        public IActionResult DoctorsProfile(int page)
        {
            if (page < 0)
            {
                page = 0;
            }
            int pageSize = 9;
            var doctorCards = _doctorAccountService.GetDoctorsInfo(page*pageSize, pageSize);
            var model = (doctorCards, page);
            return View("DoctorsProfile", model);

        }
        [HttpGet]
        public IActionResult UserProfileMap()
        {
            return View("UserProfileMap");

        }
        [HttpGet]
        public async Task<IActionResult> DoctorInfo(string email)
        {
            var profile = await  _doctorAccountService.GetDoctorByEmail(email);
           // var model = _mapper.Map<DoctorVM>(profile);
            return View("DoctorInfo", profile);

        }
        [HttpGet]
        public async Task<IActionResult> DoctorInfoById(Guid id)
        {
            var profile = await _doctorAccountService.GetDoctorById(id);
            // var model = _mapper.Map<DoctorVM>(profile);
            return View("DoctorInfo", profile);

        }

        [HttpGet]
        public IActionResult Appointments(Guid id)
        {

            var listaProgramari = _programareService.GetAppointments();
            var listaData = _programareService.GetAcceptedAppointmentsOnPage(id);
            var lista = _doctorAccountService.GetDoctorProgram(id);
            var model = new AppointmentModel()
            {

                SpecialistId = id,
                AppointmentsVMs = listaProgramari.Select(app => new AppointmentsVM
                {
                    Id = app.Id,
                    DataProgramare = app.DataProgramare,
                    Doctor = app.Doctor,
                    TipProgramare = app.TipProgramare,
                    StareProgramare = app.StareProgramare

                }).ToList(),
                AcceptedAppointmentsData = listaData.Select(ld => new AppointmentsDoctorProfileVM
                {
                    DataProgramare = ld.DataProgramare
                }).ToList(),
                DoctorWorkProgramVMs  = lista.Select(dwp => new DoctorWorkProgramVM
                {
                    Id = dwp.Id,
                    SpecialistId = dwp.SpecialistId,
                    WorkDayId = dwp.WorkDayId,
                    WorkDay = dwp.WorkDay,
                    Start = dwp.Start,
                    End = dwp.End,
                    Notes = dwp.Notes
                }).ToList()
            };
 
            return View("Appointments", model);

        }

        [HttpGet]
        public IActionResult UserTodayAppointments()
        {
            var appointments = _programareService.GetUserTodayAppointments();
            var model = new AcceptedAppointmentsVM()
            {


                AcceptedAppointmentsVMs = appointments.Select(app => new AcceptedAppointmentsVM
                {
                    Id = app.Id,
                    DataProgramare = app.DataProgramare,
                    Specialist = app.Specialist,
                    Email = app.Email,
                    PhoneNumber = app.PhoneNumber,
                    UserImage = app.UserImage

                }).ToList()
            };
            return View("UserTodayAppointments", model);
        }
    }
}
