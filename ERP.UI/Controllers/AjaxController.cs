using BusinessLogicLayer.UserGroupsBLS;
using DataAccessLayer;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using DevExpress.XtraPrinting;
using EntityLayer.UserGroupsEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class AjaxController : Controller
    {
        //
        // GET: /Ajax/

        public PartialViewResult _PartialGroupUserListForShow(int GroupId)
        {
            ViewBag.GroupId = GroupId;
            List<GroupUserListModel> model = new UserGroupBL().GetUsersByGroupIdKeyValue(GroupId);
            return PartialView(model);
        }
        public PartialViewResult _PartialContactPersonListForShow(string agentInternalId)
        {
            ViewBag.agentInternalId = agentInternalId;
            List<GetContactPersListModel> model = new UserGroupBL().GetContactlistByIdKeyValue(agentInternalId);
            return PartialView(model);
        }


        public ActionResult getExportProductRate(String State)
        {

              DataTable dt = new DataTable();
            try
            {
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();

                ProcedureExecute proc = new ProcedureExecute("PRC_SaleRateLock");
                proc.AddVarcharPara("@Action", 50, "GetImportData");
                proc.AddVarcharPara("@StateID", 10, State);
                dt = proc.GetTable();

                //HttpContext.Current.Session["DownloadData"] = dt;

              //  GridViewExtension.ExportToXlsx(GetShopListTemplateByAreaExcel(dt, ""), dt, true, getXlsExportOptions());
               
            }
            catch { }
           
            return GridViewExtension.ExportToXlsx(GetShopListTemplateByAreaExcel(dt, ""), dt, true, getXlsExportOptions());
        }

        private XlsxExportOptionsEx getXlsExportOptions()
        {
            DevExpress.XtraPrinting.XlsxExportOptionsEx obj = new DevExpress.XtraPrinting.XlsxExportOptionsEx(DevExpress.XtraPrinting.TextExportMode.Text);
            obj.ExportMode = DevExpress.XtraPrinting.XlsxExportMode.SingleFile;
            obj.ExportType = DevExpress.Export.ExportType.WYSIWYG;

            return obj;
        }

        private GridViewSettings GetShopListTemplateByAreaExcel(object datatable, String dates)
        {
            var settings = new GridViewSettings();
            settings.Name = "ProductRate";
            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "ProductRate";
            settings.Name = "ProductRate";

            DataTable dt = (DataTable)datatable;



            foreach (System.Data.DataColumn datacolumn in dt.Columns)
            {
                if (datacolumn.ColumnName == "STATE" || datacolumn.ColumnName == "Code"
                    || datacolumn.ColumnName == "Description" || datacolumn.ColumnName == "PricetoDistributor" || datacolumn.ColumnName == "PricetoRetailer")
                {
                    settings.Columns.Add(column =>
                    {
                        if (datacolumn.ColumnName == "STATE")
                        {
                            column.Caption = "STATE";
                        }
                        else if (datacolumn.ColumnName == "Code")
                        {
                            column.Caption = "Code";
                        }
                        else if (datacolumn.ColumnName == "Description")
                        {
                            column.Caption = "Description";
                        }
                        else if (datacolumn.ColumnName == "PricetoDistributor")
                        {
                            column.Caption = "Price to Distributor";
                        }
                        else if (datacolumn.ColumnName == "PricetoRetailer")
                        {
                            column.Caption = "Price to Retailer";
                        }


                        column.FieldName = datacolumn.ColumnName;

                        if (datacolumn.DataType.FullName == "System.DateTime")
                        {
                            column.PropertiesEdit.DisplayFormatString = "DD-MM-YYYY";
                        }

                    });
                }

            }


            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }
	}
}