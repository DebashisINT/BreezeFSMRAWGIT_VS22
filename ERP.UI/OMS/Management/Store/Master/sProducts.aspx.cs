/*****************************************************************************************************************************
 * Rev 1.0     Sanchita    V2.0.38     31-01-2023      An error is showing while saving a new product. Refer: 25631
 * Rev 2.0     Sanchita    V2.0.39     01/03/2023      FSM >> Product Master : Listing - Implement Show Button. Refer: 25709
 * Rev 3.0     Sanchita    V2.0.40     05/05/2023      Downloaded excel showing blank data while exporting the Product Master in FSM Portal
 *                                                      Refer: 26041
 * Rev 4.0     Sanchita    V2.0.43     06/11/2023      On demand search is required in Product Master & Projection Entry. Mantis: 26858    
 * Rev 5.0     Sanchita    V2.0.45     25/01/2024      FSM Product Master - Colour Search - saved colurs not showing ticked. Mantis: 27211    
 * *****************************************************************************************************************************/
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using BusinessLogicLayer;
using System.Web.Services;
using System.Text;
using System.Collections.Generic;
using System.Resources;
using System.Collections;
using System.IO;
using EntityLayer.CommonELS;
using System.Drawing;
using System.Linq;
using System.Configuration;
using DataAccessLayer;
using System.Web.Script.Services;
using System.IO;
using System.Data;
using System.Data.OleDb;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ClsDropDownlistNameSpace;
using ClsDropDownlistNameSpace;
using ERP.Models;

namespace ERP.OMS.Management.Store.Master
{
    public partial class management_master_Store_sProducts : ERP.OMS.ViewState_class.VSPage//System.Web.UI.Page
    {
        public string pageAccess = "";
        //GenericMethod oGenericMethod;
        //DBEngine oDBEngine = new DBEngine(string.Empty);

