using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        OwnProduct product;
        bool IsExistCompany = false;
        int newCompanyId = 0;
        List<ScrollViewer> panels;
        //List<PictureBox> pictures;
        int step = 0;
        bool IsMetric = false;
        ConvertMetricInch cvrt;
        ConnectToDatabase db;
        List<Country> ListCountries;
        public static List<Color> ListColors;
        public static List<Category> ListCategories;
        public static List<SubCategory> ListSubCategories;
        public static List<ProductType> ListProductTypes;
        public static List<CategoriesSubCategory> ListCategoriesSubCategories;
        public AddProduct()
        {
            db = new ConnectToDatabase();
            ListCountries = db.countries();
            ListColors = db.listColors();
            ListCategories = db.categories();
            ListSubCategories = db.SubCategoriesList();
            ListProductTypes = db.productTypes();
            ListCategoriesSubCategories = db.categoriesSubCategories();


            InitializeComponent();

            
            cvrt = new ConvertMetricInch();
          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            panels = new List<ScrollViewer>() {Scr1,Scr2,Scr3,Scr4,Scr5 };

            cmbCountry.ItemsSource = db.countries();
            cmbCountry.SelectedIndex = 0;

            cmbCountryOfOrgin.ItemsSource = db.countries();
            cmbCountryOfOrgin.SelectedIndex = 0;

            cmbColors.ItemsSource = db.listColors();
            cmbColors.SelectedIndex = 0;

            cmbMaterial.ItemsSource = db.listMaterial();
            cmbMaterial.SelectedIndex = 0;

            cmbCategory.ItemsSource = db.categories();
            cmbCategory.SelectedIndex = 0;

            //cmbSubCategory.ItemsSource = db.subCategories();
            //cmbSubCategory.SelectedIndex = 0;

            //cmbProductType.ItemsSource = db.productTypes();
            //cmbProductType.SelectedIndex = 0;

            cmbBrand.ItemsSource = db.brands();
            cmbBrand.SelectedIndex = 0;

            
           
        }
        public void CalculateMetric()
        {
            txtWidth.Text = cvrt.WidthM.ToString();
            txtDepth.Text = cvrt.DepthM.ToString();
            txtHeight.Text = cvrt.HeightM.ToString();
            txtWeight.Text = cvrt.WeightM.ToString();
            txtLenght.Text = cvrt.LenghtM.ToString();
        }
        public void CalculateImperial()
        {
            txtWidth.Text = cvrt.WidthI.ToString();
            txtDepth.Text = cvrt.DepthI.ToString();
            txtHeight.Text = cvrt.HeightI.ToString();
            txtWeight.Text = cvrt.WeightI.ToString();
            txtLenght.Text = cvrt.LenghtI.ToString();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            panels.ElementAt(step).Visibility = Visibility.Hidden;
            step = step - 1;
            if (step == -1)
            {
                this.Close();
            }
            else
            {
                panels.ElementAt(step).Visibility = Visibility.Visible;
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if(!String.IsNullOrWhiteSpace(txtCompany.Text))
            {
                panels.ElementAt(step).Visibility = Visibility.Hidden;
                step = step + 1;
                if (step == 5)
                {
                    PeriveweAndPrepareForAdd();
                    GrAddInformation.Visibility = Visibility.Hidden;
                    GrReview.Visibility = Visibility.Visible;
                    step = 4;

                }
                else
                {
                    panels.ElementAt(step).Visibility = Visibility.Visible;
                }

            }
            else
            {
                MessageBox.Show("Enter Company Name");
                txtCompany.Text = "";
            }
            
        }

        private void PeriveweAndPrepareForAdd()
        {
            if(cmbProductType.SelectedValue==null || cmbSubCategory.SelectedValue==null)
            {
                lblReview1.Content = "Check Category - Sub Category or Product Type";
                lblReview2.Content = "";
                lblReview1.Foreground = Brushes.DarkRed;
                YesForAdd.IsEnabled = false;
            }
            else
            {
                lblReview1.Foreground = Brushes.Black;
                YesForAdd.IsEnabled = true;

                string mac = GetMACAddress().ToString();
                int categoryId = Convert.ToInt16(cmbCategory.SelectedValue.ToString());
                int SubCategoryId = Convert.ToInt16(cmbSubCategory.SelectedValue.ToString());
                int ProducttypeId = Convert.ToInt16(cmbProductType.SelectedValue.ToString());
                int colorId = Convert.ToInt16(cmbColors.SelectedValue.ToString());

                if (!IsExistCompany)
                {
                    Company company = new Company();
                    company.Id = db.GiveMeCompanyId(mac);
                    company.CompanyName = txtCompany.Text;
                    company.Manufacture = txtManufacture.Text;
                    company.Website = txtWebSite.Text;
                    company.Email = txtEmail.Text;
                    company.StreetAddress = txtStreetAddress.Text;
                    company.AddressLine2 = txtAddressLine2.Text;
                    company.City = txtCity.Text;
                    company.StateProvinceRegion = txtStateProvineRegion.Text;
                    company.ZipPostlCode = txtZipPostalCode.Text;
                    company.Country_Id_fk = Convert.ToInt16(cmbCountry.SelectedValue.ToString());
                    company.Phone = txtPhone.Text;
                    company.FAX = txtFax.Text;

                    db.AddCompany(company);
                    //if (ttt)
                    //    MessageBox.Show("Sucsess Save   " + company.CompanyName);
                    //else
                    //    MessageBox.Show("Error Db Save Company");
                }




                string lastSku = db.LastSku();
                string LastStyleNumber = db.LastStyleNumber();

                GenerateSkuStyle cd = new GenerateSkuStyle();
                var tt = cd.Generate(categoryId, SubCategoryId, ProducttypeId, colorId, lastSku, LastStyleNumber);

                product = new OwnProduct();
                product.Id = db.GiveMeProductId(mac);
                product.ProductType_Id_fk = ProducttypeId;
                product.Sku = tt["Sku"];
                product.StyleNumber = tt["StyleNumber"];
                product.ProductTittle = txtProductTittle.Text;
                product.Color_Id_fk = Convert.ToInt16(colorId);
                product.Material_Id_fk = Convert.ToInt16(cmbMaterial.SelectedValue.ToString());
                product.Brand_Id_fk = Convert.ToInt16(cmbBrand.SelectedValue.ToString());
                product.CountryofOrgin_Id_fk = Convert.ToInt16(cmbCountryOfOrgin.SelectedValue.ToString());
                product.Box = chkBox.IsChecked;
                product.Bag = chkBag.IsChecked;
                product.Wrap = chkWrap.IsChecked;
                product.NoPackaging = chkNoPacking.IsChecked;
                product.AirTransportation = chkAirTransportation.IsChecked;
                product.ShipTransportation = chkShipTransportation.IsChecked;
                product.Train = chkTrain.IsChecked;
                product.Truck = chkTruck.IsChecked;

                if (txtGsm.Text.Trim() != "")
                    product.Gsm = Int16.Parse(txtGsm.Text);
                //if (txtWidth.Text.Trim() != "")
                //    product.Width = float.Parse(txtWidth.Text);
                //if (txtLenght.Text.Trim() != "")
                //    product.length = float.Parse(txtLenght.Text);
                //if (txtDepth.Text.Trim() != "")
                //    product.Depth = float.Parse(txtDepth.Text);
                //if (txtHeight.Text.Trim() != "")
                //    product.Height = float.Parse(txtHeight.Text);
                //if (txtWeight.Text.Trim() != "")
                //    product.Weight = float.Parse(txtWeight.Text);
                product.SpecialPackingInstructions = txtSpecialPackaing.Text;
                if (txtPrice.Text.Trim() != "")
                    product.Price = float.Parse(txtPrice.Text);

                product.Width = float.Parse(cvrt.WidthI.ToString());
                product.length = float.Parse(cvrt.LenghtI.ToString());
                product.Depth = float.Parse(cvrt.DepthI.ToString());
                product.Height = float.Parse(cvrt.HeightI.ToString());
                product.Weight = float.Parse(cvrt.WeightI.ToString());

                product.Company_Id_fk = newCompanyId;
                product.DescribeMaterial = txtDescribeMaterial.Text;



                lblReview1.Content = String.Format(" Category : {0} \n\n SubCategory : {1} \n\n Product Type: {2} \n\n StyleNumber : {3} \n\n SKU :{4} \n\n Color :{5}\n\n GSM : {6} \n\n Price:{7} ",
                                    cmbCategory.Text, cmbSubCategory.Text, cmbProductType.Text, product.StyleNumber, product.Sku, cmbColors.Text, product.Gsm, product.Price);
                lblReview2.Foreground = Brushes.Black;
                lblReview2.Content = String.Format("Descibe Material : \n\n{0}", product.DescribeMaterial);

                // product.RFIDProtected = chkRFID.Checked;
                //product.TSAApprovedLock = chkTSA.Checked;
                //product.Expandable = chkExpandable.Checked;
                //product.WaterResistance = chkWaterResistance.Checked;
                //product.RetractableHandle = chkRestractable.Checked;
                //product.CheckPointFriendly = chkCheckPoint.Checked;
            }

        }
        public string GetMACAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        private void txtCompany_LostFocus(object sender, RoutedEventArgs e)
        {
           var IsExist = db.CompaniesList().FirstOrDefault(p => p.CompanyName.ToLower().CompareTo(txtCompany.Text.ToLower()) == 0);
            if(IsExist!=null)
            {
                IsExistCompany = true;


                var result = MessageBox.Show("This company already exists. Do you want to complate?", "Confirmation", MessageBoxButton.YesNo,MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    newCompanyId = IsExist.Id;
                    txtCompany.Text = IsExist.CompanyName.Trim();
                    txtManufacture.Text = IsExist.Manufacture.Trim();
                    txtWebSite.Text = IsExist.Website.Trim();
                    txtEmail.Text = IsExist.Email.Trim();
                    txtStreetAddress.Text = IsExist.StreetAddress.Trim();
                    txtAddressLine2.Text = IsExist.AddressLine2.Trim();
                    txtCity.Text = IsExist.City.Trim();
                    txtStateProvineRegion.Text = IsExist.StateProvinceRegion.Trim();
                    txtZipPostalCode.Text = IsExist.ZipPostlCode.Trim();
                    txtPhone.Text = IsExist.Phone.Trim();
                    txtFax.Text = IsExist.FAX.Trim();
                    cmbCountry.SelectedIndex = IsExist.Country_Id_fk - 1;
                    btnNext.Focus();
                }
                else if (result == MessageBoxResult.No)
                {
                    txtCompany.Text = "";
                }
                else
                {
                    //...
                }
            }
        }

        private void YesForAdd_Click(object sender, RoutedEventArgs e)
        {           
            db.AddProduct(product);
            btnSku.Content ="SKU :"+ product.Sku;
            btnStyleNumber.Content ="# : "+ product.StyleNumber;
            GrReview.Visibility = Visibility.Hidden;
            GrResult.Visibility = Visibility.Visible;
        }

        private void NoForAdd_Click(object sender, RoutedEventArgs e)
        {
            step = 3;
            Scr4.Visibility = Visibility.Visible;
            GrReview.Visibility = Visibility.Hidden;
            GrAddInformation.Visibility = Visibility.Visible;

        }

        private void btnAddAonther_Click(object sender, RoutedEventArgs e)
        {
            step = 1;
            Scr2.Visibility = Visibility.Visible;
            GrResult.Visibility = Visibility.Hidden;
            GrAddInformation.Visibility = Visibility.Visible;
            txtPrice.Text = "";
            txtGsm.Text = "";
            txtWidth.Text = "";
            txtLenght.Text = "";
            txtDepth.Text = "";
            txtHeight.Text = "";
            txtWeight.Text = "";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cmbCategory.SelectedIndex!=0)
            {
                var index = Convert.ToInt16(cmbCategory.SelectedValue);
                var list = ListCategoriesSubCategories.Where(p => p.Category_Id_fk == index || p.Category_Id_fk == 0).Select(p => p.SubCategory).ToList();
                cmbSubCategory.ItemsSource = list;
                cmbProductType.ItemsSource = null;
                cmbSubCategory.SelectedIndex = 0;
                cmbProductType.IsEnabled = false;
                cmbSubCategory.IsEnabled = true;
            }

        }

        private void cmbSubCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSubCategory.SelectedIndex != 0)
            {
                int index = Convert.ToInt16(cmbCategory.SelectedValue);
                int index1 = Convert.ToInt16(cmbSubCategory.SelectedValue);
                var index2 = ListCategoriesSubCategories.SingleOrDefault(p => p.Category_Id_fk == index && p.SubCategory_Id_fk == index1);
                if(index2!=null)
                {
                    var tt = ListProductTypes.Where(p => p.Categorysubcategoreis_Id_fk == index2.Id).Select(p => p).ToList();
                    cmbProductType.ItemsSource = tt;
                    cmbProductType.IsEnabled = true;
                }


            }
        }

        private void cmbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnImperial_Click(object sender, RoutedEventArgs e)
        {
            CalculateImperial();
            IsMetric = false;
            lblWidth.Content = "Width (in)";
            lblLenght.Content = "Lenght (in)";
            lblDepth.Content = "Depth (in)";
            lblHeight.Content = "Height (in)";
            lblWeight.Content = "Weight (lb)";
        }

        private void btnMetric_Click(object sender, RoutedEventArgs e)
        {
            CalculateMetric();
            IsMetric = true;
            lblWidth.Content = "Width (cm)";
            lblLenght.Content = "Lenght (cm)";
            lblDepth.Content = "Depth (cm)";
            lblHeight.Content = "Height (cm)";
            lblWeight.Content = "Weight (g)";
        }

        private void txtWidth_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtWidth.Text != "")
            {
                if (IsMetric)
                    cvrt.WidthM = Convert.ToDouble(txtWidth.Text);
                else
                    cvrt.WidthI = Convert.ToDouble(txtWidth.Text);
            }
        }

        private void txtLenght_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtLenght.Text != "")
            {
                if (IsMetric)
                    cvrt.LenghtM = Convert.ToDouble(txtLenght.Text);
                else
                    cvrt.LenghtI = Convert.ToDouble(txtLenght.Text);
            }
        }

        private void txtDepth_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtDepth.Text != "")
            {
                if (IsMetric)
                    cvrt.DepthM = Convert.ToDouble(txtDepth.Text);
                else
                    cvrt.DepthI = Convert.ToDouble(txtDepth.Text);
            }
        }

        private void txtHeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtHeight.Text != "")
            {
                if (IsMetric)
                    cvrt.HeightM = Convert.ToDouble(txtHeight.Text);
                else
                    cvrt.HeightI = Convert.ToDouble(txtHeight.Text);
            }
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtWeight.Text != "")
            {
                if (IsMetric)
                    cvrt.WeightM = Convert.ToDouble(txtWeight.Text);
                else
                    cvrt.WeightI = Convert.ToDouble(txtWeight.Text);
            }
        }

        public bool SetNumeric(object sender, KeyEventArgs e, TextBox txt)
        {
            bool result = false;
            var tr = (sender as TextBox).Text.IndexOf('.');

            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back) || (e.Key == Key.Decimal))
            { result = false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.OemPeriod))
            { result = false; }
            else
            { result = true; }

            if (e.Key == Key.OemPeriod && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result = true;
            }
            if (e.Key == Key.Decimal && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result = true;
            }
            var count = txt.Text.Split('.');
            if (count.Count() > 1)
            {
                if (count[1].Count() > 3)
                {
                    result = true;
                }
            }
            if (e.Key == Key.Tab)
                result = false;
            return result;
        }

        private void txtWidth_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtWidth);
        }

        private void txtLenght_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtLenght);
        }

        private void txtDepth_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtDepth);
        }

        private void txtHeight_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtHeight);
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtWeight);
        }

        private void txtGsm_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e, txtGsm);
        }

        private void btnSku_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(product.Sku);
        }

        private void btnStyleNumber_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(product.StyleNumber);
        }
    }
}
