using MentalHealthApp.BusinessLogic.Base;
using MentalHealthApp.Common.Exceptions;
using MentalHealthApp.Entities.Enums;
using MentalHealthApp.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using MentalHealthApp.BusinessLogic.Implementation.Appointments.ViewModels;
using MentalHealthApp.Common.DTOs;
using MentalHealthApp.BusinessLogic.Implementation.Admin.ViewModels;
using MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels;
using MentalHealthApp.BusinessLogic.Implementation.Account;
using System.Data;
using MentalHealthApp.BusinessLogic.Implementation.Appointments.Validations;
using MentalHealthApp.Common.Extensions;

namespace MentalHealthApp.BusinessLogic.Implementation
{
    public class ProgramareService : BaseService
    {
        private readonly DoctorAccountService _doctorAccountService;
        private readonly AppointmentValidation _appointmentValidation;
        public ProgramareService(ServiceDependencies dependencies,
                                DoctorAccountService doctorAccountService) : base(dependencies)
        {
            _doctorAccountService = doctorAccountService;
            _appointmentValidation = new AppointmentValidation();
        }


        public string GetDoctorNameById(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var name = uow.IdentityUsers.Get().Where(n => n.Id.Equals(id)).Select(n => $"{n.LastName} {n.FirstName}").Single();
                return name;
            });
        }
        public void AddDoctorAppointment(AppointmentModel model)
        {
            _appointmentValidation.Validate(model).ThenThrow(model);

            ExecuteInTransaction(uow =>
            {

                var programare = uow.Programari.Get()
                                               .SingleOrDefault(s => s.DataProgramare.Equals(model.DataProgramare));
                if(programare != null)
                {
                    string message = "Exista deja o programare pentru  " + model.DataProgramare ;
                    throw new AlreadyExistsInDB(nameof(Programare), message);
                }
                else
                {
                    var specialist = uow.Specialisti.Get().Single(c => c.SpecialistId.Equals(model.SpecialistId));
                    var utilizatorId = uow.IdentityUsers
                                        .Get()
                                        .Include(iu => iu.Utilizator)
                                        .Single(iu => iu.Id.Equals(CurrentUser.Id.Value))
                                        .Utilizator.UserId;
                    var user = uow.Utilizatori.Get().Single(c => c.UserId.Equals(utilizatorId));
                    var newAppointment = new Programare
                    {
                       // OraProgramare = model.OraProgramare,
                        DataProgramare = model.DataProgramare,
                        TipProgramare = model.TipProgramare,
                        StareProgramare = AppointmentStatus.In_Asteptare.ToString(),
                        Specialist = specialist,
                        Utilizator = user
                    };
                    uow.Programari.Insert(newAppointment);
                    uow.SaveChanges();
                }

            });
        }
        public IEnumerable<AppointmentsAdminModel> GetAllAppointmentsAdmin()
        {
            return ExecuteInTransaction(uow =>
            {
              
                var appointments = uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Include(p => p.Specialist)
                                     .Select(u => new AppointmentsAdminModel
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,

                                         //  OraProgramare = u.OraProgramare,
                                         SpecialistId = u.SpecialistId,
                                         DoctorName = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         UtilizatorId = u.UtilizatorId,
                                         UserName = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         TipProgramare = u.TipProgramare,
                                         StareProgramare = u.StareProgramare
                                     });
                var result = appointments.OrderBy(u => Convert.ToDateTime(u.DataProgramare).Date)
                                     .OrderBy(u => Convert.ToDateTime(u.DataProgramare).Hour);
                return result;


            });
        }

        public IEnumerable<AppointmentsAdminModel> GetOnePageAppointmentsAdmin(int pages, int pageSize)
        {
            return ExecuteInTransaction(uow =>
            {

                var appointments = uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Include(p => p.Specialist)
                                      .Skip(pages).Take(pageSize)
                                     .Select(u => new AppointmentsAdminModel
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,

                                         //  OraProgramare = u.OraProgramare,
                                         SpecialistId = u.SpecialistId,
                                         DoctorName = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         UtilizatorId = u.UtilizatorId,
                                         UserName = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         TipProgramare = u.TipProgramare,
                                         StareProgramare = u.StareProgramare
                                     }).ToList();
                
                var result = appointments.OrderBy(u => Convert.ToDateTime(u.DataProgramare).Date)
                                     .OrderBy(u => Convert.ToDateTime(u.DataProgramare).Hour);
                return result;


            });
        }
        public List<AppointmentsAdminModel> SearchAppointmentsByDoctorName(string doctorName)
        {
            return ExecuteInTransaction(uow =>
            {
                var appointments =  uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Include(p => p.Specialist)
                                     .Select(u => new AppointmentsAdminModel
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,
                                         SpecialistId = u.SpecialistId,
                                         DoctorName = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         UtilizatorId = u.UtilizatorId,
                                         UserName = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         TipProgramare = u.TipProgramare,
                                         StareProgramare = u.StareProgramare
                                     })
                                    .ToList();
                var result = appointments.Where(u => u.DoctorName.Contains(doctorName)).ToList().OrderBy(u => Convert.ToDateTime(u.DataProgramare).Date)
                                     .OrderBy(u => Convert.ToDateTime(u.DataProgramare).Hour).ToList(); 
                return result;

            });
        }

        public bool CheckDate(string appointmentDate, Guid doctorId)
        {
            var time = _doctorAccountService.GetDoctorProgramById(doctorId);
            var countAppointments = GetAcceptedAppointments(doctorId)
                                .Where(u => Convert.ToDateTime(u.DataProgramare).Day
                                .Equals(Convert.ToDateTime(appointmentDate).Day)
                                       && (Convert.ToDateTime(appointmentDate).Minute - Convert.ToDateTime(u.DataProgramare).Minute < time))
                                .Count();
            if (countAppointments > 0)
            {
                return false;
            }

            var lista = _doctorAccountService.GetDoctorProgram(doctorId);
            if(lista == null)
            {
                return true;
            }
            foreach (var item in lista)
            {
                if (Convert.ToDateTime(item.Start).Hour > Convert.ToDateTime(appointmentDate).Hour && item.WorkDay == Convert.ToDateTime(appointmentDate).DayOfWeek.ToString())
                {
                    return false;

                  
                }
                if((Convert.ToDateTime(item.Start).Hour == Convert.ToDateTime(appointmentDate).Hour 
                && Convert.ToDateTime(item.Start).Minute > Convert.ToDateTime(appointmentDate).Minute) 
                && item.WorkDay == Convert.ToDateTime(appointmentDate).DayOfWeek.ToString())
                {
                    return false;
                }
                   
                if ((Convert.ToDateTime(item.End).Hour < Convert.ToDateTime(appointmentDate).Hour 
                || (Convert.ToDateTime(item.End).Hour == Convert.ToDateTime(appointmentDate).Hour) 
                && Convert.ToDateTime(item.End).Minute < Convert.ToDateTime(appointmentDate).Minute) 
                && item.WorkDay == Convert.ToDateTime(appointmentDate).DayOfWeek.ToString())
                {
                    return false;
                } 

            }
            
            return true;
            }
        public IEnumerable<DoctorNameModel> GetDoctorsName()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Programari.Get()
                                      .Include(p => p.Specialist)
                                     .Select(u => new DoctorNameModel
                                     {
                                         SpecialistId = u.SpecialistId,
                                         DoctorName = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),

                                     }).Distinct();
                          
                

            });
        }
        public List<ProgramareDto> GetAppointments()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Programari.Get()
                                      .Include(p => p.Specialist)
                                      .Where(p => p.UtilizatorId.Equals(CurrentUser.Id.Value))
                                     .Select(u => new ProgramareDto
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,
                                         Doctor = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         TipProgramare = u.TipProgramare,
                                         StareProgramare = u.StareProgramare,

                                     })
                                    .ToList();


            });
        }
        public List<ProgramareProfilMedicDto> GetAllAwaitingAppointments()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Where(p => p.SpecialistId.Equals(CurrentUser.Id.Value) && p.StareProgramare==AppointmentStatus.In_Asteptare.ToString())
                                     .Select(u => new ProgramareProfilMedicDto
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,
                                         Pacient = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         StareProgramare = u.StareProgramare
                                     })
                                    .ToList();


            });
        }
        public List<AppointmentsDoctorProfileVM> GetAcceptedAppointments(Guid doctorId)
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Where(p => !(p.UtilizatorId.Equals(CurrentUser.Id.Value)) && p.SpecialistId.Equals(doctorId) && p.StareProgramare == AppointmentStatus.Programare_Acceptata.ToString())
                                      .Select(u => new AppointmentsDoctorProfileVM
                                      {
                                          DataProgramare = u.DataProgramare,
                                      })
                                    .ToList();


            });
        }

        public List<AppointmentsDoctorProfileVM> GetAcceptedAppointmentsOnPage(Guid doctorId)
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Where(p => !(p.UtilizatorId.Equals(CurrentUser.Id.Value)) && p.SpecialistId.Equals(doctorId) && p.StareProgramare == AppointmentStatus.Programare_Acceptata.ToString())
                                      .Select(u => new AppointmentsDoctorProfileVM
                                     {
                                         DataProgramare = u.DataProgramare,
                                     })
                                    .ToList();


            });
        }
        public List<AcceptedAppointmentsDto> GetAllOnlineAppointments()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Where(p => p.SpecialistId.Equals(CurrentUser.Id.Value) && p.StareProgramare != AppointmentStatus.In_Asteptare.ToString() && p.TipProgramare == "Online")
                                     .Select(u => new AcceptedAppointmentsDto
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,
                                         Pacient = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         Email = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.Email).Single(),
                                         BirthDate = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.BirthDate).Single(),
                                         PhoneNumber = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.PhoneNumber).Single(),
                                         Categorie = uow.Utilizatori.Get().Where(n => n.UserId.Equals(u.UtilizatorId)).Select(n => n.Categorie).Single(),
                                         Asigurat = uow.Utilizatori.Get().Where(n => n.UserId.Equals(u.UtilizatorId)).Select(n => n.Asigurat).Single(),
                                         StareProgramare = u.StareProgramare, 
                                         UserImage = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.UserImage).Single(),
                                     })
                                    .ToList();


            });
        }
        public List<AcceptedAppointmentsDto> GetAllPhysicalAppointments()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Where(p => p.SpecialistId.Equals(CurrentUser.Id.Value) && p.StareProgramare != AppointmentStatus.In_Asteptare.ToString() && p.TipProgramare == "Fizic")
                                     .Select(u => new AcceptedAppointmentsDto
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,
                                         Pacient = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         Email = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.Email).Single(),
                                         BirthDate = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.BirthDate).Single(),
                                         PhoneNumber = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.PhoneNumber).Single(),
                                         Categorie = uow.Utilizatori.Get().Where(n => n.UserId.Equals(u.UtilizatorId)).Select(n => n.Categorie).Single(),
                                         Asigurat = uow.Utilizatori.Get().Where(n => n.UserId.Equals(u.UtilizatorId)).Select(n => n.Asigurat).Single(),
                                         StareProgramare = u.StareProgramare
                                     })
                                    .ToList();


            });
        }
        public List<AcceptedAppointmentsDto> GetTodayAppointments()
        {
            return ExecuteInTransaction(uow =>
            {
                var appointments = uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Where(p => p.SpecialistId.Equals(CurrentUser.Id.Value) && p.TipProgramare == "Online" && p.StareProgramare == AppointmentStatus.Programare_Acceptata.ToString())
                                      .ToList()
                                      .Select(u => new AcceptedAppointmentsDto
                                     {
                                         Id = u.Id,
                                         DataProgramare = u.DataProgramare,
                                         Pacient = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                         Email = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.Email).Single(),
                                         BirthDate = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.BirthDate).Single(),
                                         PhoneNumber = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.PhoneNumber).Single(),
                                         Categorie = uow.Utilizatori.Get().Where(n => n.UserId.Equals(u.UtilizatorId)).Select(n => n.Categorie).Single(),
                                         Asigurat = uow.Utilizatori.Get().Where(n => n.UserId.Equals(u.UtilizatorId)).Select(n => n.Asigurat).Single(),
                                         UserImage = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.UtilizatorId)).Select(n => n.UserImage).Single()
                                      })
                                     .OrderBy(u => Convert.ToDateTime(u.DataProgramare))
                                    .ToList();

                var result = appointments.Where(u => Convert.ToDateTime(u.DataProgramare).Date == DateTime.Now.Date).ToList();
                return result;


            });
        }

        public List<AcceptedAppointmentsDto> GetUserTodayAppointments()
        {
            return ExecuteInTransaction(uow =>
            {
                var appointments = uow.Programari.Get()
                                      .Include(p => p.Specialist)
                                      .Where(p => p.UtilizatorId.Equals(CurrentUser.Id.Value) && p.TipProgramare == "Online" && p.StareProgramare == AppointmentStatus.Programare_Acceptata.ToString())
                                      .ToList()
                                      .Select(u => new AcceptedAppointmentsDto
                                      {
                                          Id = u.Id,
                                          DataProgramare = u.DataProgramare,
                                          Specialist = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => $"{n.LastName} {n.FirstName}").Single(),
                                          Email = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => n.Email).Single(),
                                          PhoneNumber = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => n.PhoneNumber).Single(),
                                          UserImage = uow.IdentityUsers.Get().Where(n => n.Id.Equals(u.SpecialistId)).Select(n => n.UserImage).Single()
                                      })
                                     .OrderBy(u => Convert.ToDateTime(u.DataProgramare))
                                    .ToList();

                var result = appointments.Where(u => Convert.ToDateTime(u.DataProgramare).Date == DateTime.Now.Date).ToList();
                return result;


            });
        }

        public int AwaitingAppoinments()
        {
            return ExecuteInTransaction(uow =>
            {
              return  uow.Programari.Get()
                                      .Include(p => p.Utilizator)
                                      .Where(p => p.SpecialistId.Equals(CurrentUser.Id.Value) && p.StareProgramare == AppointmentStatus.In_Asteptare.ToString())
                                      .Count();

            });
        }
        public  string EditStatus(int AppoitmentId, string newStatus)
        {
            return ExecuteInTransaction(uow =>
            {
                var status = uow.Programari.Get()
                                            .Where(p => p.Id.Equals(AppoitmentId))
                                            .SingleOrDefault();
                status.StareProgramare = newStatus;
                uow.Programari.Update(status);
                uow.SaveChanges();
                return newStatus.ToString();
            });

        }
        public string EditTime(int AppoitmentId, string newStatus)
        {
            return ExecuteInTransaction(uow =>
            {
                var status = uow.Programari.Get()
                                            .Where(p => p.Id.Equals(AppoitmentId))
                                            .SingleOrDefault();
                status.DataProgramare = newStatus;
                uow.Programari.Update(status);
                uow.SaveChanges();
                return newStatus;
            });

        }
        public async Task<Programare> DeleteAppointment(int id)
        {

            return ExecuteInTransaction(uow =>
            {

                var app = uow.Programari.Get()
                                                .Where(cd => cd.Id.Equals(id))
                                                .Single();
                uow.Programari.Delete(app);
                uow.SaveChanges();
                return app;

            });
        }
    }
}
