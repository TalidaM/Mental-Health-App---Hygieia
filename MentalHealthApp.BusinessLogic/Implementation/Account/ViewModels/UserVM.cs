using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels
{
    public class UserVM
    {
        [Required]
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Categorie { get; set; } = null!;
        public string? Asigurat { get; set; }
        public string Tara { get; set; } = null!;
        public string Judet { get; set; } = null!;
        public string Oras { get; set; } = null!;
        public string? StradaNumar { get; set; }
        public string? BlocScaraApartament { get; set; }
        public string? Sector { get; set; }
        public string? CodPostal { get; set; }
        public byte[]? UserImage { get; set; }

    }
}
