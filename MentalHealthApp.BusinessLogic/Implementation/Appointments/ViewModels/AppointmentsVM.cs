using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Appointments.ViewModels
{
    public class AppointmentsVM
    {
        public int Id { get; set; }
        public string DataProgramare { get; set; } = null!;
        //public string OraProgramare { get; set; } = null!;
        public string Doctor { get; set; } = null!;
        public string TipProgramare { get; set; } = null!;
        public string? StareProgramare { get; set; }
    }
}
