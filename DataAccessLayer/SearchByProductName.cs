using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Decorator;

namespace DataAccessLayer
{
    public class SearchByProductName : AdvancedSearchDecorator<Product>
    {
        public SearchByProductName()
        {
        }

        public SearchByProductName(AdvancedSearchComponent<Product> component)
            : base(component)
        {
        }

        public string ProductName { get; set; }

        public override List<Product> Search(List<Product> data)
        {
            List<Product> result = data.Where(x => x.ProductName.ToLower().Contains(ProductName.ToLower())).ToList();

            if (AdvancedSearchComponent != null)
            {
                result = AdvancedSearchComponent.Search(result);
            }

            return result;
        }
    }
}
