using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Web;
using System.Configuration;

namespace BusinessLogicLayer
{
    public class PosSalesInvoiceBl
    {
        public DataTable GetBasketDetails(string branchList)
        {
            ProcedureExecute proc;
            DataTable basketTable = new DataTable();
            try
            {


                using (proc = new ProcedureExecute("prc_getBasketDetail"))
                {
                    //  int i = proc.RunActionQuery();
                    proc.AddVarcharPara("@branchHierchy", 1000, branchList);
                    basketTable = proc.GetTable();

                    return basketTable;

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }
        }


        public int GetWaitingCount(string branchList)
        {
            ProcedureExecute proc;
            DataTable basketWaitingTable = new DataTable();
            int count = 0;
            try
            {


                using (proc = new ProcedureExecute("prc_posListingDetails"))
                {

                    proc.AddVarcharPara("@Action", 50, "GetWaitingCount");
                    proc.AddVarcharPara("@BranchList", 1000, branchList);
                    basketWaitingTable = proc.GetTable();
                    if (basketWaitingTable.Rows.Count > 0)
                    {
                        count = Convert.ToInt32(basketWaitingTable.Rows[0][0]);
                    }
                    return count;

                }
            }

            catch (Exception ex)
            {
                return count;
            }

            finally
            {
                proc = null;
            }

        }


        public DataSet GetBusketDetailsById(int id)
        {
            DataSet basketDetails = new DataSet();
            ProcedureExecute proc;
            try
            {


                using (proc = new ProcedureExecute("prc_PosSalesInvoice"))
                {
                    proc.AddVarcharPara("@action", 50, "GetbasketDetails");
                    proc.AddIntegerPara("@id", id);
                    basketDetails = proc.GetDataSet();
                    return basketDetails;

                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                proc = null;
            }

        }

        public DataTable GetInvoiceListGridData(string userbranchlist, string lastCompany)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "GetQuotationListGridData");
            proc.AddVarcharPara("@BranchList", 500, userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetInvoiceListGridDataByDate(string userbranchlist, string lastCompany, string fromdate, string todate, string branch)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_PosListGridData");
            proc.AddVarcharPara("@BranchList", 500, userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@fromdate", 50, fromdate);
            proc.AddVarcharPara("@todate", 50, todate);
            proc.AddVarcharPara("@branchId", 50, branch);
            dt = proc.GetTable();
            return dt;
        }


        public DataTable GetISTInvoiceListGridDataByDate(string userbranchlist, string lastCompany, string fromdate, string todate, string branch)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "GetISTListGridDataByDate");
            proc.AddVarcharPara("@BranchList", 500, userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@fromdate", 50, fromdate);
            proc.AddVarcharPara("@todate", 50, todate);
            proc.AddVarcharPara("@branchId", 50, branch);
            dt = proc.GetTable();
            return dt;
        }


        public DataTable GetDuplicateInvoiceListGridDataByDate(string userbranchlist, string lastCompany, string fromdate, string todate, string branch)
        {

            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "GetDuplicateQuotationListGridDataByDate");
            proc.AddVarcharPara("@BranchList", 500, userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@fromdate", 50, fromdate);
            proc.AddVarcharPara("@todate", 50, todate);
            proc.AddVarcharPara("@branchId", 50, branch);
            dt = proc.GetTable();
            return dt;
        }

        public DataSet GetAllDropDownDetailForSalesInvoice(string userbranch, string CompanyID, string BranchID)
        {
            string prodLoad = "Y", CustLoad = "Y";
            //if (HttpContext.Current.Session["ProductDetailsListPOS"] != null)
            //{
            //    prodLoad = "N";
            //}
            //if (HttpContext.Current.Session["CustomerDetailsListPOS"] != null)
            //{
            //    CustLoad = "N";
            //}

            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "GetAllDropDownDetailForSalesQuotation");
            proc.AddVarcharPara("@BranchList", 4000, userbranch);
            proc.AddVarcharPara("@CompanyID", 100, CompanyID);
            proc.AddVarcharPara("@BranchID", 4000, BranchID);
            proc.AddVarcharPara("@ShouldLoadProduct", 5, prodLoad);
            proc.AddVarcharPara("@ShouldLoadCustomer", 5, CustLoad);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable PopulateCustomer()
        {
            DataTable customerTable = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_POSCRMSalesInvoice_Details");
            proc.AddVarcharPara("@Action", 100, "PopulateCustomerDetail");
            customerTable = proc.GetTable();
            return customerTable;
        }

        public DataSet GetExecutive(string InternalId)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "GetExecutive");
            proc.AddVarcharPara("@cnt_internalId ", 100, InternalId);
            ds = proc.GetDataSet();
            return ds;
        }

