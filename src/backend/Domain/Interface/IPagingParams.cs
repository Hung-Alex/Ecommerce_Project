using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IPagingParams
    {
        int PageSize { get; }
        int PageNumber { get; }
        string SortColoumn { get; }
        string SortBy { get; }
    }
}
