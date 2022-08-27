using System.Collections.Generic;

namespace SweetAndSavory.Models
{
    public class Flavor
    {
        public Flavor()
        {
            this.JoinEntities = new HashSet<FlavorTreat>();
        }

        public int FlavorId { get; set; }
        public string FlavorName { get; set; }
        public string UserId { get; set; } // Foreign Key

        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<FlavorTreat> JoinEntities { get; set; }
    }
}
