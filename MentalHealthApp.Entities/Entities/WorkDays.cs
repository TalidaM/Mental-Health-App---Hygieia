using MentalHealthApp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentalHealthApp.Entities.Entities
{
    public class WorkDay : IEntity
    {
        public WorkDay()
        {
            Valabilitati = new HashSet<Valabilitate>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Valabilitate> Valabilitati { get; set; }

    }
}
