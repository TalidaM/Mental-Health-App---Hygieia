using MentalHealthApp.BusinessLogic.Base;
using MentalHealthApp.BusinessLogic.Implementation.Account.Validations;
using MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels;
using MentalHealthApp.Common;
using MentalHealthApp.Common.DTOs;
using MentalHealthApp.Common.Exceptions;
using MentalHealthApp.Common.Extensions;
using MentalHealthApp.DataAccess.Features;
using MentalHealthApp.Entities;
using MentalHealthApp.Entities.Entities;
using MentalHealthApp.Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Account
{
    public class DoctorAccountService : BaseService
    {
        private readonly IHashAlgo _hashAlgo;
        private readonly RegisterDoctorValidator _registerDoctorValidator;

        public DoctorAccountService(IHashAlgo hashAlgo, ServiceDependencies dependencies) :base(dependencies)
        {
            _hashAlgo = hashAlgo;
            _registerDoctorValidator = new RegisterDoctorValidator(UnitOfWork);
        }
      
            public List<EditDoctorDto> GetAllDoctors()
        {
            return ExecuteInTransaction(uow =>
            {
                var query = uow.IdentityUsers.Get()
                                      .Include(u => u.Adresa)
                                      .Include(u => u.Specialist)
                                      .Include(u => u.Roles).ToList();
                return query.Where(u => u.Roles.Select(r => r.Id).Contains((int)RoleTypes.Specialist))
                                      .Select(u => new EditDoctorDto

                                       {
                                           LastName = u.LastName,
                                           FirstName = u.FirstName,
                                           Email = u.Email,
                                           PhoneNumber = u.PhoneNumber,
                                           Specialitate = u.Specialist.Specialitate,
                                           Descriere = u.Specialist.Descriere,
                                           durataProgramare = u.Specialist.durataProgramare,
                                           Tara = u.Adresa.Tara,
                                           Judet = u.Adresa.Judet,
                                           Oras = u.Adresa.Oras,
                                           StradaNumar = u.Adresa.StradaNumar,
                                           BlocScaraApartament = u.Adresa.BlocScaraApartament,
                                           Sector = u.Adresa.Sector,
                                           CodPostal = u.Adresa.CodPostal

                                       })
                                      .ToList();

            });
        }

        public List<EditDoctorDto> GetPageDoctors(int pages, int numberOnPage)
        {
            return ExecuteInTransaction(uow =>
            {
                var query = uow.IdentityUsers.Get()
                                      .Include(u => u.Adresa)
                                      .Include(u => u.Specialist)
                                      .Include(u => u.Roles).ToList();
                return query.Where(u => u.Roles.Select(r => r.Id).Contains((int)RoleTypes.Specialist))
                .Skip(pages).Take(numberOnPage)
                .Select(u => new EditDoctorDto

                                      {
                                          LastName = u.LastName,
                                          FirstName = u.FirstName,
                                          Email = u.Email,
                                          PhoneNumber = u.PhoneNumber,
                                          Specialitate = u.Specialist.Specialitate,
                                          Descriere = u.Specialist.Descriere,
                                          durataProgramare = u.Specialist.durataProgramare,
                                          Tara = u.Adresa.Tara,
                                          Judet = u.Adresa.Judet,
                                          Oras = u.Adresa.Oras,
                                          StradaNumar = u.Adresa.StradaNumar,
                                          BlocScaraApartament = u.Adresa.BlocScaraApartament,
                                          Sector = u.Adresa.Sector,
                                          CodPostal = u.Adresa.CodPostal

                                      })
                                      .ToList();

            });
        }

        public byte[] ConvertToBytes(IFormFile image)
        {
            BinaryReader reader = new BinaryReader(image.OpenReadStream());
            var imageBytes = reader.ReadBytes((int)image.Length);
            return imageBytes;
        }
        public List<DoctorsCardsDto> GetDoctorsInfo(int pages, int numberOnPage)
        {
            return ExecuteInTransaction(uow =>
            {
                var query = uow.IdentityUsers.Get()
                                      .Include(u => u.Adresa)
                                      .Include(u => u.Specialist)
                                      .Include(u => u.Roles)
                                      .ToList();
                return query.Where(u => u.Roles.Select(r => r.Id).Contains((int)RoleTypes.Specialist))
                                      .Select(u => new DoctorsCardsDto

                                      {
                                          DoctorImage = u.UserImage,
                                          LastName = u.LastName,
                                          FirstName = u.FirstName,
                                          PhoneNumber = u.PhoneNumber,
                                          Email = u.Email

                                      })
                                      .Skip(pages).Take(numberOnPage)
                                      .ToList();

            });
        }
      
        public void RegisterNewDoctor(RegisterDoctorModel model)
        {
            _registerDoctorValidator.Validate(model).ThenThrow(model);

            ExecuteInTransaction(uow =>
            {
                
                var user = uow.IdentityUsers.Get()
                                                    .Include(u => u.Roles)
                    .SingleOrDefault(u => u.Email.Equals(model.Email));
                if (user != null)
                {
                    string message = "Userul " + model.Email + " este deja inregistrat.";
                    throw new AlreadyExistsInDB(nameof(IdentityUser), message);
                }
                else
                {
                    var adresa = new Adresa();
                    adresa.Tara = model.Tara;
                    adresa.Judet = model.Judet;
                    adresa.Oras = model.Oras;
                    adresa.StradaNumar = model.StradaNumar;
                    adresa.BlocScaraApartament = model.BlocScaraApartament;
                    adresa.Sector = model.Sector;
                    adresa.CodPostal = model.CodPostal;
                    uow.Adrese.Insert(adresa);

                    var registeredUser = new IdentityUser();
                    registeredUser.Id = Guid.NewGuid();
                    registeredUser.FirstName = model.FirstName;
                    registeredUser.LastName = model.LastName;
                    registeredUser.BirthDate = model.BirthDate;
                    registeredUser.Email = model.Email;
                    registeredUser.EmailConfirmed = false;
                    registeredUser.PhoneNumberCountryPrefix = model.PhoneNumberCountryPrefix;
                    registeredUser.PhoneNumber = model.PhoneNumber;
                    registeredUser.PhoneNumberConfirmed = false;
                    registeredUser.PasswordHash = _hashAlgo.CalculateHashValueWithInput(model.PasswordHash);
                    registeredUser.TwoFactorEnabled = false;
                    registeredUser.CreatedAt = DateTime.UtcNow;
                    registeredUser.NumberOfFailLoginAttempts = 0;
                    registeredUser.IsActive = false;
                    registeredUser.IsDeleted = false;
                    registeredUser.Adresa = adresa;
                    registeredUser.UserImage = ConvertToBytes(model.UserImage);
                    registeredUser.Roles = new List<IdentityRole>
                {
                   uow.IdentityRoles.Get().Single(r => r.Id.Equals((int)RoleTypes.Specialist))
                }; ;
                    uow.IdentityUsers.Insert(registeredUser);
                    var specialist = new Specialist();
                    specialist.SpecialistId = registeredUser.Id;
                    specialist.Specialitate = model.Specialitate;
                    specialist.Descriere = model.Descriere;
                    specialist.durataProgramare = model.durataProgramare;
                    uow.Specialisti.Insert(specialist);
                    uow.SaveChanges();

                }
            });
        }


        public async Task<IdentityUser> EditDoctorInfo(EditDoctorDto editDoctor)
        {
            return ExecuteInTransaction(uow =>
            {
                var user = uow.IdentityUsers.Get()
                                          .Include(iu => iu.Adresa)
                                          .Include(iu => iu.Specialist)
                                          .SingleOrDefault(ui => ui.Email.Equals(editDoctor.Email));
                user.FirstName = editDoctor.FirstName;
                user.LastName = editDoctor.LastName;
                user.PhoneNumberCountryPrefix = editDoctor.PhoneNumberCountryPrefix;
                user.PhoneNumber = editDoctor.PhoneNumber;
                var adresa = user.Adresa;
                adresa.Judet = editDoctor.Judet;
                adresa.Tara = editDoctor.Tara;
                adresa.StradaNumar = editDoctor.StradaNumar;
                adresa.Oras = editDoctor.Oras;
                adresa.Sector = editDoctor.Sector;
                adresa.BlocScaraApartament = editDoctor.BlocScaraApartament;
                adresa.CodPostal = editDoctor.CodPostal;
                var specialist = user.Specialist;
                specialist.Descriere = editDoctor.Descriere;
                specialist.durataProgramare = editDoctor.durataProgramare;
                uow.IdentityUsers.Update(user);
                uow.SaveChanges();
                return user;

            });
        }
        public IdentityUser DeleteDoctor(string email)
        {
            return ExecuteInTransaction(uow =>
            {
                var user = uow.IdentityUsers.Get()
                            .Include(iu => iu.Adresa)
                            .Include(iu => iu.Roles)
                            .Include(iu => iu.Specialist)
                            .Where(iu => iu.Email.Equals(email))
                            .Single();
                var appointments = uow.Programari.Get().Where(p => p.SpecialistId.Equals(user.Id)).ToList();
                user.IsDeleted = true;
                foreach(var appointment in appointments)
                {
                    uow.Programari.Delete(appointment);
                }
                uow.Specialisti.Delete(user.Specialist);
                uow.IdentityUsers.Delete(user);
                uow.Adrese.Delete(user.Adresa);
                uow.SaveChanges();
                return user;
            });
        }
        public async Task<EditDoctorDto> GetDoctorByEmail (string email)
        {
            return ExecuteInTransaction(uow =>
            {
                var user = uow.IdentityUsers.Get()
                                               .Include(u => u.Adresa)
                                               .Include(u => u.Specialist)
                                               .Where(u => u.Email.Equals(email))
                                               .Select(u => new EditDoctorDto
                                               {
                                                   SpecialistId = u.Specialist.SpecialistId,
                                                   LastName = u.LastName,
                                                   FirstName = u.FirstName,
                                                   Email = u.Email,
                                                   PhoneNumber = u.PhoneNumber,
                                                   Specialitate = u.Specialist.Specialitate,
                                                   Descriere = u.Specialist.Descriere,
                                                   durataProgramare = u.Specialist.durataProgramare,
                                                   Tara = u.Adresa.Tara,
                                                   Judet = u.Adresa.Judet,
                                                   Oras = u.Adresa.Oras,
                                                   StradaNumar = u.Adresa.StradaNumar,
                                                   BlocScaraApartament = u.Adresa.BlocScaraApartament,
                                                   Sector = u.Adresa.Sector,
                                                   CodPostal = u.Adresa.CodPostal,
                                                   DoctorImage =  u.UserImage

                                               }).SingleOrDefault();
                return user;
            });

        }
        public int GetDoctorProgramById(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var user = uow.Specialisti.Get()
                                               .Where(u => u.SpecialistId.Equals(id))
                                               .Select(
                                                 u => u.durataProgramare).SingleOrDefault();
                return user;
            });

        }
        public async Task<EditDoctorDto> GetDoctorById(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                var user = uow.IdentityUsers.Get()
                                               .Include(u => u.Adresa)
                                               .Include(u => u.Specialist)
                                               .Where(u => u.Id.Equals(id))
                                               .Select(u => new EditDoctorDto
                                               {
                                                   SpecialistId = u.Specialist.SpecialistId,
                                                   LastName = u.LastName,
                                                   FirstName = u.FirstName,
                                                   Email = u.Email,
                                                   PhoneNumber = u.PhoneNumber,
                                                   Specialitate = u.Specialist.Specialitate,
                                                   Descriere = u.Specialist.Descriere,
                                                   durataProgramare = u.Specialist.durataProgramare,
                                                   Tara = u.Adresa.Tara,
                                                   Judet = u.Adresa.Judet,
                                                   Oras = u.Adresa.Oras,
                                                   StradaNumar = u.Adresa.StradaNumar,
                                                   BlocScaraApartament = u.Adresa.BlocScaraApartament,
                                                   Sector = u.Adresa.Sector,
                                                   CodPostal = u.Adresa.CodPostal,
                                                   DoctorImage = u.UserImage

                                               }).SingleOrDefault();
                return user;
            });

        }
        public void AddWorkProgram(DoctorWorkProgramVM model)
        {
            ExecuteInTransaction(uow =>
            {
                var workDay = uow.Valabilitati.Get()
                                                .Include(x => x.WorkDay)
                                               .SingleOrDefault(v => v.WorkDay.Name.Equals(model.WorkDay) && v.SpecialistId.Equals(CurrentUser.Id));
                if (workDay != null)
                {
                    workDay.Start = model.Start;
                    workDay.End = model.End;
                    workDay.Notes = model.Notes;
                    uow.Valabilitati.Update(workDay);
                    uow.SaveChanges();
                    // string message = "Exsita deja o inregistrare pentru ziua respectiva" ;
                    //throw new AlreadyExistsInDB(nameof(Valabilitate), message);
                }
                else
                {
                    var specialist = uow.Specialisti.Get()
                                                     .Single(s => s.SpecialistId.Equals(CurrentUser.Id));
                    var newDailyProgram = new Valabilitate
                    {
                        Id = new Guid(),
                        SpecialistId = specialist.SpecialistId,
                        WorkDayId = uow.WorkDays.Get().Where(w => w.Name.Equals(model.WorkDay)).Select(w => w.Id).Single(),
                        Start = model.Start,
                        End = model.End,
                        Notes = model.Notes
                    };
                    UnitOfWork.Valabilitati.Insert(newDailyProgram);
                    uow.SaveChanges();
                }
            });
        }

        public List<DoctorWorkProgramVM> GetProgram()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Valabilitati.Get()
                                        .Include(v => v.WorkDay)
                                        .Where(v => v.SpecialistId.Equals(CurrentUser.Id))
                                        .Select(v => new DoctorWorkProgramVM
                                        {
                                            Id = v.Id,
                                            SpecialistId = v.SpecialistId,
                                            WorkDayId = v.WorkDayId,
                                            WorkDay = v.WorkDay.Name,
                                            Start = v.Start,
                                            End = v.End,
                                            Notes = v.Notes
                                        }).ToList();
            });
        }
        public List<DoctorWorkProgramVM> GetDoctorProgram(Guid id)
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.Valabilitati.Get()
                                        .Include(v => v.WorkDay)
                                        .Where(v => v.SpecialistId.Equals(id))
                                        .Select(v => new DoctorWorkProgramVM
                                        {
                                            Id = v.Id,
                                            SpecialistId = v.SpecialistId,
                                            WorkDayId = v.WorkDayId,
                                            WorkDay = v.WorkDay.Name,
                                            Start = v.Start,
                                            End = v.End,
                                            Notes = v.Notes
                                        }).ToList();
            });
        }
        public List<WorkDaysVM> GetWorkDays()
        {
            return ExecuteInTransaction(uow =>
            {
                return uow.WorkDays.Get()
                                        .Select(v => new WorkDaysVM
                                        {
                                            Id = v.Id,
                                            Name = v.Name,
                                        }).ToList();
            });
        }

    }
}
