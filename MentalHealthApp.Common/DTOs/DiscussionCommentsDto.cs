using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.Common.DTOs
{
    public class DiscussionCommentsDto
    {
        public Guid Id { get; set; }
        public Guid DiscutieId { get; set; }
        public string DiscussionTitle { get; set; }
        public string Username { get; set; }
        public string Continut { get; set; } = null!;
        public DateTime DataComentariu { get; set; }
    }
}
