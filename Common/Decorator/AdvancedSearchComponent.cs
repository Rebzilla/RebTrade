using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Decorator
{
    public abstract class AdvancedSearchComponent<T>
    {
        public abstract List<T> Search(List<T> data);
    }
}
