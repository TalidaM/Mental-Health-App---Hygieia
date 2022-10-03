using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Forum.ViewModels
{
    public class DiscussionVM
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public Guid UserId { get; set; }
        public string Titlu { get; set; } = null!;
        public string Continut { get; set; } = null!;
        public DateTime DataCreare { get; set; }
        public int CommentReaction { get; set; }
        public byte[] UserImage { get; set; } = null!;
        public List<CreateDiscussionCommentsVM> createDiscussionCommentsVMs { get; set; } = null!;
    }
}