        public DataTable IsLedgerExistsForFinancer(string InternalId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "GetFinancerLedger");
            proc.AddVarcharPara("@cnt_internalId ", 100, InternalId);
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetInvoiceEditData(string InvoiceID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_POSCRMSalesInvoice_Details");
            proc.AddVarcharPara("@Action", 500, "InvoiceEditDetails");
            proc.AddVarcharPara("@InvoiceID", 500, InvoiceID);
            dt = proc.GetTable();
            return dt;
        }


        public DataTable GetOldUnitDetails(string invoiceId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_POSCRMSalesInvoice_Details");
            proc.AddVarcharPara("@Action", 500, "GetOldUnitDetails");
            proc.AddVarcharPara("@InvoiceID", 500, invoiceId);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetCustomerReceiptDetails(string cnt_internalId, string FinYear, string CompanyID, string posInvoiceDtae, string BranchId, string HSNList)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@action", 500, "CustomerReceiptDetails");
            proc.AddVarcharPara("@CompanyID", 100, CompanyID);
            proc.AddVarcharPara("@cnt_internalId", 100, cnt_internalId);
            proc.AddVarcharPara("@FinYear", 100, FinYear);
            proc.AddVarcharPara("@posInvoiceDtae", 10, posInvoiceDtae);
            proc.AddIntegerPara("@userBranch", Convert.ToInt32(BranchId));
            proc.AddVarcharPara("@HSNlist", 2000, HSNList);
            dt = proc.GetTable();
            return dt;
        }

        public DataTable GetCustomerTotalAmountOnSingleDay(string cnt_internalId, string posInvoiceDtae, int branchId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("Prc_GetCustomerTotalAmountOnSingleDay");
            proc.AddVarcharPara("@action", 100, "GetCustomerTotalAmt");
            proc.AddVarcharPara("@doc_date", 30, posInvoiceDtae);
            proc.AddVarcharPara("@customer_id", 10, cnt_internalId);
            proc.AddIntegerPara("@branchId", branchId);
            ds = proc.GetTable();
            return ds;

        }

