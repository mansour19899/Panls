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
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;
using System.IO;

namespace Panls
{
    /// <summary>
    /// Interaction logic for BackupPanel.xaml
    /// </summary>
    public partial class BackupPanel : Window
    {
        public BackupPanel()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateBackup();
        }
        
        private void CreateBackup ()
        {
            ConnectToDatabase db = new ConnectToDatabase();
            var list = db.GiveListProducts();

            var PathApp = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            FileInfo newFile = new FileInfo(PathApp + "\\T-Backup.xlsx");
            string filee = PathApp + "\\Backup" + "\\" + "Backup " + DateTime.Today.ToShortDateString().Replace("/", "") + ".xlsx";
            string fileOneDrive = PathApp + "\\Backup" + "\\" + "Backup " + DateTime.Today.ToShortDateString().Replace("/", "") + ".xlsx";
            FileInfo newFilee = new FileInfo(filee);

            ExcelPackage excel = new ExcelPackage(newFilee, newFile);

            //Add the Content sheet
            //  var ws = pck.Workbook.Worksheets.Add("Content");

            var ws = excel.Workbook.Worksheets.ElementAt(0);
            // var wss = excel.Workbook.Worksheets("");
           // var workSheet = excel.Workbook.Worksheets.Add("hi");
            var workSheet2 = excel.Workbook.Worksheets.Add("Sheet12");
            ExcelWorksheet workSheet = excel.Workbook.Worksheets[2];

            //workSheet.Row(1).Height = 90;
            //workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //workSheet.Row(1).Style.Font.Bold = true;

            // Header of the Excel sheet 
            workSheet.Cells[1, 1].Value = "Mansour";
            workSheet.Cells[1, 2].Value = "Id";
            workSheet.Cells[1, 3].Value = "Name";
            int i = 3;
            foreach (var item in list)
            {
                workSheet.Cells[i, 2].Value =Convert.ToInt32( item.StyleNumber.Split('.').Last()).ToString();
                workSheet.Cells[i, 3].Value = item.Id.ToString();
                workSheet.Cells[i, 4].Value = item.ProductType.Name;
                workSheet.Cells[i, 5].Value = item.Color.Name.Trim();
                workSheet.Cells[i, 6].Value = item.Price.ToString();
                workSheet.Cells[i, 7].Value = item.Width.ToString();
                workSheet.Cells[i, 8].Value = item.length.ToString();
                workSheet.Cells[i, 9].Value = item.Weight.ToString();
                workSheet.Cells[i, 10].Value = item.Gsm.ToString();
                workSheet.Cells[i, 11].Value = item.Company.CompanyName.Trim();
                //workSheet.Cells[i, 12].Value = item.Barcode.BarcodeNumber;
                workSheet.Cells[i, 13].Value = item.Sku.Trim();
                workSheet.Cells[i, 14].Value = item.StyleNumber.Trim();


                i++;
            }

            //workSheet.Column(1).AutoFit();
            //workSheet.Column(2).AutoFit();
            //workSheet.Column(3).AutoFit();

            FileStream objFileStrm = File.Create(filee);
            objFileStrm.Close();

            // Write content to excel file  
            File.WriteAllBytes(filee, excel.GetAsByteArray());
        }
    }
}
