using MentalHealthApp.BusinessLogic.Base;
using MentalHealthApp.BusinessLogic.Implementation.Account.Validations;
using MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels;
using MentalHealthApp.Common.DTOs;
using MentalHealthApp.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Account
{
    public class UserProfileService :BaseService
    {
        private readonly UserAccountService _user;

        public UserProfileService(ServiceDependencies dependencies,
                                UserAccountService userAccount
                                ) 
                                : base(dependencies)
        {
            _user = userAccount;
        }

        public  IdentityUser EditUserInfo(EditUserDto editUser)
        {
          return ExecuteInTransaction(uow =>
                {
                    var user = uow.IdentityUsers.Get()
                                           .Include(iu => iu.Adresa)
                                           .Include(iu => iu.Utilizator)
                                           .SingleOrDefault(ui => ui.Email.Equals(editUser.Email));
                    user.FirstName = editUser.FirstName;
                    user.LastName = editUser.LastName;
                    user.BirthDate = editUser.BirthDate;
                   // user.PhoneNumberCountryPrefix = editUser.PhoneNumberCountryPrefix;
                    var adresa = user.Adresa;
                    adresa.Judet = editUser.Judet;
                    adresa.Tara = editUser.Tara;
                    adresa.StradaNumar = editUser.StradaNumar;
                    adresa.Oras = editUser.Oras;
                    adresa.BlocScaraApartament = editUser.BlocScaraApartament;
                    adresa.CodPostal = editUser.CodPostal;
                    adresa.Sector = editUser.Sector;
                    var utilizator = user.Utilizator;
                    utilizator.Username = editUser.Username;
                    utilizator.Asigurat = editUser.Asigurat;
                    utilizator.Categorie = editUser.Categorie;

                    uow.IdentityUsers.Update(user);
                    uow.SaveChanges();
                    return user;

                });
            }

        public IFormFile ChangeProfilePhoto(IFormFile userImage)
        {
            return ExecuteInTransaction(uow =>
            {
                var user = uow.IdentityUsers.Get()
                                       .SingleOrDefault(ui => ui.Id.Equals(CurrentUser.Id));
                user.UserImage = _user.ConvertToBytes(userImage);
                uow.IdentityUsers.Update(user);
                uow.SaveChanges();
                return userImage;

            });
        }

        public async Task<UserVM> GetUserByEmail(string email)
        {
            return ExecuteInTransaction(uow =>
            {
                var user = uow.IdentityUsers.Get()
                                     .Include(u => u.Adresa)
                                     .Include(u => u.Utilizator)
                                     .Where(u => u.Email == email)
                                     .Select(u => new UserVM
                                     {
                                         LastName = u.LastName,
                                         FirstName = u.FirstName,
                                         BirthDate = u.BirthDate,
                                         Email = u.Email,
                                         PhoneNumber = u.PhoneNumber,
                                         Username = u.Utilizator.Username,
                                         Categorie = u.Utilizator.Categorie,
                                         Asigurat = u.Utilizator.Asigurat,
                                         Tara = u.Adresa.Tara,
                                         Judet = u.Adresa.Judet,
                                         Oras = u.Adresa.Oras,
                                         StradaNumar = u.Adresa.StradaNumar,
                                         BlocScaraApartament = u.Adresa.BlocScaraApartament,
                                         Sector = u.Adresa.Sector,
                                         CodPostal = u.Adresa.CodPostal,
                                         UserImage = u.UserImage
                                         

                                         
                                     })
                                    .SingleOrDefault();
                return user;
            });
            
        }
    }
}
