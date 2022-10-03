using MentalHealthApp.Common;
using MentalHealthApp.Entities.Entities;
using System;
using System.Collections.Generic;

namespace MentalHealthApp.Entities
{
    public partial class Utilizator : IEntity
    {
        public Utilizator()
        {
            CameraConferinta = new HashSet<CameraConferintum>();
            IstoricDiagnosticUtilizators = new HashSet<IstoricDiagnosticUtilizator>();
            Programares = new HashSet<Programare>();
            MedicalReports = new HashSet<MedicalReport>();
            Simptomes = new HashSet<Simptome>();
        }

        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Categorie { get; set; } = null!;
        public string? Asigurat { get; set; }

        public virtual IdentityUser User { get; set; } = null!;
        public virtual ICollection<CameraConferintum> CameraConferinta { get; set; }
        public virtual ICollection<IstoricDiagnosticUtilizator> IstoricDiagnosticUtilizators { get; set; }
        public virtual ICollection<Programare> Programares { get; set; }
        public virtual ICollection<MedicalReport> MedicalReports { get; set; }
        public virtual ICollection<Simptome> Simptomes { get; set; }
    }
}
