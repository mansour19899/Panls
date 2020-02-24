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
    /// Interaction logic for CostCenter.xaml
    /// </summary>
    public partial class CostCenter : Window
    {
        double MainPrice = 0;
        double LandedCost = 0;
        double WholeSalePrice = 0;
        double SalesCom5 = 0;
        double CreditInsurance10 = 0;
        double WholeSaleCash = 0;
        double wholeSaleCredit = 0;
        double WLGrossMargin = 0;
        double WLMargin = 0;
        double Retail = 0;
        double RTLGrossMargin = 0;
        double USDtoCAD = 0;
        double LandedCostRate = 15;
        public CostCenter()
        {
            InitializeComponent();
        }
        public CostCenter(double price)
        {
            InitializeComponent();
            MainPrice = price;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            USDtoCAD = 1.38;
            txtCostRate.Text = "15";

        }
        public void CalculatePrice()
        {

            LandedCost = MainPrice * (1 + (LandedCostRate / 100));
            WholeSalePrice = LandedCost * 1.55;
            SalesCom5 = WholeSalePrice * 0.05;
            CreditInsurance10 = WholeSalePrice * 0.1 + SalesCom5;
            WholeSaleCash = SalesCom5 * 0.05 + WholeSalePrice;
            wholeSaleCredit = WholeSaleCash * 1.1;
            WLGrossMargin = WholeSaleCash - LandedCost;
            WLMargin = WLGrossMargin / WholeSaleCash * 100;
            Retail = LandedCost * 4;
            RTLGrossMargin = (Retail - LandedCost) / Retail * 100;
        }
        public void UpdatePrice()
        {
            if(LandedCost!=0)
            { 
            txtLandedCostUSD.Text = Math.Round(LandedCost, 2).ToString();
            txtWholesaleUSD.Text = Math.Round(WholeSalePrice, 2).ToString();
            txtSales5USD.Text = Math.Round(SalesCom5, 2).ToString();
            txtCreditIN10USD.Text = Math.Round(CreditInsurance10, 2).ToString();
            txtWholeCashUSD.Text = Math.Round(WholeSaleCash, 2).ToString();
            txtWholeCreditUSD.Text = Math.Round(wholeSaleCredit, 2).ToString();
            txtWLGrossUSD.Text = Math.Round(WLGrossMargin, 2).ToString();
            txtWLMarginUSD.Text = Math.Round(WLMargin, 2).ToString() + " %";
            txtReatailUSD.Text = Math.Round(Retail, 2).ToString();
            txtRTLGrossUSD.Text = Math.Round(RTLGrossMargin, 2).ToString() + " %";

            txtLandedCostCAD.Text = Math.Round(LandedCost * USDtoCAD, 2).ToString();
            txtWholesaleCAD.Text = Math.Round(WholeSalePrice * USDtoCAD, 2).ToString();
            txtSales5CAD.Text = Math.Round(SalesCom5 * USDtoCAD, 2).ToString();
            txtCreditIN10CAD.Text = Math.Round(CreditInsurance10 * USDtoCAD, 2).ToString();
            txtWholeCashCAD.Text = Math.Round(WholeSaleCash * USDtoCAD, 2).ToString();
            txtWholeCreditCAD.Text = Math.Round(wholeSaleCredit * USDtoCAD, 2).ToString();
            txtWLGrossCAD.Text = Math.Round(WLGrossMargin * USDtoCAD, 2).ToString();
            txtWLMarginCAD.Text = Math.Round(WLMargin, 2).ToString() + " %";
            txtReatailCAD.Text = Math.Round(Retail * USDtoCAD, 2).ToString();
            txtRTLGrossCAD.Text = Math.Round(RTLGrossMargin, 2).ToString() + " %";
            }

        }
        public void CalculateAndUpdatetxt()
        {
            CalculatePrice();
            UpdatePrice();
        }
        public bool SetNumeric(object sender, KeyEventArgs e,TextBox txt)
        {
            bool result = false;

            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) || (e.Key == Key.Back))
            { result= false; }
            else if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key == Key.OemPeriod))
            { result= false; }
            else
            { result= true; }

            if (e.Key == Key.OemPeriod && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                result= true;
            }
            var count = txt.Text.Split('.');
            if (count.Count() > 1)
            {
                if (count[1].Count() > 3)
                {
                    result= true;
                }
            }
            return result;
        }

        private void txtMainPrice_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = SetNumeric(sender, e,txtMainPrice);

        }

        private void txtCostRate_KeyDown(object sender, KeyEventArgs e)
        {
         
            e.Handled = SetNumeric(sender, e,txtCostRate);
        }

        private void txtMainPrice_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtMainPrice.Text != "")
                MainPrice = Convert.ToDouble(txtMainPrice.Text);
            else
                MainPrice = 0;
            CalculateAndUpdatetxt();
        }

        private void txtCostRate_KeyUp(object sender, KeyEventArgs e)
        {
            rdbBangladesh.IsChecked = false;
            rdbChina.IsChecked = false;
            if (txtCostRate.Text != "")
                LandedCostRate = Convert.ToDouble(txtCostRate.Text);
            else
                LandedCostRate = 0;
            CalculateAndUpdatetxt();
        }

        private void rdbChina_Checked(object sender, RoutedEventArgs e)
        {
            LandedCostRate = 18;
            txtCostRate.Text = LandedCostRate.ToString();
            CalculateAndUpdatetxt();
        }

        private void rdbBangladesh_Checked(object sender, RoutedEventArgs e)
        {
            LandedCostRate = 15;
            txtCostRate.Text = LandedCostRate.ToString();
            CalculateAndUpdatetxt();
        }
    }
}
