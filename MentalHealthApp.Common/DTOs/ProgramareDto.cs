using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.Common.DTOs
{
    public class ProgramareDto
    {

        public int Id { get; set; }
        public string DataProgramare { get; set; } = null!;
        public Guid SpecialistId { get; set; }
        public Guid UtilizatorId { get; set; }
        public string Doctor { get; set; } = null!;
        public string TipProgramare { get; set; } = null!;
        public string? StareProgramare { get; set; }
    }
}
