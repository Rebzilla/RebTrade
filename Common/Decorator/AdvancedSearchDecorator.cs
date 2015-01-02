using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Decorator
{
    public abstract class AdvancedSearchDecorator<T> : AdvancedSearchComponent<T>
    {
        public AdvancedSearchComponent<T> AdvancedSearchComponent { get; set; }

        public AdvancedSearchDecorator()
        { }

        public AdvancedSearchDecorator(AdvancedSearchComponent<T> component)
        {
            AdvancedSearchComponent = component;
        }
    }
}
