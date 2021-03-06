﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;


namespace Panls
{
    class ConnectToDatabase
    {
        VanmeEntities1 db;
        
        int StartId = 0;
        int EndId = 0;

        public ConnectToDatabase()
        {
            db = new VanmeEntities1();
            SetStartId();

        }
        public void SetStartId()
        {
          string  mac = GetMACAddress().ToString();
            StartId = Convert.ToInt16(db.Users.FirstOrDefault(p => p.MacAdress == mac).StartNumber);
             EndId = StartId + 10000;
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

        public OwnProduct GiveMeProduct(string str)
        {
            var x = db.OwnProducts.Where(p => p.Sku.CompareTo(str) == 0).FirstOrDefault();
            int yy = int.Parse(str);
            var y = db.OwnProducts.Where(p => p.Id == yy).FirstOrDefault();

            return y;
        }

        public List<OwnProduct> GiveListProductsWithSytyle(string str)
        {
            //var x = db.OwnProducts.Where(p => p.StyleNumber.Trim().CompareTo(str.Trim()) == 0).ToList();               
            var x = db.OwnProducts.Where(p => p.StyleNumber.Trim().Contains(str.Trim())).ToList();               

            return x;
        }

        public List<OwnProduct> GiveListProductsWithSKU(string str)
        {
            var x = db.OwnProducts.Where(p => p.Sku.Trim().Contains(str.Trim())).ToList();

            return x;
        }
        public List<OwnProduct> GiveProductWithBarcode(string str)
        {
            var x = db.OwnProducts.Where(p => p.Barcode.BarcodeNumber.Trim().CompareTo(str)==0).ToList();

            return x;
        }
        public OwnProduct GiveProductWithBarcode1(string str)
        {
            var x = db.OwnProducts.Where(p => p.Barcode.BarcodeNumber.Trim().CompareTo(str) == 0).FirstOrDefault();

            return x;
        }

        public List<OwnProduct> GiveListProducts()
        {
            var x = db.OwnProducts.ToList();
            return x;
        }
        public List<Company> CompaniesList()
        {
            var x = db.Companies.ToList();
            foreach (var item in x)
            {
                item.CompanyName = item.CompanyName.Trim();
            }
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
        public bool AddImage(int ProductId,string str)
        {
            var CreatImageId = db.Images.ToList().LastOrDefault(p => p.Id > StartId && p.Id < EndId);
            int newImageId = StartId + 1;
            if(CreatImageId !=null)
            {
                newImageId = CreatImageId.Id + 1;
            }

            Image t = new Image() {Id=newImageId, Product_Id_fk = ProductId, ImageName = str };
           
            var d = db.Companies.FirstOrDefault();            
            db.Images.Add(t);
            var r = db.SaveChanges();
            if (r > 0)
                return true;
            else
                return false;

        }
        public List<string> GiveImagesOfProduct(int Id)
        {
            var list = db.Images.Where(p => p.Product_Id_fk == Id).Select(p=>p.ImageName.Trim()).Take(6).ToList();
            return list;
        }

        public List<Color> listColors()
        {
            var list = db.Colors.ToList();
            return list;
        }

        public List<Material> listMaterial()
        {
            var list = db.Materials.ToList();
            return list;
        }

        public List<Country> countries()
        {
            var list = db.Countries.ToList();
            return list;
        }
        public List<Category> categories()
        {
            var list = db.Categories.ToList();
            return list;
        }

        public List<SubCategory> subCategories()
        {
            var list = db.SubCategories.ToList();
            return list;
        }
        public List<CategoriesSubCategory> categoriesSubCategories()
        {
            var list = db.CategoriesSubCategories.ToList();
            return list;
        }
        public List<ProductType> productTypes()
        {
            var list = db.ProductTypes.ToList();
            return list;
        }
        public List<Brand> brands()
        {
            var list = db.Brands.ToList();
            return list;
        }

        public string LastSku()
        {
            return db.OwnProducts.ToList().LastOrDefault().Sku.Trim();
        }
        public string LastStyleNumber()
        {
            return db.OwnProducts.ToList().LastOrDefault().StyleNumber.Trim();
        }
        
        public int GiveMeStartId(string mac)
        {
            return Convert.ToInt16(db.Users.FirstOrDefault(p => p.MacAdress == mac).StartNumber);
        }
        public int GiveMeCompanyId(string mac)
        {
            var startId= Convert.ToInt16(db.Users.FirstOrDefault(p => p.MacAdress == mac).StartNumber);
            int EndId = startId + 10000;
            var t = db.Companies.ToList().LastOrDefault(p=>p.Id>startId && p.Id<EndId);
            if (t != null)
                return t.Id + 1;
            else
             return startId+1;
        }
        public int GiveMeProductId(string mac)
        {
            var startId = Convert.ToInt16(db.Users.FirstOrDefault(p => p.MacAdress == mac).StartNumber);
            int EndId = startId + 10000;
            var t = db.OwnProducts.ToList().LastOrDefault(p => p.Id > startId && p.Id < EndId);
            if (t != null)
                return t.Id + 1;
            else
                return startId + 1;
        }
        public void AddCompany(Company company)
        {
          db.Companies.Add(company);           

        }
        public void AddProduct(OwnProduct ownProduct)
        {
            db.OwnProducts.Add(ownProduct);

        }

        public int SaveChange()
        {
            return db.SaveChanges();
        }
    }
}
