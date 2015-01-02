using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Decorator;


namespace DataAccessLayer
{
    public class SearchByProductCategory : AdvancedSearchDecorator<Product>
    {
        public SearchByProductCategory()
        {
        }

        public SearchByProductCategory(AdvancedSearchComponent<Product> component)
            : base(component)
        {
        }

        public string CategoryName { get; set; }

        public override List<Product> Search(List<Product> data)
        {
            List<Product> result = data.Where(x => x.ProductCategory.CategoryName.ToLower().Contains(CategoryName.ToLower())).ToList();
 
            if (AdvancedSearchComponent != null)
            {
                result = AdvancedSearchComponent.Search(result);
            }

            return result;
        }
    }
}
