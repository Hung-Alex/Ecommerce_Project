using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BannerImage : BaseEntity, IDatedModification
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt {  get; set; }
    }
}
