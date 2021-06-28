using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_Core.Application.GridViewModels
{
    public class DataGridViewModel<T>
    {
        public IEnumerable<T> Records { get; set; }
        public int TotalCount { get; set; }
    }
}
