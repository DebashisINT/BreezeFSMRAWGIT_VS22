using BusinessLogicLayer.SalesmanTrack;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using Models;
using MyShop.Models;
using SalesmanTrack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilityLayer;

namespace MyShop.Areas.MYSHOP.Controllers
{
    public class WarehouseMasterController : Controller
    {
        UserList lstuser = new UserList();
        MasterWarehouseBL objwar = new MasterWarehouseBL();
        public ActionResult WarehouseIndex()
        {
            WarehouseMasterModel model = new WarehouseMasterModel();
            List<CountryList> country = new List<CountryList>();
            List<StateList> state = new List<StateList>();
            List<CityDistrictList> city = new List<CityDistrictList>();
            List<DistributerList> shop = new List<DistributerList>();

            DataSet ds = objwar.GetMasterDropdownListAll();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    country.Add(new CountryList
                    {
                        cou_id = Convert.ToString(item["cou_id"]),
                        cou_country = Convert.ToString(item["cou_country"])
                    });
                }
            }

            if (ds != null && ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow item in ds.Tables[1].Rows)
                {
                    shop.Add(new DistributerList
                    {
                        Shop_Code = Convert.ToString(item["Shop_Code"]),
                        Shop_Name = Convert.ToString(item["Shop_Name"])
                    });
                }
            }
            model.State_List = state;
            model.CityDistrict_List = city;
            model.Country_List = country;
            model.Distributer_List = shop;
            return View(model);
        }

        [HttpPost]
        public ActionResult SatetListView(string countryID)
        {
            List<StateList> state = new List<StateList>();
            DataTable statedt = objwar.GetMasterDropdownList(countryID, "0", "STATE");
            if (statedt != null && statedt.Rows.Count > 0)
            {
                foreach (DataRow item in statedt.Rows)
                {
                    state.Add(new StateList
                    {
                        ID = Convert.ToString(item["id"]),
                        Name = Convert.ToString(item["state"])
                    });
                }
            }
            return Json(state, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CityListView(string StateID)
        {
            List<CityDistrictList> city = new List<CityDistrictList>();
            DataTable Citydt = objwar.GetMasterDropdownList("0", StateID, "CITY");
            if (Citydt != null && Citydt.Rows.Count > 0)
            {
                foreach (DataRow item in Citydt.Rows)
                {
                    city.Add(new CityDistrictList
                    {
                        city_id = Convert.ToString(item["city_id"]),
                        city_name = Convert.ToString(item["city_name"])
                    });
                }
            }
            return Json(city, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddWarehouseMaster(String action, String WareHouse_ID, string warehouseName, string address1, String address2, String address3, String country, String State, String City, String Pin, String contactPerson, String ContactPhone, List<String> Distributor, String defaultvalue)
        {
            if (WareHouse_ID == "")
            {
                WareHouse_ID = null;
            }
            String DistributorList="";

            string StateId = "";
            int i = 1;
            if (Distributor != null && Distributor.Count > 0)
            {
                foreach (string item in Distributor)
                {
                    if (i > 1)
                        DistributorList = DistributorList + "," + item;
                    else
                        DistributorList = item;
                    i++;
                }
            }

            String msg = "";
            DataTable dt = objwar.Masterdatainsert(action, WareHouse_ID, warehouseName, address1, address2, address3, country, State, City, Pin, contactPerson, ContactPhone, DistributorList, defaultvalue, Session["userid"].ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                msg = dt.Rows[0]["MSG"].ToString();
                //if (dt.Rows[0]["MSG"].ToString() == "-11")
                //{
                //    msg = "Default warehouse already define.";
                //}
                //else if (dt.Rows[0]["MSG"].ToString() == "10")
                //{
                //    msg = "Ware house added successfully.";
                //}
                //else if (dt.Rows[0]["MSG"].ToString() == "20")
                //{
                //    msg = "Ware house updated successfully.";
                //}
                //else
                //{
                //    msg = "please try again later.";
                //}
            }
            else
            {
                msg = "please try again later.";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult WarehouseMasterGrid(String Is_PageLoad)
        {
            Is_PageLoad = "";
            DataTable dt = objwar.MasterdataList("LIST", Session["userid"].ToString());
            return PartialView("~/Areas/MYSHOP/Views/WarehouseMaster/_PartialWareHouseGrid.cshtml", GetWareHouse(Is_PageLoad));
        }

        public IEnumerable GetWareHouse(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string Userid = Convert.ToString(Session["userid"]);
            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_WAREHOUSE_REPORTs
                        where d.LOGINID == Convert.ToInt32(Userid)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_WAREHOUSE_REPORTs
                        where d.LOGINID == Convert.ToInt32(Userid) && d.WAREHOUSE_ID == 0
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
        }

        [HttpPost]
        public ActionResult WareHouseView(string WareHouse_ID)
        {
            WarehouseMasterModel warehouse = new WarehouseMasterModel();
            DataTable dt = objwar.MasterdataView("VIEW", WareHouse_ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                warehouse.WarehouseID = dt.Rows[0]["WAREHOUSE_ID"].ToString();
                warehouse.WarehouseName = dt.Rows[0]["WAREHOUSE_NAME"].ToString();
                warehouse.Address1 = dt.Rows[0]["ADDRESS1"].ToString();
                warehouse.Address2 = dt.Rows[0]["ADDRESS2"].ToString();
                warehouse.Address3 = dt.Rows[0]["ADDRESS3"].ToString();
                warehouse.Country = dt.Rows[0]["COUNTRY_ID"].ToString();
                warehouse.State = dt.Rows[0]["STATE_ID"].ToString();
                warehouse.CityDistrict = dt.Rows[0]["DISTRICT"].ToString();
                warehouse.Pin = dt.Rows[0]["PIN"].ToString();
                warehouse.ContactName = dt.Rows[0]["CONTACT_NAME"].ToString();
                warehouse.ContactPhone = dt.Rows[0]["CONTACT_PHONE"].ToString();
                warehouse.isDefault = dt.Rows[0]["ISDEFAULT"].ToString();
                warehouse.Distributer = dt.Rows[0]["DISTRIBUTER_CODE"].ToString();
            }
            return Json(warehouse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult WareHouseDelete(string WareHouse_ID)
        {
            string returns = "Not Deleted please try again later.";
            DataTable dt = objwar.MasterdataView("DELETE", WareHouse_ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                returns = "Deleted Successfully.";
            }
            return Json(returns);
        }

        public ActionResult ExportWarehouselist(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetBookingGridViewSettings(), GetWareHouse(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetBookingGridViewSettings(), GetWareHouse(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetBookingGridViewSettings(), GetWareHouse(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetBookingGridViewSettings(), GetWareHouse(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetBookingGridViewSettings(), GetWareHouse(""));
                default:
                    break;
            }
            return null;
        }

        private GridViewSettings GetBookingGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "WareHouse Master";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "WareHouse Master";

            settings.Columns.Add(column =>
            {
                column.Caption = "Warehouse Name";
                column.FieldName = "WAREHOUSE_NAME";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Address 1";
                column.FieldName = "ADDRESS1";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Address 2";
                column.FieldName = "ADDRESS2";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Address 3";
                column.FieldName = "ADDRESS3";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Country";
                column.FieldName = "cou_country";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "state";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "City/District";
                column.FieldName = "city_name";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pin";
                column.FieldName = "PIN";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact Person Name";
                column.FieldName = "CONTACT_NAME";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact Person Phone";
                column.FieldName = "CONTACT_PHONE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Distributor";
                column.FieldName = "Shop_Name";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Is Default";
                column.FieldName = "ISDEFAULT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Created By";
                column.FieldName = "CREATED_BY";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Entered On";
                column.FieldName = "CREATE_ON";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy hh:mm:ss";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Modified By";
                column.FieldName = "UPDATED_BY";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Modified On";
                column.FieldName = "UPDATE_ON";
                column.PropertiesEdit.DisplayFormatString = "dd/MM/yyyy hh:mm:ss";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }

        public ActionResult PartialWarehousePermission()
        {

            //List<OrderDetailsSummaryProducts> oproduct = new List<OrderDetailsSummaryProducts>();
            try
            {
            //    string Is_PageLoad = string.Empty;
            //    String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
            //    List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

            //    DataTable dt = new DataTable();
            //    DataTable dtproduct = new DataTable();
            //    dtproduct = objshop.GetProducts();
            //    List<Productlist_Order> oproductlist = new List<Productlist_Order>();

            //    oproductlist = APIHelperMethods.ToModelList<Productlist_Order>(dtproduct);

            //    mproductwindow.products = oproductlist;

                return PartialView("~/Areas/MYSHOP/Views/WarehouseMaster/_PartialWareHousePermission.cshtml",null);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult PartialShopList()
        {
            List<ShopList> Shop_list = new List<ShopList>();
            DataTable Shopdt = objwar.GetMasterShopList("4","SHOPLIST");
            if (Shopdt != null && Shopdt.Rows.Count > 0)
            {
                foreach (DataRow item in Shopdt.Rows)
                {
                    Shop_list.Add(new ShopList
                    {
                        Shop_code = Convert.ToString(item["Shop_Code"]),
                        Shop_name = Convert.ToString(item["Shop_Name"]),
                        Type = Convert.ToString(item["SHOP_TYPE"]),
                        ContactNo = Convert.ToString(item["Shop_Owner_Contact"]),
                        //ReportTo = Convert.ToString(item["city_id"]),
                        State = Convert.ToString(item["state"]),
                        Address = Convert.ToString(item["SHOP_TYPE"])
                    });
                }
            }
            return PartialView(Shop_list);
        }

        public ActionResult GetShopList(EmployeeListModel model)
        {
            try
            {
                string StateId = "";
                int i = 1;
                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (i > 1)
                            StateId = StateId + "," + item;
                        else
                            StateId = item;
                        i++;
                    }
                }
                List<Getmasterstock> modelshop = new List<Getmasterstock>();
                DataTable dtshop = objwar.GetShopListByparam(StateId, "ShopbyState","4");
                modelshop = APIHelperMethods.ToModelList<Getmasterstock>(dtshop);
                return PartialView("~/Areas/MYSHOP/Views/WarehouseMaster/_ShopPartial.cshtml", modelshop);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }
    }
}