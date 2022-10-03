using MentalHealthApp.Common;
using System;
using System.Collections.Generic;

namespace MentalHealthApp.Entities
{
    public partial class Programare : IEntity
    {
        public int Id { get; set; }
        public Guid SpecialistId { get; set; }
        public Guid UtilizatorId { get; set; }
        public string DataProgramare { get; set; } = null!;
       // public string OraProgramare { get; set; } = null!;
        public string TipProgramare { get; set; } = null!;
        public string? StareProgramare { get; set; }

        public virtual Specialist Specialist { get; set; } = null!;
        public virtual Utilizator Utilizator { get; set; } = null!;
    }
}
