using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Decorator;

namespace DataAccessLayer
{
    public class SearchByProductDescription: AdvancedSearchDecorator<Product>
    {
        public SearchByProductDescription()
        {
        }

        public SearchByProductDescription(AdvancedSearchComponent<Product> component)
            : base(component)
        {
        }

        public string Description { get; set; }

        public override List<Product> Search(List<Product> data)
        {
            List<Product> result = data.Where(x => x.Description.ToLower().Contains(Description.ToLower())).ToList();

            if (AdvancedSearchComponent != null)
            {
                result = AdvancedSearchComponent.Search(result);
            }

            return result;
        }
    }
}
