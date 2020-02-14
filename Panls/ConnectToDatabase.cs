using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panls
{
    class ConnectToDatabase
    {
        VanmeEntities db;

        public ConnectToDatabase()
        {
            db = new VanmeEntities();

        }
        public OwnProduct GiveMeProduct(string str)
        {
            var x = db.OwnProducts.Where(p => p.Sku.CompareTo(str) == 0).FirstOrDefault();
            int yy = int.Parse(str);
            var y = db.OwnProducts.Where(p => p.Id == yy).FirstOrDefault();

            return y;
        }

        public List<OwnProduct> GiveListProducts()
        {
            var x = db.OwnProducts.ToList();
            return x;
        }
        public List<Company> CompaniesList()
        {
            var x = db.Companies.ToList();
            return x;
        }
        public List<Category> CategoriesList()
        {
            var x = db.Categories.ToList();
            foreach (var item in x)
            {
                item.Name = item.Name.Trim();
            }
            return x;
        }
        public List<SubCategory> SubCategoriesList()
        {
            var x = db.SubCategories.ToList();
            foreach (var item in x)
            {
                item.Name = item.Name.Trim();
            }
            return x;
        }
        public List<ProductType> ProductTypeList()
        {
            var x = db.ProductTypes.ToList();
            foreach (var item in x)
            {
                item.Name = item.Name.Trim();
            }
            return x;
        }
    }
}
