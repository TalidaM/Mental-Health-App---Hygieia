using MentalHealthApp.Common;
using System;
using System.Collections.Generic;

namespace MentalHealthApp.Entities
{
    public partial class Simptome : IEntity
    {
        public Simptome()
        {
            Diagnostics = new HashSet<Diagnostic>();
            Utilizators = new HashSet<Utilizator>();
        }

        public Guid Id { get; set; }
        public string Denumire { get; set; } = null!;
        public string? Descriere { get; set; }

        public virtual ICollection<Diagnostic> Diagnostics { get; set; }
        public virtual ICollection<Utilizator> Utilizators { get; set; }
    }
}
