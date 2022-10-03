using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Appointments.ViewModels
{
    public class AppointmentsDoctorProfileVM
    {
        public int Id { get; set; }
       // public DateTime DataProgramare { get; set; }
        public string DataProgramare { get; set; } = null!;
        public Guid UtilizatorId { get; set; }
        public string Pacient { get; set; } = null!;
        public string? StareProgramare { get; set; }
        public List<AppointmentsDoctorProfileVM> AppointmentDoctorProfileVMs = null!;
    }
}
