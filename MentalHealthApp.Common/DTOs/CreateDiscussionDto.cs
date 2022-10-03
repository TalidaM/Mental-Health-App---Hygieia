using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.Common.DTOs
{
    public class CreateDiscussionDto
    {
        public string Titlu { get; set; } = null!;
        public string Continut { get; set; } = null!;
    }
}
