using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Forum.ViewModels
{
    public class CreateDiscussionCommentsVM
    {
        public Guid Id { get; set; }
        public Guid DiscutieId { get; set; }
        public Guid UserId { get; set; }
        public string DiscussionTitle { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Continut { get; set; } = null!;
        public int? CommentReaction { get; set; }
        public DateTime DataComentariu { get; set; }
        public byte[] UserImage { get; set; } = null!;
    }
}
