using MentalHealthApp.Common;
using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

namespace MentalHealthApp.Entities
{
    public partial class IdentityUser : IEntity
    {
        public IdentityUser()
        {
            ComentariiDiscuties = new HashSet<ComentariiDiscutie>();
            Discuties = new HashSet<Discutie>();
            IdentityUserTokenConfirmations = new HashSet<IdentityUserTokenConfirmation>();
            IdentityUserTokens = new HashSet<IdentityUserToken>();
            Roles = new HashSet<IdentityRole>();
        }

        public Guid Id { get; set; }
        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }
        public string PhoneNumberCountryPrefix { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public bool PhoneNumberConfirmed { get; set; }
        public string PasswordHash { get; set; } = null!;
        public bool TwoFactorEnabled { get; set; }
        public DateTime CreatedAt { get; set; }
        public int NumberOfFailLoginAttempts { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int AdresaId { get; set; }
        public byte[] UserImage { get; set; } = null!;



        public virtual Adresa Adresa { get; set; } = null!;
        public virtual Specialist Specialist { get; set; } = null!;
        public virtual Utilizator Utilizator { get; set; } = null!;
        public virtual ICollection<ComentariiDiscutie> ComentariiDiscuties { get; set; }
        public virtual ICollection<Discutie> Discuties { get; set; }
        public virtual ICollection<IdentityUserTokenConfirmation> IdentityUserTokenConfirmations { get; set; }
        public virtual ICollection<IdentityUserToken> IdentityUserTokens { get; set; }

        public virtual ICollection<IdentityRole> Roles { get; set; }
    }
}
