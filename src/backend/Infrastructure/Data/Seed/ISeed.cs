using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Seed
{
    public interface ISeed
    {
        Task InitData();
    }
}
