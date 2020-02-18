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
using System.Xml;

namespace Panls
{
    /// <summary>
    /// Interaction logic for SearchProduct.xaml
    /// </summary>
    public partial class SearchProduct : Window
    {
        ConnectToDatabase connect;
        List<OwnProduct> TempList;
        List<OwnProduct> SelectedList;
        Boolean SwitchSelectedlist = true;
        public SearchProduct()
        {
            InitializeComponent();

             connect = new ConnectToDatabase();
            SelectedList = new List<OwnProduct>();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCategory.ItemsSource = connect.CategoriesList();
            cmbCompany.ItemsSource = connect.CompaniesList();
            cmbProductType.ItemsSource = connect.ProductTypeList();
            cmbSubCategory.ItemsSource = connect.SubCategoriesList();

                        
            cmbType.ItemsSource = new[]{
                new { Id = 1, Name = "---------" },
                new { Id = 2, Name = "Style Number" },
                new { Id = 3, Name = "SKU" },
                new { Id = 4, Name = "Tittle" }
            }.ToList();

            cmbType.SelectedIndex = 0;
            cmbCategory.SelectedIndex = 0;
            cmbCompany.SelectedIndex = 0;
            cmbProductType.SelectedIndex = 0;
            cmbSubCategory.SelectedIndex = 0;
        }
        void SetInformation(string str)
        {
            var t = connect.GiveMeProduct(str);
            lblProductTittle.Content = "Product Tittel :  " + t.ProductTittle;
            lblCompany.Content = "Company :  " + t.Company.CompanyName;
            lblCategory.Content = "Category : " + t.ProductType.CategoriesSubCategory.Category.Name;
            lblSubCat.Content = "Sub Category :  " + t.ProductType.CategoriesSubCategory.SubCategory.Name;
            lblProductType.Content = "Product Type : " + t.ProductType.Name;
            lblBrand.Content = "Brand : " + t.Brand.Name;
            lblCountryOfOrgin.Content = "Country Of Orgin : " + t.Country.Name;
            lblColor.Content = "Color : " + t.Color.Name.Trim();
            lblMeterial.Content = "Meterial : " + t.Material.MaterialName;
            lblSku.Content = "SKU : " + t.Sku;
            lblStyleNumber.Content = "Style Number :" + t.StyleNumber;
            if (t.Barcode_Id_fk != null)
                lblUPC.Content = "UPC : " + t.Barcode.BarcodeNumber;
            lblPrice.Content = "Price : " + t.Price.ToString();
            lblSize.Content = "Size : " + t.Width.ToString() + " * " + t.length.ToString();
            lblGsm.Content = "GSM : " + t.Gsm.ToString();
            lblHeader.Content = t.StyleNumber;
            GrSearch.Visibility = Visibility.Hidden;
        }
        void SetInformation(OwnProduct t)
        {            
            lblProductTittle.Content = "Product Tittel :  " + t.ProductTittle;
            lblCompany.Content = "Company :  " + t.Company.CompanyName;
            lblCategory.Content = "Category : " + t.ProductType.CategoriesSubCategory.Category.Name;
            lblSubCat.Content = "Sub Category :  " + t.ProductType.CategoriesSubCategory.SubCategory.Name;
            lblProductType.Content = "Product Type : " + t.ProductType.Name;
            lblBrand.Content = "Brand : " + t.Brand.Name;
            lblCountryOfOrgin.Content = "Country Of Orgin : " + t.Country.Name;
            lblColor.Content = "Color : " + t.Color.Name.Trim();
            lblMeterial.Content = "Meterial : " + t.Material.MaterialName;
            lblSku.Content = "SKU : " + t.Sku;
            lblStyleNumber.Content = "Style Number :" + t.StyleNumber;
            if(t.Barcode_Id_fk!= null)
              lblUPC.Content = "UPC : " + t.Barcode.BarcodeNumber;
            lblPrice.Content = "Price : " + t.Price.ToString();
            lblSize.Content = "Size : " + t.Width.ToString() + " * " + t.length.ToString();
            lblGsm.Content = "GSM : " + t.Gsm.ToString();
            lblHeader.Content = t.StyleNumber;
            GrSearch.Visibility = Visibility.Hidden;
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.Enter)
            {
                string x = cmbType.SelectedValue.ToString();
                //if (x.CompareTo("1")==0)
                //lvProducts.ItemsSource = connect.GiveListProducts();

                switch (x)
                {
                    case "1":
                        lvProducts.ItemsSource = connect.GiveListProducts();
                        break;
                    case "2":
                        lvProducts.ItemsSource = connect.GiveListProductsWithSytyle(txtSearch.Text);
                        break;
                    case "3":
                        lvProducts.ItemsSource = connect.GiveListProductsWithSKU(txtSearch.Text);
                        break;
                    case "4":
                        MessageBox.Show("salam");
                        break;
                    default:
                        MessageBox.Show("Hi");
                        break;
                }

                TempList = lvProducts.ItemsSource as List<OwnProduct>;
                lblCountResult.Content = "Results :  " + TempList.Count();
            }
        }

