using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.BusinessLogic.Implementation.Forum.ViewModels
{
    public class CreateDiscussionVM
    {
        //public Guid Id { get; set; }
        public string Titlu { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public string Continut { get; set; } = null!;
        public List<DiscussionVM> DiscussionVMs { get; set; } = null!;
        public List<string> Conditions { get; set; } = null!;
        public int? Page {get; set;}
    }
}
