using MentalHealthApp.BusinessLogic.Implementation.Account.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Admin.ViewModels
{
    public class AppointmentsAdminModel
    {
        public int Id { get; set; }
        public Guid SpecialistId { get; set; }
        public string DoctorName { get; set; } = null!;
        public Guid UtilizatorId { get; set; }
        public string UserName { get; set; } = null!;
        public string DataProgramare { get; set; } = null!;
        //public string OraProgramare { get; set; } = null!;
        public string TipProgramare { get; set; } = null!;
        public string? StareProgramare { get; set; }


    }
}
