using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.Common.DTOs
{
    public class AcceptedAppointmentsDto
    {
        public int Id { get; set; }
        public string DataProgramare { get; set; } = null!;
        public Guid UtilizatorId { get; set; }
        public string Pacient { get; set; } = null!;
        public string? Specialist { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Categorie { get; set; } = null!;
        public string? Asigurat { get; set; }
        public string? StareProgramare { get; set; }
        public byte[]? UserImage { get; set; }
    }
}
