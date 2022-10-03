using MentalHealthApp.Common;
using System;
using System.Collections.Generic;

namespace MentalHealthApp.Entities
{
    public partial class Adresa :IEntity
    {
        public Adresa()
        {
            IdentityUsers = new HashSet<IdentityUser>();
        }

        public int Id { get; set; }
        public string Tara { get; set; } = null!;
        public string Judet { get; set; } = null!;
        public string Oras { get; set; } = null!;
        public string? StradaNumar { get; set; }
        public string? BlocScaraApartament { get; set; }
        public string? Sector { get; set; }
        public string? CodPostal { get; set; }

        public virtual ICollection<IdentityUser> IdentityUsers { get; set; }
    }
}
