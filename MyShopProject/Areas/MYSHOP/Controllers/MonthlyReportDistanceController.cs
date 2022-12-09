using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BusinessLogicLayer;
using SalesmanTrack;
using System.Data;
using UtilityLayer;
using System.Web.Script.Serialization;
using MyShop.Models;
using System.IO;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using BusinessLogicLayer.SalesmanTrack;


namespace MyShop.Areas.MYSHOP.Controllers
{
    public class MonthlyReportDistanceController : Controller
    {
        //
        // GET: /MYSHOP/MonthlyReportDistance/
        UserList lstuser = new UserList();
        Distancereport objshop = new Distancereport();

        DataTable dtuser = new DataTable();

        public ActionResult Distance()
        {
            try
            {
                string userid = Session["userid"].ToString();
                DistanceDate omodel = new DistanceDate();
                omodel.Date = DateTime.Now.ToString("dd-MM-yyyy");
                return View(omodel);
            }

            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
           
        }

        public ActionResult GetDistanceDetails(DistanceReportInput model)
        {
            try
            {
                //model.Month = "2";
                //model.Year = "2018";
                String weburl = System.Configuration.ConfigurationSettings.AppSettings["SiteURL"];
                DistanceReportOutput omel = new DistanceReportOutput();


                DataSet ds = new DataSet();
                string datfrmat = "";
                string dattoat = "";
                string userid = Session["userid"].ToString();
                ds = objshop.GetTotalDistance(model.Month, userid, Int32.Parse(model.Year));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    omel.dates = APIHelperMethods.ToModelList<Dateormatsmontwise>(ds.Tables[0]);
                    omel.distance = APIHelperMethods.ToModelList<DistanceReport>(ds.Tables[1]);
                  //omel.users = APIHelperMethods.ToModelList<Userformats>(ds.Tables[2]);

                    TempData["ExportDistance"] = omel.distance;
                    TempData.Keep();

                }

                return PartialView("_PartialdistanceReport", omel);
                //  return PartialView("_PartialShopListgridview", omel);

            }
            catch
            {

                //   return Redirect("~/OMS/Signoff.aspx");

                return RedirectToAction("Logout", "Login", new { Area = "" });
            }
        }


        public ActionResult ExportDistancelist(int type)
        {
            List<AttendancerecordModel> model = new List<AttendancerecordModel>();
            if (TempData["ExportDistance"] != null)
            {

                switch (type)
                {
                    case 1:
                        return GridViewExtension.ExportToPdf(GetEmployeeBatchGridViewSettings(), TempData["ExportDistance"]);
                    //break;
                    case 2:
                        return GridViewExtension.ExportToXlsx(GetEmployeeBatchGridViewSettings(), TempData["ExportDistance"]);
                    //break;
                    case 3:
                        return GridViewExtension.ExportToXls(GetEmployeeBatchGridViewSettings(), TempData["ExportDistance"]);
                    //break;
                    case 4:
                        return GridViewExtension.ExportToRtf(GetEmployeeBatchGridViewSettings(), TempData["ExportDistance"]);
                    //break;
                    case 5:
                        return GridViewExtension.ExportToCsv(GetEmployeeBatchGridViewSettings(), TempData["ExportDistance"]);
                    default:
                        break;
                }
            }
            return null;
        }

        private GridViewSettings GetEmployeeBatchGridViewSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "Monthly Report Distance";

            settings.SettingsExport.ExportedRowType = GridViewExportedRowType.All;
            settings.SettingsExport.FileName = "Monthly Report Distance";


            settings.Columns.Add(column =>
            {
                column.Caption = "Reporting Name";
                column.FieldName = "ReportName";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "UserName";
                column.FieldName = "UserName";

            });
            settings.Columns.Add(column =>
            {
                column.Caption = "Designation";
                column.FieldName = "Designation";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "1";
                column.FieldName = "Date_1";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "2";

                column.FieldName = "Date_2";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "3";
                column.FieldName = "Date_3";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "4";
                column.FieldName = "Date_4";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "5";
                column.FieldName = "Date_5";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "6";
                column.FieldName = "Date_6";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "7";
                column.FieldName = "Date_7";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "8";
                column.FieldName = "Date_8";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "9";
                column.FieldName = "Date_9";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "10";
                column.FieldName = "Date_10";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "11";
                column.FieldName = "Date_11";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "12";
                column.FieldName = "Date_12";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "13";
                column.FieldName = "Date_13";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "14";
                column.FieldName = "Date_14";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "15";
                column.FieldName = "Date_15";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "16";
                column.FieldName = "Date_16";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "17";
                column.FieldName = "Date_17";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "18";
                column.FieldName = "Date_18";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "19";
                column.FieldName = "Date_19";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });


            settings.Columns.Add(column =>
            {
                column.Caption = "20";
                column.FieldName = "Date_20";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "21";
                column.FieldName = "Date_21";
                column.PropertiesEdit.DisplayFormatString = "0.00";

            });

