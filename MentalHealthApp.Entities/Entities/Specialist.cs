using MentalHealthApp.Common;
using MentalHealthApp.Entities.Entities;
using System;
using System.Collections.Generic;

namespace MentalHealthApp.Entities
{
    public partial class Specialist :IEntity
    {
        public Specialist()
        {
            CameraConferinta = new HashSet<CameraConferintum>();
            Programares = new HashSet<Programare>();
            MedicalReports = new HashSet<MedicalReport>();
        }

        public Guid SpecialistId { get; set; }
        public string Specialitate { get; set; } = null!;
        public string Descriere { get; set; } = null!;
        public int durataProgramare { get; set; }

        public virtual IdentityUser SpecialistNavigation { get; set; } = null!;
        public virtual ICollection<CameraConferintum> CameraConferinta { get; set; }
        public virtual ICollection<Programare> Programares { get; set; }
        public virtual ICollection<MedicalReport> MedicalReports { get; set; }
        public virtual ICollection<Valabilitate> Valabilitati { get; set; }
    }
}
