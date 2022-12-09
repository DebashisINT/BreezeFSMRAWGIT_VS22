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
    public class OrderStatusController : Controller
    {
        UserList lstuser = new UserList();
        OrderStatusBL objordr = new OrderStatusBL();
        OrderDetailsSummaryProducts mproductwindow = new OrderDetailsSummaryProducts();
        public ActionResult OrderStatusIndex()
        {
            try
            {
                string userid = Session["userid"].ToString();
                OrderStatusModel omodel = new OrderStatusModel();
                omodel.FromDate = DateTime.Now.ToString("dd-MM-yyyy");
                omodel.ToDate = DateTime.Now.ToString("dd-MM-yyyy");
                // omodel.UserID = userid;
                //Rev Debashis 0025198
                DataTable dtbranch = lstuser.GetHeadBranchList(Convert.ToString(Session["userbranchHierarchy"]), "HO");
                DataTable dtBranchChild = new DataTable();
                if (dtbranch.Rows.Count > 0)
                {
                    dtBranchChild = lstuser.GetChildBranch(Convert.ToString(Session["userbranchHierarchy"]));
                    if (dtBranchChild.Rows.Count > 0)
                    {
                        DataRow dr;
                        dr = dtbranch.NewRow();
                        dr[0] = 0;
                        dr[1] = "All";
                        dtbranch.Rows.Add(dr);
                        dtbranch.DefaultView.Sort = "BRANCH_ID ASC";
                        dtbranch = dtbranch.DefaultView.ToTable();
                    }
                }
                omodel.modelbranch = APIHelperMethods.ToModelList<GetBranch>(dtbranch);
                string h_id = omodel.modelbranch.First().BRANCH_ID.ToString();
                ViewBag.h_id = h_id;
                //End of Rev Debashis 0025198
                return View(omodel);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult GetOrderStatusReportlistPartial(OrderStatusModel model)
        {
            try
            {
                DataTable dt = new DataTable();
                string Employee = "";
                int i = 1;

                if (model.EmployeeID != null && model.EmployeeID.Count > 0)
                {
                    foreach (string item in model.EmployeeID)
                    {
                        if (i > 1)
                            Employee = Employee + "," + item;
                        else
                            Employee = item;
                        i++;
                    }
                }

                string state = "";
                int k = 1;

                if (model.StateId != null && model.StateId.Count > 0)
                {
                    foreach (string item in model.StateId)
                    {
                        if (k > 1)
                            state = state + "," + item;
                        else
                            state = item;
                        k++;
                    }

                }

                string desig = "";
                int j = 1;

                if (model.desgid != null && model.desgid.Count > 0)
                {
                    foreach (string item in model.desgid)
                    {
                        if (j > 1)
                            desig = desig + "," + item;
                        else
                            desig = item;
                        j++;
                    }

                }

                //Rev Debashis 0025198
                string Branch_Id = "";
                int b = 1;
                if (model.BranchId != null && model.BranchId.Count > 0)
                {
                    foreach (string item in model.BranchId)
                    {
                        if (b > 1)
                            Branch_Id = Branch_Id + "," + item;
                        else
                            Branch_Id = item;
                        b++;
                    }
                }
                //End of Rev Debashis 0025198

                string Is_PageLoad = string.Empty;
                if (model.Ispageload == "0")
                {
                    Is_PageLoad = "Ispageload";
                    model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                    model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
                }

                if (model.FromDate == null)
                {
                    model.FromDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    model.FromDate = model.FromDate.Split('-')[2] + '-' + model.FromDate.Split('-')[1] + '-' + model.FromDate.Split('-')[0];
                }

                if (model.ToDate == null)
                {
                    model.ToDate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    model.ToDate = model.ToDate.Split('-')[2] + '-' + model.ToDate.Split('-')[1] + '-' + model.ToDate.Split('-')[0];
                }
                string FromDate = model.FromDate;
                string ToDate = model.ToDate;
                string userID = Convert.ToString(Session["userid"]);
                if (model.Ispageload != "0")
                {
                    double days = (Convert.ToDateTime(ToDate) - Convert.ToDateTime(FromDate)).TotalDays;
                    if (days <= 30)
                    {
                        //Rev Debashis 0025198
                        //dt = objordr.GenerateLocationReportData(Employee, FromDate, ToDate, Convert.ToInt64(userID), state, desig, model.REPORT_BY);
                        dt = objordr.GenerateLocationReportData(Employee, FromDate, ToDate, Convert.ToInt64(userID), state, desig, model.REPORT_BY, Branch_Id);
                        //End of Rev Debashis 0025198
                    }
                }
                return PartialView("_PartialOrderStatusReportGrid", GetOrderStatusReportActive(Is_PageLoad));
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public IEnumerable GetOrderStatusReportActive(string Is_PageLoad)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ERP_ConnectionString"].ConnectionString;
            string userID = Convert.ToString(Session["userid"]);

            if (Is_PageLoad != "Ispageload")
            {
                ReportsDataContext dc = new ReportsDataContext(connectionString);
                var q = from d in dc.FTS_ORDER_STATUS_REPORTs
                        where d.LOGIN_ID == Convert.ToInt32(userID)
                        orderby d.SEQ ascending
                        select d;
                return q;
            }
            else
            {
                if (Is_PageLoad != "Ispageload")
                {
                    ReportsDataContext dc = new ReportsDataContext(connectionString);
                    var q = from d in dc.FTS_ORDER_STATUS_REPORTs
                            where d.LOGIN_ID == Convert.ToInt32(userID)
                            orderby d.SEQ ascending
                            select d;
                    return q;
                }
                else
                {
                    return null;
                }
            }
        }

        public ActionResult PartialOrderStatusAllProducts(string OrderId = null)
        {
            try
            {
                string Is_PageLoad = string.Empty;
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                List<GpsStatusClasstOutput> omel = new List<GpsStatusClasstOutput>();

                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(OrderId))
                {
                    dt = objordr.GetallorderDetails(Int32.Parse(OrderId));
                    mproductwindow.productdetails = APIHelperMethods.ToModelList<OrderDetailsSummaryProductslist>(dt);
                }
                return PartialView("_PartialOrderStatusProductDetails", mproductwindow.productdetails);
            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }

        public ActionResult ExportOrderStatuslist(int type)
        {
            switch (type)
            {
                case 1:
                    return GridViewExtension.ExportToPdf(GetOrderReportGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 2:
                    return GridViewExtension.ExportToXlsx(GetOrderReportGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 3:
                    return GridViewExtension.ExportToXls(GetOrderReportGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 4:
                    return GridViewExtension.ExportToRtf(GetOrderReportGridViewSettings(), GetOrderStatusReportActive(""));
                //break;
                case 5:
                    return GridViewExtension.ExportToCsv(GetOrderReportGridViewSettings(), GetOrderStatusReportActive(""));
                default:
                    break;
            }

            return null;
        }

        private GridViewSettings GetOrderReportGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Order Status";
            // settings.CallbackRouteValues = new { Controller = "Employee", Action = "ExportEmployee" };
            // Export-specific settings
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Order Status";

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee ID";
                column.FieldName = "Employee_ID";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Employee Name";
                column.FieldName = "Employee_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "State";
                column.FieldName = "State_name";

            });

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "District";
                column.FieldName = "SHOP_DISTRICT";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Pincode";
                column.FieldName = "SHOP_PINCODE";
            });
            //End of Rev Debashis -- 0024575

            settings.Columns.Add(column =>
            {
                column.Caption = "Branch";
                column.FieldName = "BRANCHDESC";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Name";
                column.FieldName = "Shop_Name";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Code";
                column.FieldName = "ENTITYCODE";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Address";
                column.FieldName = "Address";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Contact";
                column.FieldName = "Contact";
            });

            //Rev Debashis -- 0024577
            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Phone No.";
                column.FieldName = "ALT_MOBILENO1";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Alternate Email ID";
                column.FieldName = "SHOP_OWNER_EMAIL2";
            });
            //End of Rev Debashis -- 0024577

            settings.Columns.Add(column =>
            {
                column.Caption = "Shop Type";
                column.FieldName = "Shop_Type";

            });

            //Rev Debashis -- 0024576
            settings.Columns.Add(column =>
            {
                column.Caption = "Sub Type";
                column.FieldName = "SubType";

            });
            //Rev Debashis -- 0024576

            //Rev Debashis -- 0024575
            settings.Columns.Add(column =>
            {
                column.Caption = "Cluster";
                column.FieldName = "SHOP_CLUSTER";
            });
            //End of Rev Debashis -- 0024575

            settings.Columns.Add(column =>
            {
                column.Caption = "PP Name";
                column.FieldName = "PPName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "DD Name";
                column.FieldName = "DDName";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Number";
                column.FieldName = "Order_Number";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Date";
                column.FieldName = "Order_Date";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Entered On";
                column.FieldName = "Order_CreateDate";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Order Value";
                column.FieldName = "Order_Value";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Inv No";
                column.FieldName = "Invoice_Number";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Inv Date";
                column.FieldName = "Invoice_Date";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Inv Entered On";
                column.FieldName = "Invoice_CreateDate";
                column.PropertiesEdit.DisplayFormatString = "dd-MM-yyyy hh:mm:ss";
                // (column.PropertiesEdit as DateEditProperties).EditFormatString = "dd-MM-yyyy";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Delivered Value";
                column.FieldName = "Delivered_Value";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
    }
}