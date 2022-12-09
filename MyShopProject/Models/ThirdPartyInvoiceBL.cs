using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace MyShop.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class HEADER
    {
        public string TALLYREQUEST { get; set; }
    }

    public class STATICVARIABLES
    {
        public string SVCURRENTCOMPANY { get; set; }
    }

    public class REQUESTDESC
    {
        public string REPORTNAME { get; set; }
        public STATICVARIABLES STATICVARIABLES { get; set; }
    }

    public class OLDAUDITENTRYIDSLIST
    {
        public string TYPE { get; set; }
        public string OLDAUDITENTRYIDS { get; set; }
    }

    public class INVOICEDELNOTESLIST
    {
        public string BASICSHIPPINGDATE { get; set; }
        public string BASICSHIPDELIVERYNOTE { get; set; }
    }

    public class INVOICEORDERLISTLIST
    {
        public string BASICORDERDATE { get; set; }
        public string BASICPURCHASEORDERNO { get; set; }
    }

    public class OLDAUDITENTRYIDSLIST2
    {
        public string TYPE { get; set; }
        public string OLDAUDITENTRYIDS { get; set; }
    }

    public class BILLALLOCATIONSLIST
    {
        public string NAME { get; set; }
        public string BILLTYPE { get; set; }
        public string TDSDEDUCTEEISSPECIALRATE { get; set; }
        public string AMOUNT { get; set; }
    }

    public class LEDGERENTRIESLIST
    {
        public string LEDGERNAME { get; set; }
        public string ISDEEMEDPOSITIVE { get; set; }
        public string LEDGERFROMITEM { get; set; }
        public string REMOVEZEROENTRIES { get; set; }
        public string ISPARTYLEDGER { get; set; }
        public string ISLASTDEEMEDPOSITIVE { get; set; }
        public string ISCAPVATTAXALTERED { get; set; }
        public string ISCAPVATNOTCLAIMED { get; set; }
        public string AMOUNT { get; set; }

    }

    public class BATCHALLOCATIONSLIST
    {
        public string GODOWNNAME { get; set; }
        public string BATCHNAME { get; set; }
        public string DYNAMICCSTISCLEARED { get; set; }
        public string AMOUNT { get; set; }
        public string ACTUALQTY { get; set; }
        public string BILLEDQTY { get; set; }
    }

    public class OLDAUDITENTRYIDSLIST3
    {
        public string TYPE { get; set; }
        public string OLDAUDITENTRYIDS { get; set; }
    }

    public class ACCOUNTINGALLOCATIONSLIST
    {
        public string LEDGERNAME { get; set; }
        public string ISDEEMEDPOSITIVE { get; set; }
        public string LEDGERFROMITEM { get; set; }
        public string REMOVEZEROENTRIES { get; set; }
        public string ISPARTYLEDGER { get; set; }
        public string ISLASTDEEMEDPOSITIVE { get; set; }
        public string ISCAPVATTAXALTERED { get; set; }
        public string ISCAPVATNOTCLAIMED { get; set; }
        public string AMOUNT { get; set; }


        public class ALLINVENTORYENTRIESLIST
        {
            public string STOCKITEMNAME { get; set; }
            public string ISDEEMEDPOSITIVE { get; set; }
            public string ISLASTDEEMEDPOSITIVE { get; set; }
            public string ISAUTONEGATE { get; set; }
            public string ISCUSTOMSCLEARANCE { get; set; }
            public string ISTRACKCOMPONENT { get; set; }
            public string ISTRACKPRODUCTION { get; set; }
            public string ISPRIMARYITEM { get; set; }
            public string ISSCRAP { get; set; }
            public string RATE { get; set; }
            public string AMOUNT { get; set; }
            public string ACTUALQTY { get; set; }
            public string BILLEDQTY { get; set; }
        }

        public class VOUCHER
        {
            public string REMOTEID { get; set; }
            public string VCHKEY { get; set; }
            public string VCHTYPE { get; set; }
            public string ACTION { get; set; }
            public string OBJVIEW { get; set; }
            public string DATE { get; set; }
            public string GUID { get; set; }
            public string STATENAME { get; set; }
            public string NARRATION { get; set; }
            public string COUNTRYOFRESIDENCE { get; set; }
            public string PARTYNAME { get; set; }
            public string VOUCHERTYPENAME { get; set; }
            public string REFERENCE { get; set; }
            public string VOUCHERNUMBER { get; set; }
            public string PARTYLEDGERNAME { get; set; }
            public string BASICBASEPARTYNAME { get; set; }
            public string FBTPAYMENTTYPE { get; set; }
            public string PERSISTEDVIEW { get; set; }
            public string BASICBUYERNAME { get; set; }
            public string BASICDATETIMEOFINVOICE { get; set; }
            public string BASICDATETIMEOFREMOVAL { get; set; }
            public string DIFFACTUALQTY { get; set; }
            public string ISMSTFROMSYNC { get; set; }
            public string ASORIGINAL { get; set; }
            public string AUDITED { get; set; }
            public string FORJOBCOSTING { get; set; }
            public string ISOPTIONAL { get; set; }
            public string EFFECTIVEDATE { get; set; }
            public string USEFOREXCISE { get; set; }
            public string ISFORJOBWORKIN { get; set; }
            public string ALLOWCONSUMPTION { get; set; }
            public string USEFORINTEREST { get; set; }
            public string USEFORGAINLOSS { get; set; }
            public string USEFORGODOWNTRANSFER { get; set; }
            public string USEFORCOMPOUND { get; set; }
            public string USEFORSERVICETAX { get; set; }
            public string ISEXCISEVOUCHER { get; set; }
            public string EXCISETAXOVERRIDE { get; set; }
            public string USEFORTAXUNITTRANSFER { get; set; }
            public string IGNOREPOSVALIDATION { get; set; }
            public string EXCISEOPENING { get; set; }
            public string USEFORFINALPRODUCTION { get; set; }
            public string ISTDSOVERRIDDEN { get; set; }
            public string ISTCSOVERRIDDEN { get; set; }
            public string ISTDSTCSCASHVCH { get; set; }
            public string INCLUDEADVPYMTVCH { get; set; }
            public string ISSUBWORKSCONTRACT { get; set; }
            public string ISVATOVERRIDDEN { get; set; }
            public string IGNOREORIGVCHDATE { get; set; }
            public string ISVATPAIDATCUSTOMS { get; set; }
            public string ISDECLAREDTOCUSTOMS { get; set; }
            public string ISSERVICETAXOVERRIDDEN { get; set; }
            public string ISISDVOUCHER { get; set; }
            public string ISEXCISEOVERRIDDEN { get; set; }
            public string ISEXCISESUPPLYVCH { get; set; }
            public string ISGSTOVERRIDDEN { get; set; }
            public string GSTNOTEXPORTED { get; set; }
            public string IGNOREGSTINVALIDATION { get; set; }
            public string ISVATPRINCIPALACCOUNT { get; set; }
            public string ISBOENOTAPPLICABLE { get; set; }
            public string ISSHIPPINGWITHINSTATE { get; set; }
            public string ISOVERSEASTOURISTTRANS { get; set; }
            public string ISDESIGNATEDZONEPARTY { get; set; }
            public string ISCANCELLED { get; set; }
            public string HASCASHFLOW { get; set; }
            public string ISPOSTDATED { get; set; }
            public string USETRACKINGNUMBER { get; set; }
            public string ISINVOICE { get; set; }
            public string MFGJOURNAL { get; set; }
            public string HASDISCOUNTS { get; set; }
            public string ASPAYSLIP { get; set; }
            public string ISCOSTCENTRE { get; set; }
            public string ISSTXNONREALIZEDVCH { get; set; }
            public string ISEXCISEMANUFACTURERON { get; set; }
            public string ISBLANKCHEQUE { get; set; }
            public string ISVOID { get; set; }
            public string ISONHOLD { get; set; }
            public string ORDERLINESTATUS { get; set; }
            public string VATISAGNSTCANCSALES { get; set; }
            public string VATISPURCEXEMPTED { get; set; }
            public string ISVATRESTAXINVOICE { get; set; }
            public string VATISASSESABLECALCVCH { get; set; }
            public string ISVATDUTYPAID { get; set; }
            public string ISDELIVERYSAMEASCONSIGNEE { get; set; }
            public string ISDISPATCHSAMEASCONSIGNOR { get; set; }
            public string ISDELETED { get; set; }
            public string CHANGEVCHMODE { get; set; }
            public string ALTERID { get; set; }
            public string MASTERID { get; set; }
            public string VOUCHERKEY { get; set; }
            public List<ALLINVENTORYENTRIESLIST> ALLINVENTORYENTRIES { get; set; }

        }

        public class REMOTECMPINFOLIST
        {
            public string NAME { get; set; }
            public string REMOTECMPNAME { get; set; }
            public string REMOTECMPSTATE { get; set; }
        }


        public class BillingModel
        {
            public string user_id { get; set; }
            public string bill_id { get; set; }
            public string invoice_no { get; set; }
            public string invoice_date { get; set; }
            public string invoice_amount { get; set; }
            public string remarks { get; set; }
            public string order_id { get; set; }
            //Add Product in add billing Tanmoy 22-11-2019
            public List<ProductLists> product_list { get; set; }
            //End Add Product in add billing Tanmoy 22-11-2019
        }


        public class ProductLists
        {
            public String id { get; set; }
            public String product_name { get; set; }
            public decimal qty { get; set; }
            public decimal rate { get; set; }
            public decimal total_price { get; set; }
        }

        public class XmlConversion
        {
            #region ******************************************** Xml Conversion  ********************************************
            public static string ConvertToXml<T>(List<T> table, int metaIndex = 0)
            {
                XmlDocument ChoiceXML = new XmlDocument();
                ChoiceXML.AppendChild(ChoiceXML.CreateElement("root"));
                Type temp = typeof(T);

                foreach (var item in table)
                {
                    XmlElement element = ChoiceXML.CreateElement("data");

                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        element.AppendChild(ChoiceXML.CreateElement(pro.Name)).InnerText = Convert.ToString(item.GetType().GetProperty(pro.Name).GetValue(item, null));
                    }
                    ChoiceXML.DocumentElement.AppendChild(element);
                }

                return ChoiceXML.InnerXml.ToString();
            }


            #endregion

        }


    }


}