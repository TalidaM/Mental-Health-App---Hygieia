using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.Common.DTOs
{
    public class EditDoctorDto
    {
        public Guid SpecialistId { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumberCountryPrefix { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Specialitate { get; set; } = null!;
        public string Descriere { get; set; } = null!;
        public int durataProgramare { get; set; }
        public string Tara { get; set; } = null!;
        public string Judet { get; set; } = null!;
        public string Oras { get; set; } = null!;
        public string? StradaNumar { get; set; }
        public string? BlocScaraApartament { get; set; }
        public string? Sector { get; set; }
        public string? CodPostal { get; set; }
        public byte[] DoctorImage { get; set; } = null!;
    }
}
