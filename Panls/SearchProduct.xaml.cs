using Microsoft.Win32;
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
        OwnProduct SelectedProduct;
        List<string> UploadedPicturs;
        StringBuilder TempBarcode;
        bool AllowScanBarcodeCon = false;



        Boolean SwitchSelectedlist = true;
        public SearchProduct()
        {
            InitializeComponent();

             connect = new ConnectToDatabase();
            SelectedList = new List<OwnProduct>();
            UploadedPicturs = new List<string>();
            TempBarcode = new StringBuilder();
            //Imageis = new List<Image>() {img2 };
            //List<ImageSource> Imageis = new List<ImageSource>();
            //Imageis.Add(img3.Source);
            //ImageSource imageSource = new BitmapImage(new Uri("D:\\Product-image\\2020131392.jpg"));
            //img4.Source = imageSource;            

        }
        public void UploadPictures()
        {
            if(UploadedPicturs.Count>0)
            {
                img1.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(0)));
            }
            if (UploadedPicturs.Count > 1)
            {
                img2.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(1)));
            }
            if (UploadedPicturs.Count > 2)
            {
                img3.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(2)));
            }
            if (UploadedPicturs.Count > 3)
            {
                img4.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(3)));
            }
            if (UploadedPicturs.Count > 4)
            {
                img5.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(4)));
            }
            if (UploadedPicturs.Count > 5)
            {
                img6.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(5)));
            }
            if (UploadedPicturs.Count > 6)
            {
                img7.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(6)));
            }
            if (UploadedPicturs.Count > 7)
            {
                img8.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(7)));
            }
            if (UploadedPicturs.Count > 8)
            {
                img9.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(8)));
            }
            if (UploadedPicturs.Count > 9)
            {
                img10.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(9)));
            }
            if (UploadedPicturs.Count > 10)
            {
                img11.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(10)));
            }
            if (UploadedPicturs.Count > 11)
            {
                img12.Source = new BitmapImage(new Uri(UploadedPicturs.ElementAt(11)));
            }
        }
        private void ClearImages()
        {
            imgMain.Source = null;
            img1.Source = null;
            img2.Source = null;
            img3.Source = null;
            img4.Source = null;
            img5.Source = null;
            img6.Source = null;
            img7.Source = null;
            img8.Source = null;
            img9.Source = null;
            img10.Source = null;
            img11.Source = null;
            img12.Source = null;
        }
        public void FindPictureProduct(int Id)
        {
            UploadedPicturs.Clear();
            ClearImages();
            var y = connect.GiveImagesOfProduct(Id);
            string x = ManageFileFolder.destinationFolder;
            if(y.Count()!=0)
            {
                foreach (var item in y)
                {
                    UploadedPicturs.Add(x+"\\"+item);
                }
            }
            UploadPictures();
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
                new { Id = 3, Name = "Barcode" },
                new { Id = 4, Name = "SKU" },
                new { Id = 5, Name = "Tittle" }
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
            lblDescription.Content = string.Format("{0}",t.DescribeMaterial);
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
                        lvProducts.ItemsSource = connect.GiveProductWithBarcode(txtSearch.Text);
                        //ShowProduct(connect.GiveProductWithBarcode1(txtSearch.Text));
                        txtSearch.Clear();
                        break;
                    case "4":
                        lvProducts.ItemsSource = connect.GiveListProductsWithSKU(txtSearch.Text);
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

            SelectedProduct = (OwnProduct)lvProducts.ItemContainerGenerator.ItemFromContainer(dep);

            ShowProduct(SelectedProduct);



        }

        private void  ShowProduct(OwnProduct ownProduct)
        {
            SetInformation(ownProduct);
            FindPictureProduct(ownProduct.Id);
            GrDescription.Visibility = Visibility.Hidden;
            GrImagees.Visibility = Visibility.Hidden;
            GrSearch.Visibility = Visibility.Hidden;
            GrTotalInformation.Visibility = Visibility.Visible;
            GrInformation.Visibility = Visibility.Visible;

            btnDescription.Content = "Description";
            btnImages.Content = "Images";
        }

        private void lblHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GrTotalInformation.Visibility = Visibility.Hidden;
            GrSearch.Visibility = Visibility.Visible;
            txtSearch.Focus();
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
            AllowScanBarcodeCon = false;
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
                    txtSearch.Clear();
                    AllowScanBarcodeCon = true;
                    break;
                case 4:
                    MessageBox.Show("salam");
                    break;
                default:
                    MessageBox.Show("Hi");
                    break;
            }
            txtSearch.Focus();
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

        private void lblPrice_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CostCenter cost = new CostCenter(lblPrice.Content.ToString());
            cost.Owner = this;
            cost.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(btnDescription.Content.ToString().CompareTo("Description")==0)
            {
                GrInformation.Visibility = Visibility.Hidden;
                GrImagees.Visibility = Visibility.Hidden;
                GrDescription.Visibility = Visibility.Visible;
                btnDescription.Content = "Information";
                btnImages.Content = "Images";
            }
            else
            {
                GrInformation.Visibility = Visibility.Visible;
                GrDescription.Visibility = Visibility.Hidden;
                btnDescription.Content = "Description";
            }
        }

        private void btnImages_Click(object sender, RoutedEventArgs e)
        {
            if (btnImages.Content.ToString().CompareTo("Images") == 0)
            {
                GrInformation.Visibility = Visibility.Hidden;
                GrDescription.Visibility = Visibility.Hidden;
                GrImagees.Visibility = Visibility.Visible;
                btnImages.Content = "Information";
                btnDescription.Content= "Description";
            }
            else
            {
                GrInformation.Visibility = Visibility.Visible;
                GrImagees.Visibility = Visibility.Hidden;
                btnImages.Content = "Images";
            }
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".png";
            dlg.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                    ManageFileFolder ff = new ManageFileFolder();
                    string pic = ff.copy(filename);
                 UploadedPicturs.Add(pic);
                imgMain.Source = new BitmapImage(new Uri(pic));
                var yt = pic.Split('\\').Last();
                if(connect.AddImage(SelectedProduct.Id,yt))
                 UploadPictures();
                //MessageBox.Show("pic added");
                
            }


            //OpenFileDialog open = new OpenFileDialog();
            //// image filters  
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            //if (open.ShowDialog() == MessageBoxResult.OK)
            //{
            //    // display image in picture box  
            //    ImageSource imageSource = new BitmapImage(new Uri("D:\\Product-image\\2020131392.jpg"));
            //    img4.Source = imageSource;
            //    // image file path  
            //    //MessageBox.Show(open.FileName);
            //    ManageFileFolder ff = new ManageFileFolder();
            //    string pic = ff.copy(open.FileName);
            //    UploadedPicturs.Add(pic);
            //    uploadpic();
            int x = 0;
            
        }

        private void img1_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img1.Source;
        }

        private void img2_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img2.Source;
        }

        private void img3_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img3.Source;
        }

        private void img4_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img4.Source;
        }

        private void img5_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img5.Source;
        }

        private void img6_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img6.Source;
        }

        private void img7_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img7.Source;
        }

        private void img8_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img8.Source;
        }

        private void img9_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img9.Source;
        }

        private void img10_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img10.Source;
        }

        private void img11_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img11.Source;
        }

        private void img12_MouseEnter(object sender, MouseEventArgs e)
        {
            imgMain.Source = img12.Source;
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {   if(AllowScanBarcodeCon)
            {
                if (e.Key == Key.Enter)
                {
                    var productt = connect.GiveProductWithBarcode1(TempBarcode.ToString());
                    if(productt==null)
                    {
                        MessageBox.Show("Not Exist");
                    }
                    else
                        ShowProduct(productt);

                    TempBarcode.Clear();
                    txtSearch.Clear();
                }
                else
                {
                    TempBarcode.Append(e.Key.ToString().ElementAt(1));
                }
            }


        }
    }
}