        public DataTable GetCustomerReceiptTotalAmount(string customerReceipt)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@action", 500, "GetTotalCustomerReceiptAmount");
            proc.AddVarcharPara("@customerReceiptList", 2000, customerReceipt);
            dt = proc.GetTable();
            return dt;

        }
        public DataTable GetCustomerReceiptDetailsByInvoiceId(string InvoiceId)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@action", 500, "GetCustRecByInvoiceId");
            proc.AddIntegerPara("@id", Convert.ToInt32(InvoiceId));
            dt = proc.GetTable();
            return dt;
        }


        public DataTable getBranchListByBranchList(string userbranchhierchy, string userBranch)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "getBranchList");
            proc.AddVarcharPara("@BranchList", 1000, userbranchhierchy);
            proc.AddIntegerPara("@branch", Convert.ToInt32(userBranch));
            ds = proc.GetTable();
            return ds;
        }

        public DataTable getBranchListByBranchListForMassBranch(string userbranchhierchy, string userBranch)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "getBranchListforMassBranch");
            proc.AddVarcharPara("@BranchList", 1000, userbranchhierchy);
            proc.AddIntegerPara("@branch", Convert.ToInt32(userBranch));
            ds = proc.GetTable();
            return ds;
        }

        public DataTable getBranchListByHierchy(string userbranchhierchy)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "getBranchListbyHierchy");
            proc.AddVarcharPara("@BranchList", 1000, userbranchhierchy);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable getBankForManualBRS(string userbranchhierchy)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("GetBankForBRS");
            //proc.AddVarcharPara("@Action", 100, "getBranchListbyHierchy");
            proc.AddVarcharPara("@CompanyId", 1000, userbranchhierchy);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetBranchAssignmentDetails(int InvoiceId, string companyId, string finYear, int branchId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_branchAssignmentFetch");
            proc.AddVarcharPara("@companyId", 100, companyId);
            proc.AddVarcharPara("@finYear", 50, finYear);
            proc.AddIntegerPara("@invoiceId", InvoiceId);
            proc.AddIntegerPara("@branchId", branchId);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetProductActualStock(int branchId, string ProdId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_GetProductActualStock");
            proc.AddVarcharPara("@companyId", 100, Convert.ToString(HttpContext.Current.Session["LastCompany"]));
            proc.AddVarcharPara("@finYear", 50, Convert.ToString(HttpContext.Current.Session["LastFinYear"]));
            proc.AddIntegerPara("@branchId", branchId);
            proc.AddVarcharPara("@prodId", 10, ProdId);
            ds = proc.GetTable();
            return ds;
        }


        public DataTable getWareHouseByBranch(int userbranch)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "GetWareHouseByBranch");
            proc.AddIntegerPara("@branch", userbranch);
            ds = proc.GetTable();
            return ds;
        }
         
        public DataTable UpdateAssignBranch(int pos_assignBranch, int pos_wareHouse, int Invoice_Id)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "AssignBranchTo");
            proc.AddIntegerPara("@pos_assignBranch", pos_assignBranch);
            proc.AddIntegerPara("@pos_wareHouse", pos_wareHouse);
            proc.AddIntegerPara("@Invoice_Id", Invoice_Id);
            proc.AddIntegerPara("@UserID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            ds = proc.GetTable();
            return ds;
        }


        public DataTable GetComponent(string Customer, string Date, string ComponentType, string FinYear, string BranchID, string Action, string strInvoiceID, string branchHierchy)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("_p_POSTagging_Details");
            proc.AddVarcharPara("@Action", 500, Action);
            proc.AddVarcharPara("@CustomerID", 500, Customer);
            proc.AddDateTimePara("@Date", Convert.ToDateTime(Date));
            proc.AddVarcharPara("@ComponentType", 10, ComponentType);
            proc.AddVarcharPara("@FinYear", 10, FinYear);
            proc.AddVarcharPara("@BranchID", 10, BranchID);
            proc.AddVarcharPara("@InvoiceID", 20, strInvoiceID);
            proc.AddVarcharPara("@branchlist", 500, branchHierchy);
            dt = proc.GetTable();
            return dt;
        }


        public void DeleteBasketDetailsFromtable(string basketId, int userId)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "DeleteBasketDetails");
            proc.AddIntegerPara("@id", Convert.ToInt32(basketId));
            proc.AddIntegerPara("@UserID", userId);
            proc.GetTable();

        }


        public int DeleteInvoice(string Invoiceid)
        {
            int i;
            int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("prc_DeletePOSWithAdjustment");
            proc.AddVarcharPara("@invoiceId", 10, Invoiceid);
            proc.AddVarcharPara("@ReturnValue", 50, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValue"));
            return rtrnvalue;

        }

        public DataTable IsMinSalePriceOk(string ProductList)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "IsMinSalePriceOk");
            proc.AddVarcharPara("@ProductSalePriceList", 5000, ProductList);
            DataTable ReturnTable = proc.GetTable();
            return ReturnTable;
        }


        public DataTable GetMassbranchPosDetails(string userbranchlist, string lastCompany)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "GetGridMassBranchDetails");
            proc.AddVarcharPara("@BranchList", 500, userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            dt = proc.GetTable();
            return dt;


        }

        public void SetMassAssignBranch(int InvoiceId, int BranchId)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "SetMassAssignBranch");
            proc.AddIntegerPara("@InvoiceId", InvoiceId);
            proc.AddIntegerPara("@AssignBranchId", BranchId);

            proc.RunActionQuery();
        }

        public void CancelBranchAssignment(int InvoiceId)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "CancelBranchAssignment");
            proc.AddIntegerPara("@InvoiceId", InvoiceId);

            proc.RunActionQuery();
        }

        public DataTable GetCustomerReceipttable(string BranchList)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "GetCustomerReceiptData");
            proc.AddVarcharPara("@BranchList", 1000, BranchList);
            DataTable ReturnTable = proc.GetTable();
            return ReturnTable;
        }

        public DataTable GetCustomerReceipttableByDateBranch(string userbranchlist, string lastCompany, string fromdate, string todate, string branch)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_posListingDetails");
            proc.AddVarcharPara("@Action", 500, "GetCustomerReceiptDataByBranch");
            proc.AddVarcharPara("@BranchList", 500, userbranchlist);
            proc.AddVarcharPara("@CompanyID", 50, lastCompany);
            proc.AddVarcharPara("@fromdate", 50, fromdate);
            proc.AddVarcharPara("@todate", 50, todate);
            proc.AddVarcharPara("@branchId", 50, branch);
            DataTable ReturnTable = proc.GetTable();
            return ReturnTable;
        }

        public DataTable GetSalesmanByBranch(string BranchID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "GetSalesmanByBranch");
            proc.AddVarcharPara("@branch", 4000, BranchID);
            ds = proc.GetTable();
            return ds;
        }

        public DataTable GetLinkedProductList(string Action, string ProductID)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_POSCRMSalesInvoice_Details");
            proc.AddVarcharPara("@Action", 500, Action);
            proc.AddVarcharPara("@ProductID", 2000, ProductID);
            dt = proc.GetTable();
            return dt;
        }


        public DataSet GetBasketDetailsOnly(int id)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "GetBasketDetailsOnly");
            proc.AddIntegerPara("@id", id);
            ds = proc.GetDataSet();
            return ds;
        }

        public string getProductIsInventoryExists(string ProductId)
        {
            string IsInventory = string.Empty;
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("GetIsInventoryFlagByProductID");
            proc.AddVarcharPara("@ProductId", 500, ProductId);
            dt = proc.GetTable();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (Convert.ToString(dt.Rows[0]["sProduct_IsInventory"]).ToUpper() == "TRUE")
                {
                    IsInventory = "Y";
                }
                else
                {
                    IsInventory = "N";
                }
            }
            return IsInventory;
        }

        public string GetAvailableStockCheckForOutModules(DataTable duplicatedt, string BranchId, string Date)
        {
            string StockCheck = string.Empty;
            string StockCheckMsg = string.Empty;
            //For Zero stock:Subhabrata
            if (duplicatedt != null && duplicatedt.Rows.Count > 0)
            {
                foreach (DataRow row in duplicatedt.Rows)
                {
                    Int64 ProductId = Convert.ToInt64(row["ProductID"]);
                    string IsInventory = getProductIsInventoryExists(Convert.ToString(row["ProductID"]));
                    if (IsInventory != "N")
                    {

                        BusinessLogicLayer.DBEngine oDBEngine = new BusinessLogicLayer.DBEngine(ConfigurationManager.AppSettings["DBConnectionDefault"]);
                        //DataTable dtAvailableStockCheck = oDBEngine.GetDataTable("Select dbo.fn_CheckAvailableStockSCBOIST(" + BranchId + ",'" + Convert.ToString(HttpContext.Current.Session["LastCompany"]) + "','" + Convert.ToString(HttpContext.Current.Session["LastFinYear"]) + "'," + ProductId + "'," + Convert.ToDateTime(Date) + ") as branchopenstock");
                        DataTable dtAvailableStockCheck = oDBEngine.GetDataTable("Select dbo.fn_CheckAvailableStockForAlreadyDelivered(" + BranchId + ",'" + Convert.ToString(HttpContext.Current.Session["LastCompany"]) + "','" + Convert.ToString(HttpContext.Current.Session["LastFinYear"]) + "','" + ProductId + "','" + Convert.ToDateTime(Date).ToString("yyyy-MM-dd") + "') as branchopenstock");

                        if (dtAvailableStockCheck.Rows.Count > 0)
                        {
                            StockCheck = Convert.ToString(Math.Round(Convert.ToDecimal(dtAvailableStockCheck.Rows[0]["branchopenstock"]), 2));

                            if (Convert.ToDecimal(row["Quantity"]) > Convert.ToDecimal(StockCheck))
                            {
                                StockCheckMsg = "MoreThanStock";
                                break;
                            }
                            if (StockCheck == "0.00")
                            {
                                StockCheckMsg = "ZeroStock";
                                break;
                            }

                        }
                    }
                }
            }//End
            return StockCheckMsg;
        }
        public DataTable GetProductStockBranchWise(int branchId, string ProdId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_GetProductAvailableListStatewise");
            proc.AddVarcharPara("@companyId", 100, Convert.ToString(HttpContext.Current.Session["LastCompany"]));
            proc.AddVarcharPara("@finYear", 50, Convert.ToString(HttpContext.Current.Session["LastFinYear"]));
            proc.AddIntegerPara("@branchId", branchId);
            proc.AddVarcharPara("@productId", 10, ProdId);
            ds = proc.GetTable();
            return ds;
        }

        public DataSet GetAllDetailsByBranch(string BranchID, String strCompanyID, string strFinYear)
        {
            DataSet ds = new DataSet();
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "GetAllDetailsByBranch");
            proc.AddVarcharPara("@branch", 4000, BranchID);
            proc.AddVarcharPara("@CompanyID", 100, strCompanyID);
            proc.AddVarcharPara("@BranchID", 4000, BranchID);
            proc.AddVarcharPara("@FinYear", 100, strFinYear);
            proc.AddVarcharPara("@Type", 100, "10");

            ds = proc.GetDataSet();

            return ds;
        }

        public void AssignOldunitBranch(string invoiceId, string branchId)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_PosSalesInvoice");
            proc.AddVarcharPara("@Action", 100, "AssignOldunitBranch");
            proc.AddIntegerPara("@Invoice_Id", Convert.ToInt32(invoiceId));
            proc.AddIntegerPara("@branch", Convert.ToInt32(branchId));
            proc.AddIntegerPara("@UserID", Convert.ToInt32(HttpContext.Current.Session["userid"]));
            proc.RunActionQuery();
        }
    }
}