        private void lvProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            OwnProduct item = (OwnProduct)lvProducts.ItemContainerGenerator.ItemFromContainer(dep);

            SetInformation(item);


            int x = 0;

        }

        private void lblHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GrSearch.Visibility = Visibility.Visible;
        }

        void SetFilter()
        {
            var t = TempList;
            if (SwitchSelectedlist==false)
               t = SelectedList;
            lvProducts.ItemsSource = t;
            if(cmbCategory.SelectedIndex > 0)
            {
                var x = Convert.ToInt16(cmbCategory.SelectedValue);
                t= t.Where(p => p.ProductType.CategoriesSubCategory.Category_Id_fk == x).ToList(); 
                lvProducts.ItemsSource = t;
            }
            if (cmbSubCategory.SelectedIndex > 0)
            {
                var xx = Convert.ToInt16(cmbSubCategory.SelectedValue);
                lvProducts.ItemsSource = t.Where(p => p.ProductType.CategoriesSubCategory.SubCategory_Id_fk == xx);
            }
            if (cmbProductType.SelectedIndex > 0)
            {
                var xxx = Convert.ToInt16(cmbProductType.SelectedValue);
                t= t.Where(p => p.ProductType.Id == xxx).ToList();
                lvProducts.ItemsSource = t;
            }
            if (cmbCompany.SelectedIndex > 0)
            {
                var xxxx = Convert.ToInt16(cmbCompany.SelectedValue);
                t = t.Where(p => p.Company_Id_fk == xxxx).ToList();
                lvProducts.ItemsSource = t;
            }
            if(t!=null)
            lblCountResult.Content ="Results :  "+ t.Count();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();            
        }

        private void cmbSubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void cmbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void cmbCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetFilter();
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            cmbCategory.SelectedIndex = 0;
        }

        private void Label_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            cmbSubCategory.SelectedIndex = 0;
        }

        private void Label_MouseDoubleClick_2(object sender, MouseButtonEventArgs e)
        {
            cmbProductType.SelectedIndex = 0;
        }

        private void Label_MouseDoubleClick_3(object sender, MouseButtonEventArgs e)
        {
            cmbCompany.SelectedIndex = 0;

        }

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var x =Convert.ToInt16( cmbType.SelectedValue);

            switch (x)
            {
                case 1:
                    TempList= connect.GiveListProducts();
                    lvProducts.ItemsSource = TempList;
                    txtSearch.Text = "";
                    lblCountResult.Content = TempList.Count();
                    break;
                case 2:
                    //lvProducts.ItemsSource = connect.GiveListProductsWithSytyle(txtSearch.Text);
                    break;
                case 3:
                    //lvProducts.ItemsSource = connect.GiveListProductsWithSKU(txtSearch.Text);
                    break;
                case 4:
                    MessageBox.Show("salam");
                    break;
                default:
                    MessageBox.Show("Hi");
                    break;
            }
        }

        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            var itemSelected = lvProducts.SelectedItems;
            if(SwitchSelectedlist)
            {
                foreach (var item in itemSelected)
                {
                    if (SelectedList.Contains(item) == false)
                        SelectedList.Add(item as OwnProduct);
                }
            }
            else
            {
                foreach (var item in itemSelected)
                {                    
                        SelectedList.Remove(item as OwnProduct);
                }
                lvProducts.ItemsSource = SelectedList.ToList();
            }

            lvProducts.SelectedIndex = -1;
            lblCountSelectedList.Content = "IsSelected : " + SelectedList.Count();
 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(SwitchSelectedlist)
            {
                SwitchSelectedlist = false;
                lvProducts.ItemsSource = SelectedList.ToList();
                BtnShowSelectedlist.Content = "Show Main List";
                btnAddToList.Content="Remove to List";
                SetFilter();
            }
            else {
                SwitchSelectedlist = true;
                lvProducts.ItemsSource = TempList.ToList();
                SetFilter();
                BtnShowSelectedlist.Content = "Show Selected List";
                btnAddToList.Content = "Add to List";

            }
            
        }
    }
}
