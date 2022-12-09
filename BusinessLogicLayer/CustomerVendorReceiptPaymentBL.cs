using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class CustomerVendorReceiptPaymentBL
    {
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
        public DataTable PopulateBranchForTDS()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "PopulateBranchForTDS");
            dt = proc.GetTable();
            return dt;
        }
        public DataTable PopulateCustomerDetail()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "PopulateCustomerDetail");
            dt = proc.GetTable();
            return dt;
        }
        public DataTable PopulateVendorDetail()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "PopulateVendorsDetail");
            dt = proc.GetTable();
            return dt;
        }
       
        public DataTable PopulateContactPersonOfCustomer(string InternalId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "PopulateContactPersonOfCustomer");
            proc.AddVarcharPara("@InternalId", 100, InternalId);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetCustomerCashBank(string @userbranch, string CompanyId)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "GetCashBankData");
            proc.AddVarcharPara("@BranchId", 100, @userbranch);
            proc.AddVarcharPara("@CompanyId", 100, CompanyId);

            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetSystemSettings()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "GetSystemSettingValue");
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetSystemSettingsVendor()
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_VendorReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "GetSystemSettingValue");
            dt = proc.GetTable();
            return dt;
        }
       
        public DataTable CheckMultipleType(string CRTid)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "MultipleTypePresentOrNot");
            proc.AddIntegerPara("@CRTID", Convert.ToInt32(CRTid));
            dt = proc.GetTable();
            return dt;
        }
        public DataTable CheckCRTTraanaction(string CRTid)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "CRTTagOrNotInPOS");
            proc.AddIntegerPara("@CRTID", Convert.ToInt32(CRTid));
            dt = proc.GetTable();
            return dt;
        }
        public DataTable GetSalesInvoiceDetails(string CustomerID, string voucheramount)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 500, "GetSalesInvoiceDetails");
            proc.AddVarcharPara("@CustomerID", 500, CustomerID);
            proc.AddVarcharPara("@targetAmount", 500, voucheramount);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable BindSalesInvoiceDetails(string ComponentDetailsIDs)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 500, "BindInvoiceDetails");
            proc.AddVarcharPara("@ComponentDetails", 500, ComponentDetailsIDs);
            ds = proc.GetTable();
            return ds;
        }
        public int DeleteCRPOrder(string poid)
        {
            int i;
            int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "DeleteCRP");
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(poid));
            proc.AddVarcharPara("@ReturnValueDelete", 50, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValueDelete"));
            return rtrnvalue;

        }
        public int DeleteCRPADVOrder(string ReceiptPayment_ID)
        {
            int i;
            int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerAdvanceReceiptDetails");
            proc.AddVarcharPara("@Action", 100, "DeleteCRPADV");
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(ReceiptPayment_ID));
            proc.AddVarcharPara("@ReturnValueDelete", 50, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValueDelete"));
            return rtrnvalue;

        }
        public bool IsUnpaidAmountEqual(string ReceiptPayment_ID)
        {
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerAdvanceReceiptDetails");
            proc.AddVarcharPara("@Action", 100, "UnpaidAmountChecking");
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(ReceiptPayment_ID));

            DataTable DT = proc.GetTable();

            if (DT != null && DT.Rows.Count > 0)
            {
                return Convert.ToString(DT.Rows[0]["Flag"]).Equals("true") ? true : false;
            }
            else
            {
                return false;
            }

        }
        public int DeleteVendorPROrder(string poid)
        {
            int i;
            int rtrnvalue = 0;
            ProcedureExecute proc = new ProcedureExecute("prc_VendorReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "DeleteVPR");
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(poid));
            proc.AddVarcharPara("@ReturnValueDelete", 50, "0", QueryParameterDirection.Output);
            i = proc.RunActionQuery();
            rtrnvalue = Convert.ToInt32(proc.GetParaValue("@ReturnValueDelete"));
            return rtrnvalue;

        }
        public DataTable DeleteVendorDependentOrder(string poid)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_VendorReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "DeleteVPRDependentOrder");
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(poid));
            dt = proc.GetTable();
            return dt;
        }
        public DataTable DeleteCustomerDependentOrder(string poid)
        {
            DataTable dt = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 100, "DeleteCRPDependentOrder");
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(poid));
            dt = proc.GetTable();
            return dt;

        }
        public bool IsCRPExist(string Poid)
        {
            bool IsExist = false;
            if (Poid != "" && Convert.ToString(Poid).Trim() != "")
            {
                DataTable dt = new DataTable();
                dt = DeleteCustomerDependentOrder(Poid);
                if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["isexist"]) != "0")
                {
                    IsExist = true;
                }
            }
            return IsExist;
        }
        public bool IsVRPExist(string Poid)
        {
            bool IsExist = false;
            if (Poid != "" && Convert.ToString(Poid).Trim() != "")
            {
                DataTable dt = new DataTable();
                dt = DeleteVendorDependentOrder(Poid);
                if (dt.Rows.Count > 0 && Convert.ToString(dt.Rows[0]["isexist"]) != "0")
                {
                    IsExist = true;
                }
            }
            return IsExist;
        }
        public DataTable GetExactAmount(string Type, string documentId, string strIsOpening)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 500, "GetExactAmount");
            proc.AddVarcharPara("@Type", 500, Type);
            proc.AddIntegerPara("@DocumentId", Convert.ToInt32(documentId));
            proc.AddVarcharPara("@IsOpening", 50, strIsOpening);
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetExactAmountForEdit(string Type, string documentId, string strIsOpening, string ReceiptPaymentID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_CustomerReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 500, "GetExactAmountForEdit");
            proc.AddVarcharPara("@Type", 500, Type);
            proc.AddIntegerPara("@DocumentId", Convert.ToInt32(documentId));
            proc.AddVarcharPara("@IsOpening", 50, strIsOpening);
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(ReceiptPaymentID));
            ds = proc.GetTable();
            return ds;
        }
        public DataTable GetExactAmountForVendorEdit(string Type, string documentId, string strIsOpening, string ReceiptPaymentID)
        {
            DataTable ds = new DataTable();
            ProcedureExecute proc = new ProcedureExecute("prc_VendorReceiptPaymentDetails");
            proc.AddVarcharPara("@Action", 500, "GetExactAmountForEdit");
            proc.AddVarcharPara("@Type", 500, Type);
            proc.AddIntegerPara("@DocumentId", Convert.ToInt32(documentId));
            proc.AddVarcharPara("@IsOpening", 50, strIsOpening);
            proc.AddIntegerPara("@ReceiptPayment_ID", Convert.ToInt32(ReceiptPaymentID));
            ds = proc.GetTable();
            return ds;
        }

    }
}