            settings.Columns.Add(column =>
            {
                column.Caption = "22";
                column.FieldName = "Date_22";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "23";
                column.FieldName = "Date_23";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "24";
                column.FieldName = "Date_24";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "25";
                column.FieldName = "Date_25";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "26";
                column.FieldName = "Date_26";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });


            settings.Columns.Add(column =>
            {
                column.Caption = "27";
                column.FieldName = "Date_27";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "28";
                column.FieldName = "Date_28";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "29";
                column.FieldName = "Date_29";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "30";
                column.FieldName = "Date_30";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "31";
                column.FieldName = "Date_31";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.Columns.Add(column =>
            {
                column.Caption = "Total Driving Distance";
                column.FieldName = "Totaldistancecal";
                column.PropertiesEdit.DisplayFormatString = "0.00";
            });

            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.LeftMargin = 20;
            settings.SettingsExport.RightMargin = 20;
            settings.SettingsExport.TopMargin = 20;
            settings.SettingsExport.BottomMargin = 20;

            return settings;
        }


        #region Driving Distance Points

        public ActionResult GetSalesmanLocation(UserListTrackModel modelinput)
        {
            try
            {
                Trackshop model = new Trackshop();

                string datfrmat = modelinput.Date.Split('-')[2] + '-' + modelinput.Date.Split('-')[1] + '-' + modelinput.Date.Split('-')[0];

                dtuser = lstuser.GetUserLocationList(modelinput.selectedusrid, datfrmat);

                if (dtuser.Rows.Count > 0)
                {
                    model.latlanLogin = Convert.ToString(dtuser.Rows[0]["latlanLogin"]);
                    model.latlanLogout = Convert.ToString(dtuser.Rows[0]["latlanLogout"]);

                    string Pathget = "~\\jsonsresponse\\" + "_" + modelinput.selectedusrid + "_" + datfrmat + ".json";

                    if (System.IO.File.Exists(Server.MapPath(Pathget)))
                    {

                        using (StreamReader r = new StreamReader(Server.MapPath(Pathget)))
                        {
                            string json = r.ReadToEnd().ToString();
                            model.Fullresponse = json;
                        }

                    }

                    DataTable dttrack = lstuser.GetUserLocationTrackList(modelinput.selectedusrid, datfrmat);
                    List<TracksalesmanAreaTrack> lstarea = new List<TracksalesmanAreaTrack>();
                    lstarea = APIHelperMethods.ToModelList<TracksalesmanAreaTrack>(dttrack);
                    model.salesmanarea = lstarea;
                    return Json(model);
                }

                else
                {
                    return Json("nodata");

                }

            }
            catch
            {
                return RedirectToAction("Logout", "Login", new { Area = "" });
            }

        }


        public ActionResult InserSalesmanLocationforMonthlyReport(string locationdetail, string selectedusrid, string Date, string response)
        {

            DataTable dtloaction = new DataTable();

            string currentdate = DateTime.Now.ToString("yyyy-MM-dd");

            string datfrmat = Date.Split('-')[2] + '-' + Date.Split('-')[1] + '-' + Date.Split('-')[0];


            if (currentdate != datfrmat)
            {
                string Pathget = "~\\jsonsresponse\\" + "_" + selectedusrid + "_" + datfrmat + ".json";
                if (System.IO.File.Exists(Pathget))
                {
                    System.IO.File.Delete(Pathget);

                }
                var dir = Server.MapPath("~\\jsonsresponse");
                var file = Path.Combine(dir, "_" + selectedusrid + "_" + datfrmat + ".json");
                Directory.CreateDirectory(dir);

                System.IO.File.WriteAllText(Server.MapPath(Pathget), response);

            }
            dtloaction = CreateTempTable();

            var myList = new List<string>(locationdetail.Split('^'));
            foreach (var points in myList)
            {

                var point = points.Split('|');
                string pointlatlong = point[0].ToString().Replace('(', ' ').Replace(')', ' ');
                string lat = pointlatlong.Split(',')[0].Trim();
                string lng = pointlatlong.Split(',')[1].Trim();
                if (point[1].Contains("km"))
                {
                    dtloaction.Rows.Add(lat, lng, "", "", point[1].Replace("km", "").Trim());

                }
                else
                {
                    decimal poin2 = Convert.ToDecimal(Int32.Parse(point[1].Replace("m", "").Trim()) / 1000.000000);
                    dtloaction.Rows.Add(lat, lng, "", "", poin2);
                }
            }

            Locationdata objlocation = new Locationdata();

            objlocation.LocationDistanceInsertion(dtloaction, selectedusrid, datfrmat);
            return Json("success");

        }

        public DataTable CreateTempTable()
        {

            DataTable dt = new DataTable();


            dt.Columns.Add("Latitude", typeof(string));
            dt.Columns.Add("Longitude", typeof(string));
            dt.Columns.Add("VisitDate", typeof(string));
            dt.Columns.Add("UserId", typeof(string));
            dt.Columns.Add("distanceKm", typeof(string));

            return dt;
        }

        #endregion
    }
}