        BusinessLogicLayer.GenericMethod oGenericMethod;
        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
        public EntityLayer.CommonELS.UserRightsForPage rights = new UserRightsForPage();
        public ProductComponentBL prodComp = new ProductComponentBL();
        BusinessLogicLayer.MasterDataCheckingBL masterChecking = new BusinessLogicLayer.MasterDataCheckingBL();
        BusinessLogicLayer.RemarkCategoryBL reCat = new BusinessLogicLayer.RemarkCategoryBL();
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
                string sPath = Convert.ToString(HttpContext.Current.Request.Url);
                oDBEngine.Call_CheckPageaccessebility(sPath);
            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            tdstcs.ConnectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            SqlClassSource.ConnectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            SqlHSNDataSource.ConnectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            ProductDataSource.ConnectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
            HsnDataSource.ConnectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);

        }
        protected void Page_Load(object sender, EventArgs e)
        {


            rights = BusinessLogicLayer.CommonBLS.CommonBL.GetUserRightSession("/management/store/Master/sProducts.aspx");



            cityGrid.JSProperties["cpinsert"] = null;
            cityGrid.JSProperties["cpEdit"] = null;
            cityGrid.JSProperties["cpUpdate"] = null;
            cityGrid.JSProperties["cpDelete"] = null;
            cityGrid.JSProperties["cpExists"] = null;
            cityGrid.JSProperties["cpUpdateValid"] = null;
            if (HttpContext.Current.Session["userid"] == null)
            {
                //Page.ClientScript.RegisterStartupScript(GetType(), "SighOff", "<script>SignOff();</script>");
            }
            //this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>height();</script>");
            if (!IsPostBack)
            {
                //BindCountry();
                //BindState(1); 
                IsUdfpresent.Value = Convert.ToString(getUdfCount());
                Session["exportval"] = null;
                // Rev 3.0
                Session["Master_ProductDetails"] = null;
                // End of Rev 3.0

                BindProType();
                BindProductSize();
                BindProClassCode();
                BindProductColor();
                BindQuoteCurrency();
                BindTradingLotUnits();
                BindBarCodeType();
                BindTaxCode("S", CmbTaxCodeSale);
                BindTaxCode("P", CmbTaxCodePur);
                BindTaxScheme();
                BindServiceTax();
                BindBrand();
                bindMainAccounts();
                // Mantis Issue 24299
                BindColorNew();
                BindSizeNew();
                BindGenderNew();
                // End of Mantis Issue 24299
                //BindHsnCode();

                // Rev 2.0
                string IsShowProductSearchInMaster = "0";
                DBEngine obj1 = new DBEngine();
                IsShowProductSearchInMaster = Convert.ToString(obj1.GetDataTable("select [value] from FTS_APP_CONFIG_SETTINGS WHERE [Key]='IsShowProductSearchInMaster'").Rows[0][0]);

                if (IsShowProductSearchInMaster == "1")
                {
                    divProd.Visible = true;
                }
                else
                {
                    divProd.Visible = false;
                }
                // End of Rev 2.0
            }
            // Rev 2.0
            //BindGrid();
            // End of Rev 2.0

            //new code block for showing key from resource page start

            if (File.Exists(Server.MapPath("~/Management/DailyTask/ResourceFiles/ProductValues.resx")))
            {
                ResourceReader resReader = new ResourceReader(Server.MapPath("~/Management/DailyTask/ResourceFiles/ProductValues.resx"));

                foreach (DictionaryEntry d in resReader)
                {
                    Label currLBL = new Label();
                    currLBL = (Label)Page.FindControl(Convert.ToString(d.Key));

                    if (currLBL == null) { currLBL = (Label)Popup_Empcitys.FindControl(Convert.ToString(d.Key)); }

                    currLBL.Text = Convert.ToString(d.Value);
                }

                resReader.Close();
            }

            //new code block for showing key from resource page end

            if (!IsPostBack && Request.QueryString["DirectEdit"] != null)
            {
                ScriptManager.RegisterStartupScript(HttpContext.Current.Handler as System.Web.UI.Page, HttpContext.Current.GetType(), "Js1", "fn_Editcity(" + Request.QueryString["DirectEdit"] + ");", true);
            }

        }



        //binding dropdowns start

        protected void BindProType()
        {
            //  / oGenericMethod = new GenericMethod();
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("SELECT name,sProducts_Type FROM ( SELECT 'Raw Material' AS name, 'A' AS sProducts_Type UNION SELECT 'Work-In-Process' AS name, 'B' AS sProducts_Type UNION SELECT 'Finished Goods' AS name, 'C' AS sProducts_Type) X order by sProducts_Type  ");
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbProType, dtCmb, "name", "sProducts_Type", "");
            }

        }


        protected void bindMainAccounts()
        {
            //BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            //DataTable dtCmb = new DataTable();
            //ProcedureExecute proc = new ProcedureExecute("prc_ProductMaster_bindData");
            //proc.AddVarcharPara("@action", 20, "GetMainAccount"); 
            //dtCmb = proc.GetTable();



            //cmbsalesInvoice.DataSource = dtCmb;
            //cmbsalesInvoice.TextField = "MainAccount_Name";
            //cmbsalesInvoice.ValueField = "MainAccount_AccountCode";
            //cmbsalesInvoice.DataBind();

            //cmbPurInvoice.DataSource = dtCmb;
            //cmbPurInvoice.TextField = "MainAccount_Name";
            //cmbPurInvoice.ValueField = "MainAccount_AccountCode";
            //cmbPurInvoice.DataBind();

            //cmbSalesReturn.DataSource = dtCmb;
            //cmbSalesReturn.TextField = "MainAccount_Name";
            //cmbSalesReturn.ValueField = "MainAccount_AccountCode";
            //cmbSalesReturn.DataBind();

            //cmbPurReturn.DataSource = dtCmb;
            //cmbPurReturn.TextField = "MainAccount_Name";
            //cmbPurReturn.ValueField = "MainAccount_AccountCode";
            //cmbPurReturn.DataBind();


        }


        protected void BTNSave_clicked(object sender, EventArgs e)
        {
            string[] key = Convert.ToString(KeyField.Text).Split(',');
            string[] value = Convert.ToString(ValueField.Text).Split(',');
            string RexName = Convert.ToString(RexPageName.Text).Trim();

            if (File.Exists(Server.MapPath("~/Management/DailyTask/ResourceFiles/" + RexName + ".resx")))
            {
                File.Delete(Server.MapPath("~/Management/DailyTask/ResourceFiles/" + RexName + ".resx"));
            }

            ResourceWriter resourceWriter = new ResourceWriter(Server.MapPath("~/Management/DailyTask/ResourceFiles/" + RexName + ".resx"));
            for (int i = 0; i < key.Length; i++)
            {
                resourceWriter.AddResource(key[i].Trim(), value[i].Trim());
            }
            resourceWriter.Generate();
            resourceWriter.Close();

            Response.Redirect("");
        }




        protected void BindProClassCode()
        {
            //  / oGenericMethod = new GenericMethod();
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            // Rev 1.0
            //dtCmb = oGenericMethod.GetDataTable("SELECT ProductClass_ID,ProductClass_Name FROM Master_ProductClass order by ProductClass_Name");
            dtCmb = oGenericMethod.GetDataTable("SELECT ProductClass_ID,trim(ProductClass_Name) ProductClass_Name FROM Master_ProductClass order by ProductClass_Name");
            // End of Rev 1.0
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbProClassCode, dtCmb, "ProductClass_Name", "ProductClass_ID", "");
            }

        }

        //Tax Code bind here Debjyoti 05-01-2017
        protected void BindTaxCode(string taxType, ASPxComboBox cmb)
        {

            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            //dtCmb = oGenericMethod.GetDataTable("select 0  Taxes_ID,'-Select-' Taxes_Name union all select Taxes_ID,Taxes_Name from Master_Taxes where Taxes_ApplicableFor in('B','" + taxType.Trim() + "')");
            dtCmb = oGenericMethod.GetDataTable("select 0  Taxes_SchemeID,'--Select--' Taxes_SchemeName union all select TaxRates_ID,TaxRatesSchemeName from Config_TaxRates ct inner join Master_Taxes mt on ct.TaxRates_TaxCode=mt.Taxes_ID where TaxRates_TaxCode in (select Taxes_ID from Master_Taxes where Taxes_ApplicableFor in ('B','" + taxType + "')) and mt.TaxTypeCode<>'O' order by Taxes_SchemeName");

            AspxHelper oAspxHelper = new AspxHelper();

            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(cmb, dtCmb, "Taxes_SchemeName", "Taxes_SchemeID");
            }



        }

        //tax Scheme bind here debjyoti 05-01-2017

        protected void BindTaxScheme()
        {

            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();

            dtCmb = oGenericMethod.GetDataTable("select 0  TaxRates_ID,'-Select-' TaxRates_Scheme union all select TaxRates_ID,isnull(TaxRatesSchemeName,'') from Config_TaxRates");
            AspxHelper oAspxHelper = new AspxHelper();

            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbTaxScheme, dtCmb, "TaxRates_Scheme", "TaxRates_ID");
            }



        }

        //BarCode tye added here Debjyoti 30-12-2016
        protected void BindBarCodeType()
        {
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("select id,Symbology from tbl_master_BarCodeSymbology where isActive=1");
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbBarCodeType, dtCmb, "Symbology", "id");
            }

        }


        protected void BindHsnCode()
        {
            //BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            //DataTable dtCmb = new DataTable();
            //dtCmb = oGenericMethod.GetDataTable("select HSN_id,Code+'  ['+Description+']' as Description  from  tbl_HSN_Master");
            //aspxHsnCode.DataSource = dtCmb;
            //aspxHsnCode.ValueField = "HSN_id";
            //aspxHsnCode.TextField = "Description";
            //aspxHsnCode.DataBind();

        }


        protected void BindTradingLotUnits()
        {
            //  / oGenericMethod = new GenericMethod();
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("select UOM_ID,UOM_Name from Master_UOM  order by UOM_Name");
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbTradingLotUnits, dtCmb, "UOM_Name", "UOM_ID");

                oAspxHelper.Bind_Combo(CmbQuoteLotUnit, dtCmb, "UOM_Name", "UOM_ID");

                oAspxHelper.Bind_Combo(CmbDeliveryLotUnit, dtCmb, "UOM_Name", "UOM_ID");

                //added for stock uom
                oAspxHelper.Bind_Combo(cmbStockUom, dtCmb, "UOM_Name", "UOM_ID", "");

                //Added for packing uom
                oAspxHelper.Bind_Combo(cmbPackingUom, dtCmb, "UOM_Name", "UOM_ID", "");

            }

        }


        protected void BindQuoteCurrency()
        {
            //  / oGenericMethod = new GenericMethod();
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("select Currency_ID, Currency_Name  from Master_Currency order by Currency_Name");
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbQuoteCurrency, dtCmb, "Currency_Name", "Currency_ID", "");
            }

        }

        protected void BindProductColor()
        {
            //  / oGenericMethod = new GenericMethod();
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            //................CODE UPDATED BY sAM ON 18102016.................................................
            //dtCmb = oGenericMethod.GetDataTable("SELECT [Color_ID],[Color_Name] FROM [dbo].[Master_Color] UNION SELECT 0 AS [Color_ID],'None' AS [Color_Name] UNION SELECT NULL AS [Color_ID],'' AS [Color_Name] ORDER BY [Color_ID]");
            dtCmb = oGenericMethod.GetDataTable("SELECT [Color_ID],[Color_Name] FROM [dbo].[Master_Color] UNION SELECT 0 AS [Color_ID],'Select' AS [Color_Name] ");

            //................CODE ABOVE UPDATED BY sAM ON 18102016.................................................
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbProductColor, dtCmb, "Color_Name", "Color_ID", "");
                CmbProductColor.SelectedIndex = 0;
            }

        }

        protected void BindBrand()
        {
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("select Brand_Id ,Brand_Name from tbl_master_brand where Brand_IsActive=1");

            AspxHelper oAspxHelper = new AspxHelper();
            // Rev 4.0
            //if (dtCmb.Rows.Count > 0)
            //{
            //    oAspxHelper.Bind_Combo(cmbBrand, dtCmb, "Brand_Name", "Brand_Id", "");
            //}
            // End of Rev 4.0

        }



        protected void BindProductSize()
        {
            //  / oGenericMethod = new GenericMethod();
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            //................CODE UPDATED BY sAM ON 18102016.................................................
            //dtCmb = oGenericMethod.GetDataTable("SELECT [Size_ID],[Size_Name] FROM [dbo].[Master_Size] UNION SELECT 0 AS [Size_ID],'None' AS [Size_Name] UNION SELECT NULL AS [Size_ID],'' AS [Size_Name]");
            dtCmb = oGenericMethod.GetDataTable("SELECT [Size_ID],[Size_Name] FROM [dbo].[Master_Size] UNION SELECT 0 AS [Size_ID],'Select' AS [Size_Name]");
            //................CODE aBOVE UPDATED BY sAM ON 18102016.................................................
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                oAspxHelper.Bind_Combo(CmbProductSize, dtCmb, "Size_Name", "Size_ID", "");
                CmbProductSize.SelectedIndex = 0;
            }

        }

        // Mantis Issue 24299
        protected void BindColorNew()
        {
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            clsDropDownList oclsDropDownList = new clsDropDownList();
            string[,] DataColorNew= oDBEngine.GetFieldValue("Master_Color", "Color_ID,Color_Name",null,2,"Color_Name");
            // Rev rev 4.0
            //oclsDropDownList.AddDataToDropDownList(DataColorNew, ddlColorNew);
            // End of Rev rev 4.0
        }
        protected void BindSizeNew()
        {
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            clsDropDownList oclsDropDownList = new clsDropDownList();
            //string[,] DataSizeNew = oDBEngine.GetFieldValue("Master_Size", "Size_ID,Size_Name", "Size_ID IN(40,41,42,43,44)", 2);
            string[,] DataSizeNew = oDBEngine.GetFieldValue("Master_Size", "Size_ID,Size_Name", null, 2);
            oclsDropDownList.AddDataToDropDownList(DataSizeNew, ddlSizeNew);
        }
        protected void BindGenderNew()
        {
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            clsDropDownList oclsDropDownList = new clsDropDownList();
            string[,] DataGenderNew = oDBEngine.GetFieldValue("Master_Gender", "Gender_ID,Gender_Name", null, 2);
            oclsDropDownList.AddDataToDropDownList(DataGenderNew, ddlGenderNew);

        }
        // End of Mantis Issue 24299

        [WebMethod]
        public static bool CheckUniqueName(string ProductName, int procode)
        {
            DataTable dt = new DataTable();
            ProductName = ProductName.Replace("'", "''");
            bool IsPresent = false;
            BusinessLogicLayer.GenericMethod oGeneric = new BusinessLogicLayer.GenericMethod();
            if (procode == 0)
            {
                dt = oGeneric.GetDataTable("SELECT COUNT(sProducts_Code) AS sProducts_Name FROM Master_sProducts WHERE sProducts_Code = '" + ProductName + "'");
            }
            else
            {
                dt = oGeneric.GetDataTable("SELECT COUNT(sProducts_Code) AS sProducts_Name FROM Master_sProducts WHERE sProducts_Code = '" + ProductName + "' and sProducts_ID<>" + procode + "");
            }
            //DataTable dt = oGeneric.GetDataTable("SELECT COUNT(sProducts_Code) AS sProducts_Name FROM Master_sProducts WHERE sProducts_Code = '" + ProductName + "'");

            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["sProducts_Name"]) > 0)
                {
                    IsPresent = true;
                }
            }
            return IsPresent;
        }
        //binding dropdown ends

        protected void BindCountry()
        {
            //  / oGenericMethod = new GenericMethod();
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("select cou_id as id,cou_country as name from tbl_master_country order By cou_country");
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                //oAspxHelper.Bind_Combo(CmbCountryName, dtCmb, "name", "id", "India");
            }

        }

        protected void BindState(int countryID)
        {
            //CmbState.Items.Clear();

            // oGenericMethod = new GenericMethod();
            oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("Select id,state as name From tbl_master_STATE Where countryID=" + countryID + " Order By Name");//+ " Order By state "
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                //CmbState.Enabled = true;
                //oAspxHelper.Bind_Combo(CmbState, dtCmb, "name", "id", 0);
            }
            else
            {
                //CmbState.Enabled = false;
            }
        }
        protected void BindCity(int stateID)
        {
            //CmbCity.Items.Clear();

            // oGenericMethod = new GenericMethod();
            oGenericMethod = new BusinessLogicLayer.GenericMethod();

            DataTable dtCmb = new DataTable();
            dtCmb = oGenericMethod.GetDataTable("Select city_id,city_name From tbl_master_city Where state_id=" + stateID + " Order By city_name");//+ " Order By state "
            AspxHelper oAspxHelper = new AspxHelper();
            if (dtCmb.Rows.Count > 0)
            {
                //CmbState.Enabled = true;
                // oAspxHelper.Bind_Combo(CmbCity, dtCmb, "city_name", "city_id", 0);
            }
            else
            {
                //CmbCity.Enabled = false;
            }
        }

        protected void BindGrid()
        {
            // Rev 2.0
            //Store_MasterBL oStore_MasterBL = new Store_MasterBL();
            //DataTable dtFillGrid = new DataTable();
            //dtFillGrid = oStore_MasterBL.GetsProductList();

            DataTable dtFillGrid = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("FSM_FetchProductsList");
            proc.AddPara("@Products", Convert.ToString(txtProduct_hidden.Value));
            // Rev Sanchita
            proc.AddPara("@User_id", Convert.ToInt32(Session["userid"]));
            // End of Rev Sanchita
            dtFillGrid = proc.GetTable();
            // End of Rev 2.0

            AspxHelper oAspxHelper = new AspxHelper();
            // Rev Sanchita
            //if (dtFillGrid.Rows.Count > 0)
            //{
            //    // Rev 3.0
            //    Session["Master_ProductDetails"] = dtFillGrid;
            //    // End of Rev 3.0
            //    cityGrid.DataSource = dtFillGrid;
            //    cityGrid.DataBind();
            //}
            //else
            //{
            //    // Rev 3.0
            //    Session["Master_ProductDetails"] = null;
            //    // End of Rev 3.0
            //    cityGrid.DataSource = null;
            //    cityGrid.DataBind();
            //}

            cityGrid.JSProperties["cpLoadData"] = "Success";
            // End of Rev Sanchita
        }

        // Rev Sanchita
        protected void EntityServerModelogDataSource_Selecting(object sender, DevExpress.Data.Linq.LinqServerModeDataSourceSelectEventArgs e)
        {
            e.KeyExpression = "sProducts_ID";
            string connectionString = Convert.ToString(System.Web.HttpContext.Current.Session["ErpConnection"]);
             string IsFilter = Convert.ToString(hfIsFilter.Value);
            
            ERPDataClassesDataContext dc1 = new ERPDataClassesDataContext(connectionString);

            if (IsFilter == "Y")
            {
                var q = from d in dc1.FSMProduct_Masters
                        where d.USERID == Convert.ToInt64(HttpContext.Current.Session["userid"].ToString())
                        orderby d.sProducts_ID descending
                        select d;
                e.QueryableSource = q;
            }
            else
            {
                var q = from d in dc1.FSMProduct_Masters
                        where d.sProducts_ID == 0
                        select d;
                e.QueryableSource = q;
            }
        }
        // End of Rev Sanchita


        //bind service tax
        protected void BindServiceTax()
        {
            DataTable serviceTax = oDBEngine.GetDataTable("SELECT TAX_ID,SERVICE_CATEGORY_CODE,SERVICE_TAX_NAME,ACCOUNT_HEAD_TAX_RECEIPTS,ACCOUNT_HEAD_OTHERS_RECEIPTS,ACCOUNT_HEAD_PENALTIES,ACCOUNT_HEAD_DeductRefund FROM TBL_MASTER_SERVICE_TAX");
            AspxServiceTax.DataSource = serviceTax;
            AspxServiceTax.DataBind();
        }

        public void bindexport(int Filter)
        {
            //cityGrid.Columns[6].Visible = false;

            //MainAccountGrid.Columns[20].Visible = false;
            // MainAccountGrid.Columns[21].Visible = false;
            string filename = "Products";
            exporter.FileName = filename;

            exporter.PageHeader.Left = "Products";
            exporter.PageFooter.Center = "[Page # of Pages #]";
            exporter.PageFooter.Right = "[Date Printed]";

            switch (Filter)
            {
                case 1:
                    exporter.WritePdfToResponse();
                    break;
                case 2:
                    exporter.WriteXlsToResponse();
                    break;
                case 3:
                    exporter.WriteRtfToResponse();
                    break;
                case 4:
                    exporter.WriteCsvToResponse();
                    break;
            }
        }
        protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Int32 Filter = int.Parse(Convert.ToString(drdExport.SelectedItem.Value));
            if (Filter != 0)
            {
                // Rev 3.0
                cityGrid.DataSource = Session["Master_ProductDetails"];
                cityGrid.DataBind();
                // End of Rev 3.0

                if (Session["exportval"] == null)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
                else if (Convert.ToInt32(Session["exportval"]) != Filter)
                {
                    Session["exportval"] = Filter;
                    bindexport(Filter);
                }
            }
        }

        protected void btnSearch(object sender, EventArgs e)
        {
            cityGrid.Settings.ShowFilterRow = true;
        }

        protected void cityGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
        {
            if (e.RowType == GridViewRowType.Data)
            {
                int commandColumnIndex = -1;
                for (int i = 0; i < cityGrid.Columns.Count; i++)
                    if (cityGrid.Columns[i] is GridViewCommandColumn)
                    {
                        commandColumnIndex = i;
                        break;
                    }
                if (commandColumnIndex == -1)
                    return;
                //____One colum has been hided so index of command column will be leass by 1 
                commandColumnIndex = commandColumnIndex - 2;
                DevExpress.Web.Rendering.GridViewTableCommandCell cell = e.Row.Cells[commandColumnIndex] as DevExpress.Web.Rendering.GridViewTableCommandCell;
                for (int i = 0; i < cell.Controls.Count; i++)
                {
                    DevExpress.Web.Rendering.GridCommandButtonsCell button = cell.Controls[i] as DevExpress.Web.Rendering.GridCommandButtonsCell;
                    if (button == null) return;
                    DevExpress.Web.Internal.InternalHyperLink hyperlink = button.Controls[0] as DevExpress.Web.Internal.InternalHyperLink;

                    if (hyperlink.Text == "Delete")
                    {
                        if (Convert.ToString(Session["PageAccess"]).Trim() == "DelAdd" || Convert.ToString(Session["PageAccess"]).Trim() == "Delete" || Convert.ToString(Session["PageAccess"]).Trim() == "All")
                        {
                            hyperlink.Enabled = true;
                            continue;
                        }
                        else
                        {
                            hyperlink.Enabled = false;
                            continue;
                        }
                    }
                }
            }
        }

        protected void cityGrid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
        {
            if (!cityGrid.IsNewRowEditing)
            {
                ASPxGridViewTemplateReplacement RT = cityGrid.FindEditFormTemplateControl("UpdateButton") as ASPxGridViewTemplateReplacement;
                if (Convert.ToString(Session["PageAccess"]).Trim() == "Add" || Convert.ToString(Session["PageAccess"]).Trim() == "Modify" || Convert.ToString(Session["PageAccess"]).Trim() == "All")
                    RT.Visible = true;
                else
                    RT.Visible = false;
            }

        }

        protected void cityGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            cityGrid.JSProperties["cpinsert"] = null;
            cityGrid.JSProperties["cpEdit"] = null;
            cityGrid.JSProperties["cpUpdate"] = null;
            cityGrid.JSProperties["cpDelete"] = null;
            cityGrid.JSProperties["cpExists"] = null;
            cityGrid.JSProperties["cpUpdateValid"] = null;

            int insertcount = 0;
            int updtcnt = 0;
            int deletecnt = 0;

            // oGenericMethod = new GenericMethod();
            oGenericMethod = new BusinessLogicLayer.GenericMethod();

            string WhichCall = Convert.ToString(e.Parameters).Split('~')[0];
            string WhichType = null;
            if (Convert.ToString(e.Parameters).Contains("~"))
                if (Convert.ToString(e.Parameters).Split('~')[1] != "")
                    WhichType = Convert.ToString(e.Parameters).Split('~')[1];

            if (e.Parameters == "s")
                cityGrid.Settings.ShowFilterRow = true;
            if (e.Parameters == "All")
                cityGrid.FilterExpression = string.Empty;

            if (WhichCall == "savecity")
            {
                oGenericMethod = new BusinessLogicLayer.GenericMethod();
                Store_MasterBL oStore_MasterBL = new Store_MasterBL();
                int TradingLot = 0;
                int QuoteLot = 0;
                int DeliveryLot = 0;
                int productSize = 0;
                int ProductColor = 0;
                // Mantis Issue 24299
                string productSizeNew = "";
                string productColorNew = "";
                string productGenderNew = "";
                // End of Mantis Issue 24299
                string strMsg = "fail";
                //-----Arindam
                txtQuoteLot.Text = "1";
                txtTradingLot.Text = "1";
                txtDeliveryLot.Text = "1";
                //--Arindam for tryparse
                /*insertcount = oStore_MasterBL.InsertProduct(txtPro_Code.Text, txtPro_Name.Text, txtPro_Description.Text,
                    Convert.ToString(CmbProType.SelectedItem.Value), Convert.ToInt32(CmbProClassCode.SelectedItem.Value), txtGlobalCode.Text,
                    TradingLot, Convert.ToInt32(CmbTradingLotUnits.SelectedItem.Value));*/
if (!string.IsNullOrEmpty(txtPro_Code.Text.Trim()) && !string.IsNullOrEmpty(txtPro_Name.Text.Trim()))
                {

                    if (int.TryParse(txtQuoteLot.Text, out QuoteLot))
                    {
                        if (int.TryParse(txtTradingLot.Text, out TradingLot))
                        {
                            if (int.TryParse(txtDeliveryLot.Text, out DeliveryLot))
                            {
                                //if (CmbProductSize.SelectedItem.Value != "") //28.12.2016 commented by Subhra because it's getting error
                                if (CmbProductSize.Text != "")
                                {
                                    productSize = Convert.ToInt32(CmbProductSize.SelectedItem.Value);
                                }
                                if (CmbProductColor.Text != "")
                                //if (CmbProductColor.SelectedItem.Value != "") //28.12.2016 commented by Subhra because it's getting error
                                {
                                    ProductColor = Convert.ToInt32(CmbProductColor.SelectedItem.Value);
                                }
                                // Mantis Issue 24299
                                if (hdnColorNew.Value.ToString() != "")
                                {
                                    productColorNew = hdnColorNew.Value.ToString();
                                }

                                if (hdnSizeNew.Value.ToString() != "")
                                {
                                    productSizeNew = hdnSizeNew.Value.ToString();
                                }

                                if (hdnGenderNew.Value.ToString() != "")
                                {
                                    productGenderNew = hdnGenderNew.Value.ToString();
                                }
                                // End of Mantis Issue 24299
                                Boolean sizeapplicable = false;
                                Boolean colorapplicable = false;
                                Boolean isInventory = false;
                                Boolean autoApply = false;
                                Boolean isInstall = false;
                                Boolean isOldUnit = false;
                                Boolean IsServiceItem = false;
                                Boolean FurtheranceToBusiness = false;//Subhabrata
                                decimal saleprice = 0;
                                decimal MinSaleprice = 0;
                                decimal purPrice = 0;
                                decimal MRP = 0;
                                // Mantis Issue 25469, 25470
                                decimal Discount = 0;
                                // End of Mantis Issue 25469, 25470
                                decimal minLvl = 0;
                                decimal reorderLvl = 0;
                                decimal reorder_qty = 0;
                                Boolean isCapitalGoods = false;
                                int tdscode = 0;
                                // Rev 4.0
                                int ProductBrand = 0;
                                // End of Rev 4.0

                                if (cmb_tdstcs.Value != null)
                                {
                                    tdscode = Convert.ToInt32(cmb_tdstcs.Value);
                                }

                                if (chkFurtherance.Checked)
                                {
                                    FurtheranceToBusiness = true;
                                }

                                if (ChkAutoApply.Checked)
                                    autoApply = true;

                                if (txtReorderLvl.Text.Trim() != "")
                                {
                                    reorderLvl = Convert.ToDecimal(txtReorderLvl.Text.Trim());
                                }

                                if (txtReorderQty.Text.Trim() != "")
                                {
                                    reorder_qty = Convert.ToDecimal(txtReorderQty.Text.Trim());

                                }

                                if (txtMinLvl.Text.Trim() != "")
                                {
                                    minLvl = Convert.ToDecimal(txtMinLvl.Text.Trim());
                                }

                                if (txtMrp.Text.Trim() != "")
                                {
                                    MRP = Convert.ToDecimal(txtMrp.Text.Trim());
                                }
                                // Mantis Issue 25469, 25470
                                if (txtDiscount.Text.Trim() != "")
                                {
                                    Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
                                }
                                // End of Mantis Issue 25469, 25470

                                if (txtPurPrice.Text.Trim() != "")
                                {
                                    purPrice = Convert.ToDecimal(txtPurPrice.Text.Trim());
                                }

                                if (txtMinSalePrice.Text.Trim() != "")
                                {
                                    MinSaleprice = Convert.ToDecimal(txtMinSalePrice.Text.Trim());
                                }

                                if (txtSalePrice.Text.Trim() != "")
                                {
                                    saleprice = Convert.ToDecimal(txtSalePrice.Text.Trim());
                                }
                                if (rdblappColor.Items[0].Selected)
                                {
                                    colorapplicable = true;
                                }
                                else
                                {
                                    colorapplicable = false;
                                }


                                if (rdblapp.Items[0].Selected)
                                {
                                    sizeapplicable = true;
                                }
                                else
                                {
                                    sizeapplicable = false;
                                }

                                if (Convert.ToString(cmbIsInventory.SelectedItem.Value) == "1")
                                    isInventory = true;

                                //Get Product Componnet details
                                String ProdComponent = "";
                                List<object> ComponentList = GridLookup.GridView.GetSelectedFieldValues("sProducts_ID");
                                foreach (object Pobj in ComponentList)
                                {
                                    ProdComponent += "," + Pobj;
                                }
                                ProdComponent = ProdComponent.TrimStart(',');

                                if (Convert.ToString(aspxInstallation.SelectedItem.Value) == "1")
                                    isInstall = true;

                                if (Convert.ToString(cmbOldUnit.SelectedItem.Value) == "1")
                                    isOldUnit = true;


                                if (Convert.ToString(cmbIsCapitalGoods.SelectedItem.Value) == "1")
                                    isCapitalGoods = true;

                                if (Convert.ToString(cmbServiceItem.SelectedItem.Value) == "1")
                                    IsServiceItem = true;

                                if (!reCat.isAllMandetoryDone((DataTable)Session["UdfDataOnAdd"], "Prd"))
                                {
                                    cityGrid.JSProperties["cpinsert"] = "UDFManddratory";
                                    return;

                                }
                                // Rev 4.0
                                if (hdnBrand_hidden.Value.ToString() != "")
                                {
                                    ProductBrand = Convert.ToInt32(hdnBrand_hidden.Value);
                                }
                                // End of Rev 4.0

                                if (HttpContext.Current.Session["userid"] != null)
                                {

                                    insertcount = oStore_MasterBL.InsertProduct(txtPro_Code.Text, txtPro_Name.Text, txtPro_Description.Text,
                                     Convert.ToString(CmbProType.Value == null ? 0 : CmbProType.SelectedItem.Value), Convert.ToInt32(CmbProClassCode.Value == null ? 0 : CmbProClassCode.SelectedItem.Value), txtGlobalCode.Text,
                                     1, Convert.ToInt32(CmbTradingLotUnits.Value == null ? 0 : CmbTradingLotUnits.SelectedItem.Value),
                                     1, 1, 1, 1, Convert.ToInt32(CmbDeliveryLotUnit.Value == null ? 0 : CmbDeliveryLotUnit.SelectedItem.Value), ProductColor,
                                     productSize, Convert.ToInt32(HttpContext.Current.Session["userid"]), sizeapplicable, colorapplicable,
                                     Convert.ToInt32(CmbBarCodeType.Value == null ? 0 : CmbBarCodeType.SelectedItem.Value), txtBarCodeNo.Text.Trim(),
                                     isInventory, Convert.ToString(CmbStockValuation.SelectedItem.Value), saleprice, MinSaleprice, purPrice, MRP,
                                     Convert.ToInt32(cmbStockUom.Value == null ? 0 : cmbStockUom.SelectedItem.Value), minLvl, reorderLvl,
                                     Convert.ToString(cmbNegativeStk.SelectedItem.Value), Convert.ToInt32(CmbTaxCodeSale.Value == null ? 0 : CmbTaxCodeSale.SelectedItem.Value),
                                     Convert.ToInt32(CmbTaxCodePur.Value == null ? 0 : CmbTaxCodePur.SelectedItem.Value), Convert.ToInt32(CmbTaxScheme.Value == null ? 0 : CmbTaxScheme.SelectedItem.Value),
                                     autoApply, Convert.ToString(fileName.Value), ProdComponent, Convert.ToString(CmbStatus.SelectedItem.Value), Convert.ToString(HsnLookUp.Text).Trim(),
                                     Convert.ToInt32(AspxServiceTax.Value == null ? 0 : AspxServiceTax.Value),
                                     Convert.ToDecimal(txtPackingQty.Text.Trim()), Convert.ToDecimal(txtpacking.Text.Trim()), Convert.ToInt32(cmbPackingUom.Value != null ? cmbPackingUom.Value : 0),
                                     // Rev 4.0
                                     //isInstall, Convert.ToInt32(cmbBrand.Value == null ? 0 : cmbBrand.Value), isCapitalGoods, tdscode, Convert.ToString(Session["LastFinYear"]), isOldUnit,
                                     isInstall, ProductBrand, isCapitalGoods, tdscode, Convert.ToString(Session["LastFinYear"]), isOldUnit,
                                     // End of Rev 4.0
                                     hdnSIMainAccount.Value == null ? "" : Convert.ToString(hdnSIMainAccount.Value), hdnSRMainAccount.Value == null ? "" : Convert.ToString(hdnSRMainAccount.Value),
                                     hdnPIMainAccount.Value == null ? "" : Convert.ToString(hdnPIMainAccount.Value), hdnPRMainAccount.Value == null ? "" : Convert.ToString(hdnPRMainAccount.Value), FurtheranceToBusiness, IsServiceItem, reorder_qty
                                     // Mantis Issue 24299
                                     , productColorNew, productSizeNew, productGenderNew
                                     // End of Mantis Issue 24299
                                     // Mantis Issue 25469, 25470
                                     , Discount
                                     // End of Mantis Issue 25469, 25470
                                     );





                                    //Udf Add mode
                                    DataTable udfTable = (DataTable)Session["UdfDataOnAdd"];
                                    if (udfTable != null)
                                        Session["UdfDataOnAdd"] = reCat.insertRemarksCategoryAddMode("Prd", "ProductMaster" + Convert.ToString(insertcount), udfTable, Convert.ToString(Session["userid"]));

                                    //insertcount = oStore_MasterBL.InsertProduct(txtPro_Code.Text, txtPro_Name.Text, txtPro_Description.Text,
                                    //Convert.ToString(CmbProType.SelectedItem.Value), Convert.ToInt32(CmbProClassCode.SelectedItem.Value), txtGlobalCode.Text,
                                    //TradingLot, Convert.ToInt32(CmbTradingLotUnits.SelectedItem.Value),
                                    //Convert.ToInt32(CmbQuoteCurrency.SelectedItem.Value), QuoteLot,
                                    //Convert.ToInt32(CmbQuoteLotUnit.SelectedItem.Value), DeliveryLot,
                                    //Convert.ToInt32(CmbDeliveryLotUnit.SelectedItem.Value), ProductColor,
                                    //productSize, Convert.ToInt32(HttpContext.Current.Session["userid"]));


                                    strMsg = "Success";
                                    RefereshApplicationProductData();
                                }
                                else
                                {
                                    strMsg = "Your session is end";
                                }

                            }
                            //else
                            //{
                            //    strMsg = "Delivery lot must be numeric value";
                            //}
                        }
                        //else
                        //{
                        //    strMsg = "Trading lot must be numeric value";
                        //}

                    }
                    //else
                    //{
                    //    strMsg = "Quote lot must be numeric value";
                    //}
                }
                //else
                //{
                //    strMsg = "Product Short Name (Unique) and Name is required";
                //}



                if (insertcount > 0)
                {
                    cityGrid.JSProperties["cpinsert"] = "Success";
                    // Rev 2.0
                    //BindGrid();
                    // End of Rev 2.0
                }
                else
                {
                    cityGrid.JSProperties["cpinsert"] = strMsg;
                }
            }
            if (WhichCall == "updatecity")
            {
                Store_MasterBL oStore_MasterBL = new Store_MasterBL();

                Boolean sizeapplicable = false;
                Boolean colorapplicable = false;
                Boolean isInventory = false;
                Boolean autoApply = false;
                Boolean isInstall = false;
                Boolean isOldUnit = false;
                Boolean IsServiceItem = false;
                Boolean FurtheranceToBusiness = false;//Subhabrata
                decimal saleprice = 0;
                decimal MinSaleprice = 0;
                decimal purPrice = 0;
                decimal MRP = 0;
                // Mantis Issue 25469, 25470
                decimal Discount = 0;
                // End of Mantis Issue 25469, 25470
                decimal MinLvl = 0;
                decimal reorderLvl = 0;
                decimal reorder_qty = 0;
                Boolean isCapitalGoods = false;
                int tdscode = 0;
                // Rev 4.0
                int ProductBrand = 0;
                // End of Rev 4.0

                if (chkFurtherance.Checked)
                {
                    FurtheranceToBusiness = true;
                }

                if (cmb_tdstcs.Value != null)
                {
                    tdscode = Convert.ToInt32(cmb_tdstcs.Value);
                }

                if (ChkAutoApply.Checked)
                    autoApply = true;

                if (txtReorderLvl.Text.Trim() != "")
                {
                    reorderLvl = Convert.ToDecimal(txtReorderLvl.Text.Trim());
                }

                if (txtReorderQty.Text.Trim() != "")
                {
                    reorder_qty = Convert.ToDecimal(txtReorderQty.Text.Trim());

                }

                if (txtMinLvl.Text.Trim() != "")
                {
                    MinLvl = Convert.ToDecimal(txtMinLvl.Text.Trim());
                }
                if (txtMrp.Text.Trim() != "")
                {
                    MRP = Convert.ToDecimal(txtMrp.Text.Trim());
                }
                // Mantis Issue 25469, 25470
                if (txtDiscount.Text.Trim() != "")
                {
                    Discount = Convert.ToDecimal(txtDiscount.Text.Trim());
                }
                // End of Mantis Issue 25469, 25470
                if (txtPurPrice.Text.Trim() != "")
                {
                    purPrice = Convert.ToDecimal(txtPurPrice.Text.Trim());
                }

                if (txtMinSalePrice.Text.Trim() != "")
                {
                    MinSaleprice = Convert.ToDecimal(txtMinSalePrice.Text.Trim());
                }

                if (txtSalePrice.Text.Trim() != "")
                {
                    saleprice = Convert.ToDecimal(txtSalePrice.Text.Trim());
                }
                if (rdblappColor.Items[0].Selected)
                {
                    colorapplicable = true;
                }
                else
                {
                    colorapplicable = false;
                }


                if (rdblapp.Items[0].Selected)
                {
                    sizeapplicable = true;
                }
                else
                {
                    sizeapplicable = false;
                }

                if (Convert.ToString(cmbIsInventory.SelectedItem.Value) == "1")
                    isInventory = true;

                if (Convert.ToString(cmbServiceItem.SelectedItem.Value) == "1")
                    IsServiceItem = true;

                if (Convert.ToString(cmbOldUnit.SelectedItem.Value) == "1")
                    isOldUnit = true;
                //Get Product Componnet details for update 
                String ProdComponent = "";
                List<object> ComponentList = GridLookup.GridView.GetSelectedFieldValues("sProducts_ID");
                foreach (object Pobj in ComponentList)
                {
                    ProdComponent += "," + Pobj;
                }
                ProdComponent = ProdComponent.TrimStart(',');

                //  Delete Existing file in Update

                string[] filePath = oDBEngine.GetFieldValue1("Master_sProducts", "sProduct_ImagePath", "sProducts_ID=" + WhichType, 1);
                if (filePath[0] != "")
                {
                    if (filePath[0].Trim() != fileName.Value.Trim())
                    {
                        if ((System.IO.File.Exists(Server.MapPath(filePath[0]))))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath[0]));
                        }
                    }
                }
                //

                // Mantis Issue 24299
                string productSizeNew = "";
                string productColorNew = "";
                string productGenderNew = "";

                if (hdnColorNew.Value.ToString() != "")
                {
                    productColorNew = hdnColorNew.Value.ToString();
                }

                if (hdnSizeNew.Value.ToString() != "")
                {
                    productSizeNew = hdnSizeNew.Value.ToString();
                }

                if (hdnGenderNew.Value.ToString() != "")
                {
                    productGenderNew = hdnGenderNew.Value.ToString();
                }
                // End of Mantis Issue 24299

                if (Convert.ToString(aspxInstallation.SelectedItem.Value) == "1")
                    isInstall = true;

                if (Convert.ToString(cmbIsCapitalGoods.SelectedItem.Value) == "1")
                    isCapitalGoods = true;

                // Rev 4.0
                if (hdnBrand_hidden.Value.ToString() != "")
                {
                    ProductBrand = Convert.ToInt32(hdnBrand_hidden.Value);
                }
                // End of Rev 4.0

                updtcnt = oStore_MasterBL.UpdateProduct(Convert.ToInt32(WhichType), txtPro_Code.Text, txtPro_Name.Text, txtPro_Description.Text, Convert.ToString(CmbProType.SelectedItem == null ? 0 : CmbProType.SelectedItem.Value),
                 Convert.ToInt32(CmbProClassCode.SelectedItem == null ? 0 : CmbProClassCode.SelectedItem.Value), txtGlobalCode.Text, 1, Convert.ToInt32(CmbTradingLotUnits.SelectedItem == null ? 0 : CmbTradingLotUnits.SelectedItem.Value),
                 1, 1, 1, 1,
                 Convert.ToInt32(CmbDeliveryLotUnit.SelectedItem == null ? 0 : CmbDeliveryLotUnit.SelectedItem.Value),
                 Convert.ToInt32(CmbProductColor.SelectedItem == null ? 0 : CmbProductColor.SelectedItem.Value),
                 Convert.ToInt32(CmbProductSize.SelectedItem == null ? 0 : CmbProductSize.SelectedItem.Value),
                 Convert.ToInt32(HttpContext.Current.Session["userid"]), sizeapplicable, colorapplicable,
                 Convert.ToInt32(CmbBarCodeType.SelectedItem == null ? 0 : CmbBarCodeType.SelectedItem.Value), txtBarCodeNo.Text.Trim(),
                 isInventory, Convert.ToString(CmbStockValuation.SelectedItem == null ? "" : CmbStockValuation.SelectedItem.Value), saleprice, MinSaleprice, purPrice, MRP,
                 Convert.ToInt32(cmbStockUom.SelectedItem == null ? 0 : cmbStockUom.SelectedItem.Value), MinLvl, reorderLvl,
                 Convert.ToString(cmbNegativeStk.SelectedItem.Value), Convert.ToInt32(CmbTaxCodeSale.SelectedItem == null ? 0 : CmbTaxCodeSale.SelectedItem.Value),
                 Convert.ToInt32(CmbTaxCodePur.SelectedItem == null ? 0 : CmbTaxCodePur.SelectedItem.Value), Convert.ToInt32(CmbTaxScheme.SelectedItem == null ? 0 : CmbTaxScheme.SelectedItem.Value),
                 autoApply, Convert.ToString(fileName.Value), ProdComponent, Convert.ToString(CmbStatus.SelectedItem.Value), Convert.ToString(HsnLookUp.Text).Trim(),
                 Convert.ToInt32(AspxServiceTax.Value == null ? 0 : AspxServiceTax.Value),
                 Convert.ToDecimal(txtPackingQty.Text.Trim()), Convert.ToDecimal(txtpacking.Text.Trim()), Convert.ToInt32(cmbPackingUom.Value != null ? cmbPackingUom.Value : 0),
                 // Rev 4.0
                 //isInstall, Convert.ToInt32(cmbBrand.Value == null ? 0 : cmbBrand.Value), isCapitalGoods, tdscode, isOldUnit,
                 isInstall, ProductBrand, isCapitalGoods, tdscode, isOldUnit,
                 // End of Rev 4.0
                 hdnSIMainAccount.Value == null ? "" : Convert.ToString(hdnSIMainAccount.Value), hdnSRMainAccount.Value == null ? "" : Convert.ToString(hdnSRMainAccount.Value),
                 hdnPIMainAccount.Value == null ? "" : Convert.ToString(hdnPIMainAccount.Value), hdnPRMainAccount.Value == null ? "" : Convert.ToString(hdnPRMainAccount.Value), FurtheranceToBusiness, IsServiceItem, reorder_qty
                 // Mantis Issue 24299
                , productColorNew, productSizeNew, productGenderNew
                // End of Mantis Issue 24299
                // Mantis Issue 25469, 25470
                , Discount
                // End of Mantis Issue 25469, 25470
                 );
                //updtcnt = oStore_MasterBL.UpdateProduct(Convert.ToInt32(WhichType), txtPro_Code.Text, txtPro_Name.Text, txtPro_Description.Text, Convert.ToString(CmbProType.SelectedItem == null ? 0 : CmbProType.SelectedItem.Value),
                //   Convert.ToInt32(CmbProClassCode.SelectedItem == null ? 0 : CmbProClassCode.SelectedItem.Value), txtGlobalCode.Text, Convert.ToInt32(txtTradingLot.Text), Convert.ToInt32(CmbTradingLotUnits.SelectedItem == null ? 0 : CmbTradingLotUnits.SelectedItem.Value),
                //   Convert.ToInt32(CmbQuoteCurrency.SelectedItem == null ? 0 : CmbQuoteCurrency.SelectedItem.Value), Convert.ToInt32(txtQuoteLot.Text), Convert.ToInt32(CmbQuoteLotUnit.SelectedItem == null ? 0 : CmbQuoteLotUnit.SelectedItem.Value), Convert.ToInt32(txtDeliveryLot.Text),
                //   Convert.ToInt32(CmbDeliveryLotUnit.SelectedItem == null ? 0 : CmbDeliveryLotUnit.SelectedItem.Value), Convert.ToInt32(CmbProductColor.SelectedItem == null ? 0 : CmbProductColor.SelectedItem.Value), Convert.ToInt32(CmbProductSize.SelectedItem == null ? 0 : CmbProductSize.SelectedItem.Value), Convert.ToInt32(HttpContext.Current.Session["userid"]));

                if (updtcnt > 0)
                {
                    cityGrid.JSProperties["cpUpdate"] = "Success";
                    RefereshApplicationProductData();
                    // Rev 2.0
                    //BindGrid();
                    // End of Rev 2.0
                }
                else
                {
                    cityGrid.JSProperties["cpUpdate"] = "fail";
                }


            }
            if (WhichCall == "Delete")
            {
                int checkinvalue = masterChecking.DeleteProduct(Convert.ToInt32(WhichType));
                if (checkinvalue > 0)
                {


                    string[] filePath = oDBEngine.GetFieldValue1("Master_sProducts", "sProduct_ImagePath", "sProducts_ID=" + WhichType, 1);
                    if (filePath[0] != "")
                    {
                        if ((System.IO.File.Exists(Server.MapPath(filePath[0]))))
                        {
                            System.IO.File.Delete(Server.MapPath(filePath[0]));
                        }
                    }

                    oGenericMethod.Delete_Table("tbl_master_product_packingDetails", "packing_sProductId=" + WhichType + "");
                    // Mantis Issue 24299
                    oGenericMethod.Delete_Table("Mapping_ProductGender", "Products_ID=" + WhichType + "");
                    oGenericMethod.Delete_Table("Mapping_ProductColor", "Products_ID=" + WhichType + "");
                    oGenericMethod.Delete_Table("Mapping_ProductSize", "Products_ID=" + WhichType + "");
                    // End of Mantis Issue 24299
                    deletecnt = oGenericMethod.Delete_Table("Master_sProducts", "sProducts_ID=" + WhichType + "");
                    if (deletecnt > 0)
                    {
                        deletecnt = oGenericMethod.Delete_Table("tbl_master_ProdComponent", "Product_id=" + WhichType + "");
                        cityGrid.JSProperties["cpDelete"] = "Success";
                        RefereshApplicationProductData();
                        BindGrid();
                    }
                    else
                        cityGrid.JSProperties["cpDelete"] = "Fail";

                }
                else
                {
                    if (checkinvalue == -2)
                    {
                        cityGrid.JSProperties["cpDelete"] = "Fail";
                        cityGrid.JSProperties["cpErrormsg"] = "Transaction exists. Cannot delete.";
                    }
                    else if (checkinvalue == -3)
                    {
                        string UsedProductList = "";
                        DataTable dt = oDBEngine.GetDataTable("select (select sproducts_name from Master_sProducts where sProducts_ID=h.Product_id) as Product_idName,Product_id, (select sproducts_name from Master_sProducts where sProducts_ID=h.Component_prodId) as Component_prodIdName,Component_prodId from tbl_master_ProdComponent h  where h.Component_prodId=" + WhichType + " or Product_id=" + WhichType + "");
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                if (Convert.ToString(dr["Component_prodId"]).Trim() == WhichType)
                                {
                                    UsedProductList += ", " + Convert.ToString(dr["Product_idName"]);
                                }
                                else
                                {
                                    UsedProductList += ", " + Convert.ToString(dr["Component_prodIdName"]);
                                }
                            }
                            cityGrid.JSProperties["cpDelete"] = "Fail";
                            cityGrid.JSProperties["cpErrormsg"] = "This Product associated with " + UsedProductList.TrimStart(',') + ". \n Cannot delete.";
                        }
                    }
                    else
                    {
                        cityGrid.JSProperties["cpDelete"] = "Fail";
                        cityGrid.JSProperties["cpErrormsg"] = "Product is in use. Cannot delete.";
                    }
                }
            }
            if (WhichCall == "Edit")
            {
              
                Store_MasterBL oStore_MasterBL = new Store_MasterBL();
                Session["KeyVal_InternalID"] = "ProductMaster" + Convert.ToString(WhichType).Trim();
                DataTable dtEdit = oStore_MasterBL.GetProductDetails(Convert.ToInt32(WhichType));

                cityGrid.JSProperties["cpMainAccountInUse"] = oStore_MasterBL.getUsedMainAccountByProductId(Convert.ToInt32(WhichType));

                if (dtEdit.Rows.Count > 0 && dtEdit != null)
                {
                    string sProducts_ID = Convert.ToString(dtEdit.Rows[0]["sProducts_ID"]);
                    string sProducts_Code = Convert.ToString(dtEdit.Rows[0]["sProducts_Code"]);
                    string sProducts_Name = Convert.ToString(dtEdit.Rows[0]["sProducts_Name"]);
                    string sProducts_Description = Convert.ToString(dtEdit.Rows[0]["sProducts_Description"]);
                    string sProducts_Type = (Convert.ToString(dtEdit.Rows[0]["sProducts_Type"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProducts_Type"]));
                    string sProducts_TypeFull = Convert.ToString(dtEdit.Rows[0]["sProducts_TypeFull"]);
                    string ProductClass_Code = (Convert.ToString(dtEdit.Rows[0]["ProductClass_Code"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["ProductClass_Code"]));
                    string sProducts_GlobalCode = Convert.ToString(dtEdit.Rows[0]["sProducts_GlobalCode"]);
                    string sProducts_TradingLot = Convert.ToString(dtEdit.Rows[0]["sProducts_TradingLot"]);
                    string sProducts_TradingLotUnit = (Convert.ToString(dtEdit.Rows[0]["sProducts_TradingLotUnit"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProducts_TradingLotUnit"]));
                    string sProducts_QuoteCurrency = (Convert.ToString(dtEdit.Rows[0]["sProducts_QuoteCurrency"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProducts_QuoteCurrency"]));
                    string sProducts_QuoteLot = Convert.ToString(dtEdit.Rows[0]["sProducts_QuoteLot"]);
                    string sProducts_QuoteLotUnit = (Convert.ToString(dtEdit.Rows[0]["sProducts_QuoteLotUnit"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProducts_QuoteLotUnit"]));
                    string sProducts_DeliveryLot = Convert.ToString(dtEdit.Rows[0]["sProducts_DeliveryLot"]);
                    string sProducts_DeliveryLotUnit = (Convert.ToString(dtEdit.Rows[0]["sProducts_DeliveryLotUnit"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProducts_DeliveryLotUnit"]));
                    string sProducts_Color = Convert.ToString(dtEdit.Rows[0]["sProducts_Color"]);
                    string sProducts_Size = Convert.ToString(dtEdit.Rows[0]["sProducts_Size"]);
                    string sProducts_CreateUser = Convert.ToString(dtEdit.Rows[0]["sProducts_CreateUser"]);
                    string sProducts_CreateTime = Convert.ToString(dtEdit.Rows[0]["sProducts_CreateTime"]);
                    string sProducts_ModifyUser = Convert.ToString(dtEdit.Rows[0]["sProducts_ModifyUser"]);
                    string sProducts_ModifyTime = Convert.ToString(dtEdit.Rows[0]["sProducts_ModifyTime"]);
                    //.................Code  Added By Sam on 25102016....................................................
                    string sProducts_SizeApplicable = Convert.ToString(dtEdit.Rows[0]["sProducts_SizeApplicable"]);
                    string sProducts_ColorApplicable = Convert.ToString(dtEdit.Rows[0]["sProducts_ColorApplicable"]);
                    //.................Code Above Added By Sam on 25102016....................................................

                    //............Code Added by Debjyoti 30-12-2016
                    string sProducts_barCodeType = Convert.ToString(dtEdit.Rows[0]["sProducts_barCodeType"]);
                    string sProducts_barCode = Convert.ToString(dtEdit.Rows[0]["sProducts_barCode"]);

                    //--------------Code added by Debjyoti 04-01-2017
                    string sProduct_IsInventory = Convert.ToString(dtEdit.Rows[0]["sProduct_IsInventory"]);
                    string sFurtheranceToBusiness = Convert.ToString(dtEdit.Rows[0]["FurtheranceToBusiness"]);//Subhabrata
                    string stkValuation = Convert.ToString(dtEdit.Rows[0]["sProduct_Stockvaluation"]);
                    string sProduct_SalePrice = Convert.ToString(dtEdit.Rows[0]["sProduct_SalePrice"]);
                    string sProduct_MinSalePrice = Convert.ToString(dtEdit.Rows[0]["sProduct_MinSalePrice"]);
                    string sProduct_PurPrice = Convert.ToString(dtEdit.Rows[0]["sProduct_PurPrice"]);
                    string sProduct_MRP = Convert.ToString(dtEdit.Rows[0]["sProduct_MRP"]);
                    string sProduct_StockUOM = (Convert.ToString(dtEdit.Rows[0]["sProduct_StockUOM"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProduct_StockUOM"]));
                    string sProduct_MinLvl = (Convert.ToString(dtEdit.Rows[0]["sProduct_MinLvl"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProduct_MinLvl"]));
                    string sProduct_reOrderLvl = (Convert.ToString(dtEdit.Rows[0]["sProduct_reOrderLvl"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["sProduct_reOrderLvl"]));
                    string sProduct_NegativeStock = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sProduct_NegativeStock"]) == "" ? "W" : dtEdit.Rows[0]["sProduct_NegativeStock"]);
                    string sProduct_TaxSchemeSale = Convert.ToString(dtEdit.Rows[0]["sProduct_TaxSchemeSale"]);
                    string sProduct_TaxSchemePur = Convert.ToString(dtEdit.Rows[0]["sProduct_TaxSchemePur"]);
                    string sProduct_TaxScheme = Convert.ToString(dtEdit.Rows[0]["sProduct_TaxScheme"]);
                    string sProduct_AutoApply = Convert.ToString(dtEdit.Rows[0]["sProduct_AutoApply"]);



                    string sProduct_ImagePath = Convert.ToString(dtEdit.Rows[0]["sProduct_ImagePath"]);
                    string sproductcomponent = Convert.ToString(dtEdit.Rows[0]["ProductComponent"]).Trim();
                    sproductcomponent = sproductcomponent.TrimStart(',');
                    string sProduct_Status = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sProduct_Status"]) == "" ? "A" : dtEdit.Rows[0]["sProduct_Status"]);
                    //Packing details
                    string sProduct_quantity = Convert.ToString(dtEdit.Rows[0]["sProduct_quantity"]);
                    // string sProduct_quantity = "";
                     string packing_quantity = Convert.ToString(dtEdit.Rows[0]["packing_quantity"]);
                     // string packing_quantity = "";
                     string packing_saleUOM = Convert.ToString(dtEdit.Rows[0]["packing_saleUOM"]);

                     ///string packing_saleUOM = "";
                    if (string.IsNullOrEmpty(sproductcomponent))
                    {

                        sproductcomponent = "N";
                    }

                    string sProducts_HsnCode = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sProducts_HsnCode"]) == "0" ? "" : dtEdit.Rows[0]["sProducts_HsnCode"]);
                    string sProducts_serviceTax = Convert.ToString(dtEdit.Rows[0]["sProducts_serviceTax"]);

                    string sProducts_isInstall = Convert.ToString(dtEdit.Rows[0]["sProducts_isInstall"]);
                    string sProducts_Brand = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sProducts_Brand"]) == "0" ? "" : dtEdit.Rows[0]["sProducts_Brand"]);

                    string sProduct_IsCapitalGoods = Convert.ToString(dtEdit.Rows[0]["sProduct_IsCapitalGoods"]);
                    string Is_ServiceItem = Convert.ToString(dtEdit.Rows[0]["Is_ServiceItem"]);

                    //string TdsTcs = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["TDSTCS_ID"]) == "0" ? "" : dtEdit.Rows[0]["TDSTCS_ID"]);
                    string TdsTcs = "";

                    string sProducts_IsOldUnit = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sProducts_IsOldUnit"]) == "0" ? "" : dtEdit.Rows[0]["sProducts_IsOldUnit"]);

                    string sInv_MainAccount = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sInv_MainAccount"]) == "0" ? "" : dtEdit.Rows[0]["sInv_MainAccount"]);
                    string sRet_MainAccount = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sRet_MainAccount"]) == "0" ? "" : dtEdit.Rows[0]["sRet_MainAccount"]);
                    string pInv_MainAccount = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["pInv_MainAccount"]) == "0" ? "" : dtEdit.Rows[0]["pInv_MainAccount"]);
                    string pRet_MainAccount = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["pRet_MainAccount"]) == "0" ? "" : dtEdit.Rows[0]["pRet_MainAccount"]);


                    string sInv_MainAccount_name = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sInv_MainAccount_name"]) == "" ? "" : dtEdit.Rows[0]["sInv_MainAccount_name"]);
                    string sRet_MainAccount_name = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["sRet_MainAccount_name"]) == "" ? "" : dtEdit.Rows[0]["sRet_MainAccount_name"]);
                    string pInv_MainAccount_name = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["pInv_MainAccount_name"]) == "" ? "" : dtEdit.Rows[0]["pInv_MainAccount_name"]);
                    string pRet_MainAccount_name = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["pRet_MainAccount_name"]) == "" ? "" : dtEdit.Rows[0]["pRet_MainAccount_name"]);


                    string MASIExist = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["MASIExists"]) == "" ? "0" : dtEdit.Rows[0]["MASIExists"]);
                    string MASRExist = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["MASRExists"]) == "" ? "0" : dtEdit.Rows[0]["MASRExists"]);
                    string MAPIExist = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["MAPIExists"]) == "" ? "0" : dtEdit.Rows[0]["MAPIExists"]);
                    string MAPRExist = Convert.ToString(Convert.ToString(dtEdit.Rows[0]["MAPRExists"]) == "" ? "0" : dtEdit.Rows[0]["MAPRExists"]);

                    /* code added by Arindam*/
                    string reorder_qty = (Convert.ToString(dtEdit.Rows[0]["Reorder_Quantity"]) == "0" ? "" : Convert.ToString(dtEdit.Rows[0]["Reorder_Quantity"]));
                    /* code added by Arindam*/

                    // Mantis Issue 24299
                    string ColorNew = Convert.ToString(dtEdit.Rows[0]["ColorNew"]);
                    string SizeNew = Convert.ToString(dtEdit.Rows[0]["SizeNew"]);
                    string GenderNew = Convert.ToString(dtEdit.Rows[0]["GenderNew"]);
                    // End of Mantis Issue 24299
                    // Mantis Issue 25469, 25470
                    string sProducts_Discount = Convert.ToString(dtEdit.Rows[0]["sProducts_Discount"]);
                    // End of Mantis Issue 25469, 25470
                    // Rev rev 4.0
                    string ColorNew_Desc = Convert.ToString(dtEdit.Rows[0]["ColorNew_Desc"]);
                    string Brand_Desc = Convert.ToString(dtEdit.Rows[0]["Brand_Desc"]);
                    // End of Rev rev 4.0

                    // Rev 5.0
                    DataTable dtColorNew = oDBEngine.GetDataTable("Master_Color", " convert(varchar(100),Color_ID) as Id, Color_Name as Name  ", " Color_ID in (select Color_ID from Mapping_ProductColor where Products_ID='" + Convert.ToInt32(WhichType) + "')");

                    System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                    List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                    Dictionary<string, object> row;
                    foreach (DataRow dr in dtColorNew.Rows)
                    {
                        row = new Dictionary<string, object>();
                        foreach (DataColumn col in dtColorNew.Columns)
                        {
                            row.Add(col.ColumnName, dr[col]);
                        }
                        rows.Add(row);
                    }
                    string str_jsonColor = serializer.Serialize(rows);
                    // End of Rev 5.0

                    cityGrid.JSProperties["cpEdit"] = sProducts_ID + "~"
                                                    + sProducts_Code + "~"
                                                    + sProducts_Name + "~"
                                                    + sProducts_Description + "~"
                                                    + sProducts_Type + "~"
                                                    + sProducts_TypeFull + "~"
                                                    + ProductClass_Code + "~"
                                                    + sProducts_GlobalCode + "~"
                                                    + sProducts_TradingLot + "~"
                                                    + sProducts_TradingLotUnit + "~"
                                                    + sProducts_QuoteCurrency + "~"
                                                    + sProducts_QuoteLot + "~"
                                                    + sProducts_QuoteLotUnit + "~"
                                                    + sProducts_DeliveryLot + "~"
                                                    + sProducts_DeliveryLotUnit + "~"
                                                    + sProducts_Color + "~"
                                                    + sProducts_Size + "~"
                        //.................Code  Added By Sam on 25102016....................................................
                        ////+ WhichType;
                                                    + WhichType + "~"
                                                    + sProducts_SizeApplicable + "~"
                                                    + sProducts_ColorApplicable + "~"
                        //.................Code Above Added By Sam on 25102016....................................................

                    //------------------------Code added by Debjyoti 30-12-2016
                        // + sProducts_ColorApplicable;
                                                    + sProducts_barCodeType + "~"
                                                    + sProducts_barCode + "~"
                        //------------------------Code added by Debjyoti 30-12-2016
                        // + sProducts_barCode;
                                                    + sProduct_IsInventory + "~"
                                                    + stkValuation + "~"
                                                    + sProduct_SalePrice + "~"
                                                    + sProduct_MinSalePrice + "~"
                                                    + sProduct_PurPrice + "~"
                                                    + sProduct_MRP + "~"
                                                    + sProduct_StockUOM + "~"
                                                    + sProduct_MinLvl + "~"
                                                    + sProduct_reOrderLvl + "~"
                                                    + sProduct_NegativeStock + "~"
                                                    + sProduct_TaxSchemeSale + "~"
                                                    + sProduct_TaxSchemePur + "~"
                                                    + sProduct_TaxScheme + "~"
                                                    + sProduct_AutoApply + "~"
                                                    + sProduct_ImagePath + "~"
                                                    + sproductcomponent + "~"
                                                    + sProduct_Status + "~"
                                                    + sProducts_HsnCode + "~"
                                                    + sProducts_serviceTax + "~"
                                                    + sProduct_quantity + "~"
                                                    + packing_quantity + "~"
                                                    + packing_saleUOM + "~"
                                                    + sProducts_isInstall + "~"
                                                    + sProducts_Brand + "~"
                                                    + sProduct_IsCapitalGoods + "~"
                                                    + TdsTcs + "~"
                                                    + sProducts_IsOldUnit + "~"
                                                    + sInv_MainAccount + "~"
                                                    + sRet_MainAccount + "~"
                                                    + pInv_MainAccount + "~"
                                                    + pRet_MainAccount + "~"
                                                    + sFurtheranceToBusiness + "~"
                                                    + Is_ServiceItem + "~"
                                                    + sInv_MainAccount_name + "~"
                                                    + sRet_MainAccount_name + "~"
                                                    + pInv_MainAccount_name + "~"
                                                    + pRet_MainAccount_name + "~"
                                                    + MASIExist + "~"
                                                    + MASRExist + "~"
                                                    + MAPIExist + "~"
                                                    + MAPRExist + "~"
                                                    + reorder_qty
                                                    // Mantis Issue 24299
                                                    + "~" + ColorNew + "~"
                                                    + SizeNew + "~"
                                                    + GenderNew
                                                    // End of Mantis Issue 24299
                                                    // Mantis Issue 25469, 25470
                                                    + "~" + sProducts_Discount
                                                    // End of Mantis Issue 25469, 25470
                                                    // Rev rev 4.0
                                                    + "~" + ColorNew_Desc
                                                    + "~" + Brand_Desc
                                                    // End of Rev rev 4.0
                                                    // Rev 5.0
                                                    + "~" + str_jsonColor
                                                    // End of Rev 5.0
                                                        ;
                }
               
            }
            // Rev 2.0
            if (WhichCall == "Show")
            {
                BindGrid();
            }
            // End of Rev 2.0
        }
        public void RefereshApplicationProductData()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_POS_PRODUCTLIST");
            dt = proc.GetTable();
            Application.Remove("POSPRODUCTLISTDATA");
            Application.Add("POSPRODUCTLISTDATA", dt);
        }
        protected void CmbState_Callback(object source, CallbackEventArgsBase e)
        {
            string WhichCall = e.Parameter.Split('~')[0];
            if (WhichCall == "BindState")
            {
                int countryID = Convert.ToInt32(Convert.ToString(e.Parameter.Split('~')[1]));
                BindState(countryID);
            }
        }

        protected void CmbCity_Callback(object source, CallbackEventArgsBase e)
        {
            string WhichCall = e.Parameter.Split('~')[0];
            if (WhichCall == "BindCity")
            {
                int countryID = Convert.ToInt32(Convert.ToString(e.Parameter.Split('~')[1]));
                BindCity(countryID);
            }
        }

        [WebMethod]
        public static bool CheckUniqueCode(string MarketsCode)
        {
            bool flag = false;
            BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();
            try
            {
                DataTable dtCmb = new DataTable();
                dtCmb = oGenericMethod.GetDataTable("SELECT * FROM [dbo].[Master_Markets] WHERE [Markets_Code] = " + "'" + MarketsCode + "'");
                int cnt = dtCmb.Rows.Count;
                if (cnt > 0)
                {
                    flag = true;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
            return flag;
        }

        protected void ASPxCallback1_Callback(object sender, CallbackEventArgsBase e)
        {
            ASPxCallbackPanel sendrPanel = sender as ASPxCallbackPanel;
            FileUpload fp = (FileUpload)sendrPanel.FindControl("FileUpload1");
            //   fp.SaveAs(Server.MapPath("~/OMS/") + fp.PostedFile..FileName);
        }
        protected void ASPxUploadControl1_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            ASPxUploadControl uploader = sender as ASPxUploadControl;
            string fileName = uploader.FileName;
            string name = fileName.Substring(0, fileName.IndexOf('.'));
            string exten = fileName.Substring(fileName.IndexOf('.'), fileName.Length - fileName.IndexOf('.'));


            string ProductFilePath = "/CommonFolderErpCRM/ProductImages/" + name + Guid.NewGuid() + exten;
            uploader.SaveAs(Server.MapPath(ProductFilePath));
            e.CallbackData = ProductFilePath;


        }

        protected void Component_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {
            string componet = Convert.ToString(e.Parameter);
            ASPxCallbackPanel cbPanl = source as ASPxCallbackPanel;
            ASPxGridLookup LookUp = (ASPxGridLookup)cbPanl.FindControl("GridLookup");

            string[] eachComponet = componet.Split(',');
            LookUp.GridView.Selection.UnselectAll();
            LookUp.GridView.Selection.BeginSelection();

            foreach (string val in eachComponet)
            {
                LookUp.GridView.Selection.SelectRowByKey(val);
            }
            LookUp.GridView.Selection.EndSelection();
        }

        protected int getUdfCount()
        {
            DataTable udfCount = oDBEngine.GetDataTable("select 1 from tbl_master_remarksCategory rc where cat_applicablefor='Prd' and ( exists (select * from tbl_master_udfGroup where id=rc.cat_group_id and grp_isVisible=1) or rc.cat_group_id=0)");
            return udfCount.Rows.Count;
        }

        protected void SetHSnPanel_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        {

            string ClassId = e.Parameter;
            DataTable dt = oDBEngine.GetDataTable("select isnull(ProductClass_HSNCode,'') ProductClass_HSNCode  from Master_ProductClass where ProductClass_ID=" + ClassId);
            if (dt.Rows.Count > 0)
            {



                if (Convert.ToString(dt.Rows[0]["ProductClass_HSNCode"]) != "")
                {
                    HsnLookUp.GridView.Selection.SelectRowByKey(Convert.ToString(dt.Rows[0]["ProductClass_HSNCode"]));
                    SetHSnPanel.JSProperties["cpHsnCode"] = Convert.ToString(dt.Rows[0]["ProductClass_HSNCode"]);
                    HsnLookUp.ClientEnabled = false;
                }
                else
                {
                    HsnLookUp.GridView.Selection.SelectRowByKey("");
                    HsnLookUp.ClientEnabled = true;
                }
            }

        }

        // Rev rev 4.0
        public class ColorNew
        {
            // Rev 5.0
            //public string Color_ID { get; set; }
            //public string Color_Name { get; set; }
            public string id { get; set; }
            public string Name { get; set; }
            // End of Rev 5.0
        }

        [WebMethod(EnableSession = true)]
        public static object GetOnDemandColorNew(string SearchKey)
        {
            List<ColorNew> listColorNew = new List<ColorNew>();
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_ProductNameSearch");
            proc.AddPara("@USER_ID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@Action", "SearchByColor");
            proc.AddPara("@SearchKey", SearchKey);
            dt = proc.GetTable();

            foreach (DataRow dr in dt.Rows)
            {
                ColorNew lColor = new ColorNew();
                // Rev 5.0
                //lColor.Color_ID = dr["Color_ID"].ToString();
                //lColor.Color_Name = dr["Color_Name"].ToString();
                lColor.id = dr["Color_ID"].ToString();
                lColor.Name = dr["Color_Name"].ToString();
                // End of Rev 5.0
                listColorNew.Add(lColor);
            }
            return listColorNew;
        }

        public class Brand
        {
            public string Brand_Id { get; set; }
            public string Brand_Name { get; set; }
        }

        [WebMethod(EnableSession = true)]
        public static object GetOnProductBrand(string reqStr)
        {
            List<Brand> listBrand = new List<Brand>();
            BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationSettings.AppSettings["DBConnectionDefault"]);
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("PRC_ProductNameSearch");
            proc.AddPara("@USER_ID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.AddPara("@Action", "SearchByBrand");
            proc.AddPara("@SearchKey", reqStr);
            dt = proc.GetTable();

            foreach (DataRow dr in dt.Rows)
            {
                Brand lBrand = new Brand();
                lBrand.Brand_Id = dr["Brand_Id"].ToString();
                lBrand.Brand_Name = dr["Brand_Name"].ToString();
                listBrand.Add(lBrand);
            }
            return listBrand;
        }
        // End of Rev rev 4.0

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object GetMainAccount(string SearchKey)
        {
            List<MainAccount> listMainAccount = new List<MainAccount>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                //BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);MULTI
                BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine();
                DataTable MainAccount = oDBEngine.GetDataTable("Select top 10 * from (select '' MainAccount_AccountCode,'--Select--' MainAccount_Name union all  select MainAccount_AccountCode,MainAccount_Name from Master_MainAccount where MainAccount_BankCashType not in ('Cash','Bank') and " +
                                                               "MainAccount_AccountCode not in (select distinct SubAccount_MainAcReferenceID from Master_SubAccount where SubAccount_MainAcReferenceID is not null) " +
                                                               "and MainAccount_AccountCode not like 'SYSTM%' and MainAccount_AccountCode like '%" + SearchKey + "%' or MainAccount_Name like '%" + SearchKey + "%' ) TblMA  order by Len(MainAccount_AccountCode) ");


                listMainAccount = (from DataRow dr in MainAccount.Rows
                                   select new MainAccount()
                                   {
                                       MainAccount_Name = Convert.ToString(dr["MainAccount_Name"]),
                                       MainAccount_AccountCode = Convert.ToString(dr["MainAccount_AccountCode"]),

                                   }).ToList();
            }

            return listMainAccount;
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            string FileName = "ProductsList.xlsx";
            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.ClearContent();
            response.Clear();
            response.ContentType = "image/jpeg";
            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ";");
            response.TransmitFile(Server.MapPath("~/DownloadProsuctsSKU/SKU List.xlsx"));
            response.Flush();
            response.End();
        }


        protected void ImportExcel(object sender, EventArgs e)
        {

            if (fileprod.HasFile)
            {
                string FileName = Path.GetFileName(fileprod.PostedFile.FileName);
                string Extension = Path.GetExtension(fileprod.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath("~/Temporary/") + FileName;
                fileprod.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension, "No");

                File.Delete(FilePath);

            }
            BindProClassCode();
            BindBrand();
            // Rev 2.0
            //BindGrid();
            // End of Rev 2.0
            BindProductSize();
            // Mantis Issue 24299
            BindColorNew();
            BindSizeNew();
            BindGenderNew();
            // End of Mantis Issue 24299

            productlog.DataSource = null;
            productlog.DataBind();

            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "script", "PopupproductHide();", true);

        }

        private string GetValue(SpreadsheetDocument doc, Cell cell)
        {
            string value = cell.CellValue.InnerText;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return doc.WorkbookPart.SharedStringTablePart.SharedStringTable.ChildElements.GetItem(int.Parse(value)).InnerText;
            }
            return value;
        }


        public void Import_To_Grid(string FilePath, string Extension, string isHDR)
        {


            if (fileprod.FileName.Trim() != "")
            {

                string fileName = Path.GetFileName(fileprod.PostedFile.FileName);

                string extention = fileName.Substring(fileName.IndexOf('.'), fileName.Length - fileName.IndexOf('.'));
                extention = extention.TrimStart('.');
                extention = extention.ToUpper();



                if (extention == "XLS" || extention == "XLSX")
                {
                    fileName = fileName.Replace(fileName.Substring(0, fileName.IndexOf('.')), "Productupload");

                    DataTable dt = new DataTable();

                    using (SpreadsheetDocument doc = SpreadsheetDocument.Open(FilePath, false))
                    {
                        Sheet sheet = doc.WorkbookPart.Workbook.Sheets.GetFirstChild<Sheet>();

                        Worksheet worksheet = (doc.WorkbookPart.GetPartById(sheet.Id.Value) as WorksheetPart).Worksheet;

                        IEnumerable<Row> rows = worksheet.GetFirstChild<SheetData>().Descendants<Row>().DefaultIfEmpty();



                        foreach (Row row in rows)
                        {
                            if (row.RowIndex.Value == 1)
                            {
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    if (cell.CellValue != null)
                                    {
                                        dt.Columns.Add(GetValue(doc, cell));
                                    }
                                }
                            }
                            else
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (Cell cell in row.Descendants<Cell>())
                                {
                                    if (cell.CellValue != null)
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = GetValue(doc, cell);
                                    }
                                    i++;
                                }
                            }
                        }

                    }

                    //DataTable dtprod = new DataTable();
                    //dtprod.Columns.Add("Product_CODE");
                    //dtprod.Columns.Add("Product_NAME");
                    //dtprod.Columns.Add("Product_CLASS");
                    //dtprod.Columns.Add("Product_BRAND");
                    //dtprod.Columns.Add("Product_SIZE");

                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    if (Convert.ToString(dt.Rows[i][1]) != "")
                    //    {
                    //        dtprod.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][3], dt.Rows[i][2], dt.Rows[i][4]);
                    //    }
                    //}


                    DataTable dtCmb = new DataTable();
                    ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ProductBUlimport");
                    proc.AddPara("@PRODUCTIMPORT", dt);
                    proc.AddPara("@user_Id", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                    dtCmb = proc.GetTable();

                    productlog.DataSource = dtCmb;
                    productlog.DataBind();

                    Session["Datlog"] = dtCmb;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), UniqueID, "jAlert('invalid File')", true);
                }
            }


            //string conStr = "";

            //switch (Extension)
            //{

            //    case ".xls": //Excel 97-03

            //        conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]

            //                 .ConnectionString;

            //        break;

            //    case ".xlsx": //Excel 07

            //        conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]

            //                  .ConnectionString;

            //        break;

            //}

            //conStr = String.Format(conStr, FilePath, isHDR);

            //OleDbConnection connExcel = new OleDbConnection(conStr);

            //OleDbCommand cmdExcel = new OleDbCommand();

            //OleDbDataAdapter oda = new OleDbDataAdapter();

            //DataTable dt = new DataTable();

            //cmdExcel.Connection = connExcel;



            ////Get the name of First Sheet

            //connExcel.Open();

            //DataTable dtExcelSchema;

            //dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            //string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();

            //connExcel.Close();

            ////Read Data from First Sheet

            //connExcel.Open();
            //cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            //oda.SelectCommand = cmdExcel;
            //oda.Fill(dt);
                
            //connExcel.Close();
                 
            //DataTable dtprod = new DataTable();
            //dtprod.Columns.Add("Product_CODE");
            //dtprod.Columns.Add("Product_NAME");
            //dtprod.Columns.Add("Product_CLASS");
            //dtprod.Columns.Add("Product_BRAND");
            //dtprod.Columns.Add("Product_SIZE");

            //for(int i=1;i<dt.Rows.Count;i++)
            //{
            //    if (Convert.ToString(dt.Rows[i][1]) != "")
            //    {
            //        dtprod.Rows.Add(dt.Rows[i][0], dt.Rows[i][1], dt.Rows[i][3], dt.Rows[i][2], dt.Rows[i][4]);
            //    }
            //}

            
            //DataTable dtCmb = new DataTable();
            //ProcedureExecute proc = new ProcedureExecute("Proc_FTS_ProductBUlimport");
            //proc.AddPara("@PRODUCTIMPORT", dtprod);
            //proc.AddPara("@user_Id", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            //dtCmb = proc.GetTable();

            //grdproductlog.DataSource = dtCmb;
            //grdproductlog.DataBind();
            //Bind Data to GridView
          
            //RefereshApplicationProductData();
          
      }


        protected void ShowGrid1_DataBinding(object sender, EventArgs e)
        {
            if (Session["Datlog"] != null)
            {
                productlog.DataSource = (DataTable)Session["Datlog"];
            
            }
        }

        // Rev 2.0
        public class ProductModel
        {
            public int sProducts_ID { get; set; }
            public string sProducts_Code { get; set; }
            public string sProducts_Name { get; set; }
            //public string sProducts_Description { get; set; }
        }

        [WebMethod]
        public static object GetOnDemandProduct(string SearchKey)
        {
            List<ProductModel> listProduct = new List<ProductModel>();
            if (HttpContext.Current.Session["userid"] != null)
            {
                SearchKey = SearchKey.Replace("'", "''");
                DataTable dt = new DataTable();
                ProcedureExecute proc = new ProcedureExecute("PRC_ProductNameSearch");
                proc.AddPara("@USER_ID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
                proc.AddPara("@SearchKey", SearchKey);
                dt = proc.GetTable();

                listProduct = (from DataRow dr in dt.Rows
                               select new ProductModel()
                               {
                                   sProducts_ID = Convert.ToInt32(dr["sProducts_ID"]),
                                   sProducts_Code = Convert.ToString(dr["sProducts_Code"]),
                                   sProducts_Name = Convert.ToString(dr["sProducts_Name"])
                                   //sProducts_Description = Convert.ToString(dr["sProducts_Description"])
                               }).ToList();
            }

            return listProduct;
        }
        // End of Rev 2.0

    }

    class MainAccount
    {
        public string MainAccount_AccountCode { get; set; }
        public string MainAccount_Name { get; set; }
    }



















    //using System;
    //using System.Web;
    //using DevExpress.Web;
    //using BusinessLogicLayer;
    //using System.Data;
    //using System.Web.UI;
    //////using DevExpress.Web.ASPxClasses;
    //using System.Web.Services;
    //using System.Text;

    //public partial class management_master_Store_sMarkets : System.Web.UI.Page
    //{
    //    public string pageAccess = "";
    //  //  DBEngine oDBEngine = new DBEngine(string.Empty);
    //    BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(string.Empty);
    //    BusinessLogicLayer.GenericMethod oGenericMethod;
    //    protected void Page_PreInit(object sender, EventArgs e)
    //    {
    //        if (!IsPostBack)
    //        {
    //            //'http://localhost:2957/InfluxCRM/management/testProjectMainPage_employee.aspx'
    //            string sPath = HttpContext.Current.Request.Url.ToString();
    //            oDBEngine.Call_CheckPageaccessebility(sPath);
    //        }
    //    }
    //    protected void Page_Load(object sender, EventArgs e)
    //    {
    //        //this.Page.ClientScript.RegisterStartupScript(GetType(), "heightL", "<script>height();</script>");
    //    }
    //    protected void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
    //    {
    //        Int32 Filter = int.Parse(cmbExport.SelectedItem.Value.ToString());
    //        switch (Filter)
    //        {
    //            case 1:
    //                exporter.WritePdfToResponse();
    //                break;
    //            case 2:
    //                exporter.WriteXlsToResponse();
    //                break;
    //            case 3:
    //                exporter.WriteRtfToResponse();
    //                break;
    //            case 4:
    //                exporter.WriteCsvToResponse();
    //                break;
    //        }
    //    }
    //    protected void marketsGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    //    {
    //        if (e.RowType == GridViewRowType.Data)
    //        {
    //            int commandColumnIndex = -1;
    //            for (int i = 0; i < marketsGrid.Columns.Count; i++)
    //                if (marketsGrid.Columns[i] is GridViewCommandColumn)
    //                {
    //                    commandColumnIndex = i;
    //                    break;
    //                }
    //            if (commandColumnIndex == -1)
    //                return;
    //            //____One colum has been hided so index of command column will be leass by 1 
    //            commandColumnIndex = commandColumnIndex - 5;
    //            DevExpress.Web.Rendering.GridViewTableCommandCell cell = e.Row.Cells[commandColumnIndex] as DevExpress.Web.Rendering.GridViewTableCommandCell;
    //            for (int i = 0; i < cell.Controls.Count; i++)
    //            {
    //                DevExpress.Web.Rendering.GridCommandButtonsCell button = cell.Controls[i] as DevExpress.Web.Rendering.GridCommandButtonsCell;
    //                if (button == null) return;
    //                DevExpress.Web.Internal.InternalHyperLink hyperlink = button.Controls[0] as DevExpress.Web.Internal.InternalHyperLink;

    //                if (hyperlink.Text == "Delete")
    //                {
    //                    if (Session["PageAccess"].ToString() == "DelAdd" || Session["PageAccess"].ToString() == "Delete" || Session["PageAccess"].ToString() == "All")
    //                    {
    //                        hyperlink.Enabled = true;
    //                        continue;
    //                    }
    //                    else
    //                    {
    //                        hyperlink.Enabled = false;
    //                        continue;
    //                    }
    //                }


    //            }

    //        }

    //    }
    //    protected void marketsGrid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    //    {
    //        if (!marketsGrid.IsNewRowEditing)
    //        {
    //            ASPxGridViewTemplateReplacement RT = marketsGrid.FindEditFormTemplateControl("UpdateButton") as ASPxGridViewTemplateReplacement;
    //            if (Session["PageAccess"].ToString().Trim() == "Add" || Session["PageAccess"].ToString().Trim() == "Modify" || Session["PageAccess"].ToString().Trim() == "All")
    //                RT.Visible = true;
    //            else
    //                RT.Visible = false;
    //        }

    //    }
    //    protected void marketsGrid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    //    {
    //        if (e.Parameters == "s")
    //            marketsGrid.Settings.ShowFilterRow = true;

    //        if (e.Parameters == "All")
    //        {
    //            marketsGrid.FilterExpression = string.Empty;
    //        }
    //    }

    //     [WebMethod]

    //    public static string GetStateListBycountryId(string countryid)
    //    {
    //        StringBuilder strStates = new StringBuilder();
    //        BusinessLogicLayer.GenericMethod oGenericMethod = new BusinessLogicLayer.GenericMethod();
    //        try
    //        {
    //            DataTable dtCmb = new DataTable();
    //            dtCmb = oGenericMethod.GetDataTable("select [State_ID],[State_Name] from Master_States ms  inner join tbl_master_country tmc on tmc.cou_id=ms.[State_CountryID]  where tmc.[cou_country]=LTRIM (RTRIM (" + countryid + ")) order by State_Name");
    //            int cnt = dtCmb.Rows.Count;
    //            if (dtCmb.Rows.Count>0)
    //            {
    //                int i = 0;
    //                strStates.Append("[");
    //                foreach (DataRow dr in dtCmb.Rows)
    //                {
    //                    if (i == cnt - 1)
    //                    {
    //                        strStates.Append("{");
    //                        strStates.Append("\"statename\":\"" + dr["State_Name"] + "\",");
    //                        strStates.Append("\"ID\":\"" + dr["State_ID"] + "\"");
    //                        strStates.Append("}");
    //                    }
    //                    else
    //                    {
    //                        strStates.Append("{");
    //                        strStates.Append("\"statename\":\"" + dr["State_Name"] + "\",");
    //                        strStates.Append("\"ID\":\"" + dr["State_ID"] + "\"");
    //                        strStates.Append("},");
    //                    }
    //                    i++;
    //                }
    //            }
    //            strStates.Append("]");
    //        }
    //        catch (Exception ex)
    //        {
    //        }
    //        finally
    //        {
    //        }

    //        return strStates.ToString();
    //    }
    //}

       
}