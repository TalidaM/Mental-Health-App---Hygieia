using MentalHealthApp.Common;
using System;
using System.Collections.Generic;

namespace MentalHealthApp.Entities
{
    public partial class Discutie : IEntity
    {
        public Discutie()
        {
            ComentariiDiscuties = new HashSet<ComentariiDiscutie>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Titlu { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public string Continut { get; set; } = null!;
        public DateTime DataCreare { get; set; }
        public int CommentReaction { get; set; }
        public virtual IdentityUser User { get; set; } = null!;
        public virtual ICollection<ComentariiDiscutie> ComentariiDiscuties { get; set; }
    }
}
