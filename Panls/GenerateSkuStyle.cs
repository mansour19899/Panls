using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panls
{
     class GenerateSkuStyle
    {
        public GenerateSkuStyle()
        {

        }
        public Dictionary<string, string> Generate(int Category, int SubCategory, int ProductType, int color, string LastSkuNumber, string LastStyleNumber)
        {
            int x = Category;
            var codes = new Dictionary<string, string>();

            var CategoryStyleCode = AddProduct.ListCategories.SingleOrDefault(p => p.Id == Category).StyleNum_code;
            var CategorySkuCode = AddProduct.ListCategories.SingleOrDefault(p => p.Id == Category).Sku_code;
            var SubCategoryCode = AddProduct.ListSubCategories.SingleOrDefault(p => p.Id == SubCategory).Code;
            var ProductTypeCode = AddProduct.ListProductTypes.SingleOrDefault(p => p.Id == ProductType).Code;
            var colorCode = AddProduct.ListColors.SingleOrDefault(p => p.Id == color).Code;


            var yt = LastSkuNumber.ToCharArray().Skip(10).ToArray();
            var ytt = yt.SkipWhile(p => p.CompareTo('0') == 0).ToArray();
            var yttt = new string(ytt);
            var newSkuNumber = Convert.ToDecimal(yttt) + 1;

            string Sku = CategorySkuCode + SubCategoryCode + ProductTypeCode + colorCode + newSkuNumber.ToString().NumtoStr(6);

            var qt = LastStyleNumber.ToCharArray().Skip(6).ToArray();
            var qtt = qt.SkipWhile(p => p.CompareTo('0') == 0).ToArray();
            var qttt = new string(qtt);
            var newStyleNumber = Convert.ToDecimal(qttt) + 1;

            string StyleNumber = CategoryStyleCode + "." + SubCategoryCode + "." + newStyleNumber.ToString().NumtoStr(6);

            codes.Add("Sku", Sku);
            codes.Add("StyleNumber", StyleNumber);
            return codes;
        }

    }

}
