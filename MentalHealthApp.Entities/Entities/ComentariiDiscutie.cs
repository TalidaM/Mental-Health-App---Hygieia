using MentalHealthApp.Common;
using System;
using System.Collections.Generic;

namespace MentalHealthApp.Entities
{
    public partial class ComentariiDiscutie : IEntity
    {
        public Guid Id { get; set; }
        public Guid DiscutieId { get; set; }
        public Guid UserId { get; set; }
        public string Continut { get; set; } = null!;
        public DateTime DataComentariu { get; set; }
        public int CommentReaction { get; set; }
        public virtual Discutie Discutie { get; set; } = null!;
        public virtual IdentityUser User { get; set; } = null!;
    }
}
