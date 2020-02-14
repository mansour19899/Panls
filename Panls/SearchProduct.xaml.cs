using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Panls
{
    /// <summary>
    /// Interaction logic for SearchProduct.xaml
    /// </summary>
    public partial class SearchProduct : Window
    {
        public SearchProduct()
        {
            InitializeComponent();

            ConnectToDatabase connect = new ConnectToDatabase();
            var t = connect.GiveMeProduct("10006");
            lblProductTittle.Content ="Product Tittel :  "+ t.ProductTittle;
            lblCompany.Content ="Company :  "+t.Company.CompanyName;
            lblCategory.Content ="Category : "+ t.ProductType.CategoriesSubCategory.Category.Name;
            lblSubCat.Content ="Sub Category :  "+ t.ProductType.CategoriesSubCategory.SubCategory.Name;
            lblProductType.Content ="Product Type : "+ t.ProductType.Name;
            lblBrand.Content = "Brand : "+ t.Brand.Name;
            lblCountryOfOrgin.Content =" Country Of Orgin : "+ t.Country.Name;
            lblColor.Content = "Color : "+t.Color.Name.Trim();
            lblMeterial.Content = "Meterial : " + t.Material.MaterialName;
            lblSku.Content ="SKU : "+ t.Sku;
            lblStyleNumber.Content = "Style Number :" + t.StyleNumber;
            lblUPC.Content = "UPC : " + t.Barcode.BarcodeNumber;
            lblPrice.Content = "Price : " + t.Price.ToString();
            lblSize.Content= "Size : " + t.Width.ToString() + "*" + t.length.ToString();
            lblGsm.Content = "GSM : " + t.Gsm.ToString();

        }
    }
}
