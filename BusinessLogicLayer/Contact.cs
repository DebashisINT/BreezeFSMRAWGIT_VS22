using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using System.Data;
namespace BusinessLogicLayer
{
   public class Contact
    {


        public void Insert_ContactRegistration(string vcrg_cntID, string vcrg_company1, string vcrg_exchange1, DateTime vcrg_regisDate,
                                         DateTime vcrg_businessCmmDate, DateTime vcrg_suspensionDate, string vcrg_reasonforSuspension,
                                         string vcrg_tcode, string vCreateUser, string vcrg_SubBrokerFranchiseeID, string vcrg_Dealer,
                                         DateTime vcrg_AccountClosureDate, string vcrg_FrontOfficeBranchCode, string vcrg_FrontOfficeGroupCode,
                                         string vcrg_ParticipantSchemeCode, string vcrg_ClearingBankCode, string vcrg_SchemeCode, string vcrg_STTPattern,
                                         string vSttWap, string vMapinSwapSebi, string vcrg_exchangesegment
                                           )
        {
            ProcedureExecute proc;
            string rtrnvalue = "";
            try
            {



                using (proc = new ProcedureExecute("ContactInsert"))
                {

                    proc.AddVarcharPara("@crg_cntID", 100, vcrg_cntID);
                    proc.AddVarcharPara("@crg_company1", 100, vcrg_company1);
                    proc.AddVarcharPara("@crg_exchange1", 100, vcrg_exchange1);
                    proc.AddDateTimePara("@crg_regisDate", vcrg_regisDate);
                    proc.AddDateTimePara("@crg_businessCmmDate", vcrg_businessCmmDate);
                    proc.AddDateTimePara("@crg_suspensionDate", vcrg_suspensionDate);
                    proc.AddVarcharPara("@crg_reasonforSuspension", 2000, vcrg_reasonforSuspension);
                    proc.AddVarcharPara("@crg_tcode", 100, vcrg_tcode);
                    proc.AddIntegerPara("@CreateUser", Convert.ToInt32(vCreateUser));
                    proc.AddVarcharPara("@crg_SubBrokerFranchiseeID", 100, vcrg_SubBrokerFranchiseeID);
                    proc.AddVarcharPara("@crg_Dealer", 100, vcrg_Dealer);
                    proc.AddDateTimePara("@crg_AccountClosureDate", vcrg_AccountClosureDate);
                    proc.AddVarcharPara("@crg_FrontOfficeBranchCode", 50, vcrg_FrontOfficeBranchCode);
                    proc.AddVarcharPara("@crg_FrontOfficeGroupCode", 50, vcrg_FrontOfficeGroupCode);
                    proc.AddVarcharPara("@crg_ParticipantSchemeCode", 50, vcrg_ParticipantSchemeCode);
                    proc.AddVarcharPara("@crg_ClearingBankCode", 50, vcrg_ClearingBankCode);
                    proc.AddVarcharPara("@crg_SchemeCode", 50, vcrg_SchemeCode);
                    proc.AddVarcharPara("@crg_STTPattern", 50, vcrg_STTPattern);
                    proc.AddVarcharPara("@SttWap", 50, vSttWap);
                    proc.AddVarcharPara("@MapinSwapSebi", 10, vMapinSwapSebi);
                    proc.AddIntegerPara("@crg_exchangesegment", Convert.ToInt32(vcrg_exchangesegment));

                    // proc.AddVarcharPara("@result", 50, "", QueryParameterDirection.Output);


                    int i = proc.RunActionQuery();
                    //rtrnvalue = proc.GetParaValue("@result").ToString();
                    //return rtrnvalue;


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


        public void Update_ContactRegistration(string vcrg_internalId, string vcrg_cntID, string vcrg_company1, string vcrg_exchange1, DateTime vcrg_regisDate,
                                         DateTime vcrg_businessCmmDate, DateTime vcrg_suspensionDate, string vcrg_reasonforSuspension,
                                         string vcrg_tcode, string vCreateUser, string vcrg_SubBrokerFranchiseeID, string vcrg_Dealer,
                                         DateTime vcrg_AccountClosureDate, string vcrg_FrontOfficeBranchCode, string vcrg_FrontOfficeGroupCode,
                                         string vcrg_ParticipantSchemeCode, string vcrg_ClearingBankCode, string vcrg_SchemeCode, string vcrg_STTPattern,
                                         string vSttWap, string vMapinSwapSebi, string vcrg_exchangesegment
                                           )
        {
            ProcedureExecute proc;
            string rtrnvalue = "";
            try
            {

                using (proc = new ProcedureExecute("updateContactExchange"))
                {

                    proc.AddVarcharPara("@crg_internalId", 100, vcrg_internalId);
                    proc.AddVarcharPara("@crg_cntID", 100, vcrg_cntID);
                    proc.AddVarcharPara("@crg_company1", 100, vcrg_company1);
                    proc.AddVarcharPara("@crg_exchange1", 100, vcrg_exchange1);
                    proc.AddDateTimePara("@crg_regisDate", vcrg_regisDate);
                    proc.AddDateTimePara("@crg_businessCmmDate", vcrg_businessCmmDate);
                    proc.AddDateTimePara("@crg_suspensionDate", vcrg_suspensionDate);
                    proc.AddVarcharPara("@crg_reasonforSuspension", 2000, vcrg_reasonforSuspension);
                    proc.AddVarcharPara("@crg_tcode", 100, vcrg_tcode);
                    proc.AddIntegerPara("@CreateUser", Convert.ToInt32(vCreateUser));
                    proc.AddVarcharPara("@crg_SubBrokerFranchiseeID", 100, vcrg_SubBrokerFranchiseeID);
                    proc.AddVarcharPara("@crg_Dealer", 100, vcrg_Dealer);
                    proc.AddDateTimePara("@crg_AccountClosureDate", vcrg_AccountClosureDate);
                    proc.AddVarcharPara("@crg_FrontOfficeBranchCode", 50, vcrg_FrontOfficeBranchCode);
                    proc.AddVarcharPara("@crg_FrontOfficeGroupCode", 50, vcrg_FrontOfficeGroupCode);
                    proc.AddVarcharPara("@crg_ParticipantSchemeCode", 50, vcrg_ParticipantSchemeCode);
                    proc.AddVarcharPara("@crg_ClearingBankCode", 50, vcrg_ClearingBankCode);
                    proc.AddVarcharPara("@crg_SchemeCode", 50, vcrg_SchemeCode);
                    proc.AddVarcharPara("@crg_STTPattern", 50, vcrg_STTPattern);
                    proc.AddVarcharPara("@SttWap", 50, vSttWap);
                    proc.AddVarcharPara("@MapinSwapSebi", 10, vMapinSwapSebi);
                    proc.AddIntegerPara("@crg_exchangesegment", Convert.ToInt32(vcrg_exchangesegment));

                    // proc.AddVarcharPara("@result", 50, "", QueryParameterDirection.Output);





                    int i = proc.RunActionQuery();
                    //rtrnvalue = proc.GetParaValue("@result").ToString();
                    //return rtrnvalue;


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


        // Code  Added and commented By Priti on15122016 to add 3 fields Creditcard,txtcreditDays.Text.Trim(),txtCreditLimit.Text.Trim()
        //public string Insert_ContactGeneral(string vcontacttype, string vcnt_ucc, string vcnt_salutation, string vcnt_firstName,
        //                                   string vcnt_middleName, string vcnt_lastName, string vcnt_shortName, string vcnt_branchId, string vcnt_sex,
        //                                   string vcnt_maritalStatus, DateTime vcnt_DOB, DateTime vcnt_anniversaryDate, string vcnt_legalStatus,
        //                                   string vcnt_education, string vcnt_profession, string vcnt_organization, string vcnt_jobResponsibility,
        //                                   string vcnt_designation, string vcnt_industry, string vcnt_contactSource, string vcnt_referedBy,
        //                                   string vRPartner, string vcnt_contactType, string vcnt_contactStatus,
        //                                   DateTime vcnt_RegistrationDate, string vcnt_rating, string vcnt_reason, string vbloodgroup, string vWebLogIn,
        //                                   string vPassWord, string vlastModifyUser, string vplaceincorporation, DateTime vdatebusscommence,
        //                                   string votheroccu, string vcountry
        //                                   )
        public string Insert_ContactGeneral(string vcontacttype, string vcnt_ucc, string vcnt_salutation, string vcnt_firstName,
                                           string vcnt_middleName, string vcnt_lastName, string vcnt_shortName, string vcnt_branchId, string vcnt_sex,
                                           string vcnt_maritalStatus, DateTime vcnt_DOB, DateTime vcnt_anniversaryDate, string vcnt_legalStatus,
                                           string vcnt_education, string vcnt_profession, string vcnt_organization, string vcnt_jobResponsibility,
                                           string vcnt_designation, string vcnt_industry, string vcnt_contactSource, string vcnt_referedBy,
                                           string vRPartner, string vcnt_contactType, string vcnt_contactStatus,
                                           DateTime vcnt_RegistrationDate, string vcnt_rating, string vcnt_reason, string vbloodgroup, string vWebLogIn,
                                           string vPassWord, string vlastModifyUser, string vplaceincorporation, DateTime vdatebusscommence,
                                           string votheroccu, string vcountry, bool cnt_IsCreditHold, int cnt_CreditDays, Decimal cnt_CreditLimit, string status, string GSTIN, string AssociatedEmp
                                           )
          {
            ProcedureExecute proc;
            string rtrnvalue = "";
            try
            {


                using (proc = new ProcedureExecute("ContactInsert"))
                {
                    // .............................Code Commented and Added by Sam on 07122016 to use Convert.tostring instead of tostring(). ................

                    //--added by sanjib due mismatch data with as per db so in codebehind that page are using for many purpose so i have added this here.------
                    if(vcontacttype=="OtherEntity")
                    {proc.AddVarcharPara("@contacttype", 100, "Other Contacts/Entities");}
                    else if (vcontacttype == "Salesman/Agents")
                    {
                        proc.AddVarcharPara("@contacttype", 100, "Agents");
                    }
                    else{
                     proc.AddVarcharPara("@contacttype", 100, vcontacttype);
                    }

                    //---end


                    proc.AddVarcharPara("@cnt_ucc", 80, vcnt_ucc);
                    proc.AddIntegerPara("@cnt_salutation", Convert.ToInt32(vcnt_salutation));
                    proc.AddVarcharPara("@cnt_firstName", 150, vcnt_firstName);
                    proc.AddVarcharPara("@cnt_middleName", 100, vcnt_middleName);
                    proc.AddVarcharPara("@cnt_lastName", 100, vcnt_lastName);
                    proc.AddVarcharPara("@cnt_shortName", 80, vcnt_shortName);
                    proc.AddIntegerPara("@cnt_branchId", Convert.ToInt32(vcnt_branchId));
                    proc.AddIntegerPara("@cnt_sex", Convert.ToInt32(vcnt_sex));
                    proc.AddIntegerPara("@cnt_maritalStatus", Convert.ToInt32(vcnt_maritalStatus));
                    // .............................Code Above Commented and Added by Sam on 07122016 to use Convert.tostring instead of tostring(). ..................................... 
                    proc.AddDateTimePara("@cnt_DOB", vcnt_DOB);

                    proc.AddDateTimePara("@cnt_anniversaryDate", vcnt_anniversaryDate);

                    proc.AddIntegerPara("@cnt_legalStatus", Convert.ToInt32(vcnt_legalStatus));
                    proc.AddIntegerPara("@cnt_education", Convert.ToInt32(vcnt_education));
                    proc.AddIntegerPara("@cnt_profession", Convert.ToInt32(vcnt_profession));
                    proc.AddVarcharPara("@cnt_organization", 100, vcnt_organization);
                    proc.AddIntegerPara("@cnt_jobResponsibility", Convert.ToInt32(vcnt_jobResponsibility));
                    proc.AddIntegerPara("@cnt_designation", Convert.ToInt32(vcnt_designation));
                    proc.AddIntegerPara("@cnt_industry", Convert.ToInt32(vcnt_industry));
                    proc.AddIntegerPara("@cnt_contactSource", Convert.ToInt32(vcnt_contactSource));
                    proc.AddVarcharPara("@cnt_referedBy", 100, vcnt_referedBy);
                    proc.AddVarcharPara("@RPartner", 100, vRPartner);
                    proc.AddVarcharPara("@cnt_contactType", 100, vcnt_contactType);
                    proc.AddIntegerPara("@cnt_contactStatus", Convert.ToInt32(vcnt_contactStatus));

                    proc.AddDateTimePara("@cnt_RegistrationDate", vcnt_RegistrationDate);

                    proc.AddIntegerPara("@cnt_rating", Convert.ToInt32(vcnt_rating));
                    proc.AddVarcharPara("@cnt_reason", 400, vcnt_reason);
                    proc.AddVarcharPara("@bloodgroup", 100, vbloodgroup);
                    proc.AddVarcharPara("@WebLogIn", 100, vWebLogIn);
                    proc.AddVarcharPara("@PassWord", 100, vPassWord);
                    proc.AddVarcharPara("@lastModifyUser", 100, vlastModifyUser);
                    proc.AddVarcharPara("@placeincorporation", 100, vplaceincorporation);

                    proc.AddDateTimePara("@datebusscommence", vdatebusscommence);

                    proc.AddVarcharPara("@otheroccu", 100, votheroccu);
                    proc.AddIntegerPara("@country", Convert.ToInt32(vcountry));
                    //.................... Code  Added and commented By Priti on15122016 to add 3 fields Creditcard,creditDays,CreditLimit
                    proc.AddBooleanPara("@cnt_IsCreditHold", cnt_IsCreditHold);
                    proc.AddIntegerPara("@cnt_CreditDays ", Convert.ToInt32(cnt_CreditDays));
                    proc.AddDecimalPara("@cnt_CreditLimit",4,19,cnt_CreditLimit);
                    proc.AddVarcharPara("@status", 5, status);
                    //............end.................

                    //Debjyoti For GSTIN 020617
                    proc.AddVarcharPara("@CNT_GSTIN", 15, GSTIN);
                    proc.AddVarcharPara("@result", 50, "", QueryParameterDirection.Output);
                    proc.AddVarcharPara("@cnt_AssociatedEmp",15, AssociatedEmp);




                    int i = proc.RunActionQuery();
                    rtrnvalue = proc.GetParaValue("@result").ToString();
                    return rtrnvalue;


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

        public string Insert_DataByIDType(string internalId, string IdType, string uniqueId)
        {
            ProcedureExecute proc;
            string rtrnvalue = "";
            try
            {


                using (proc = new ProcedureExecute("prc_SetCustomerUniqueIdToCorrespondence"))
                {
                    
                        proc.AddVarcharPara("@cnt_internalId", 50, internalId);
                        proc.AddIntegerPara("@cnt_IdType", Convert.ToInt32(IdType));
                        proc.AddVarcharPara("@uniqueId", 500, uniqueId);

                        int i = proc.RunActionQuery();
                   
                    rtrnvalue = "1";
                    return rtrnvalue;


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

        public string Get_UCCCode(string vUCC)
        {
            ProcedureExecute proc;
            string rtrnvalue = "";
            try
            {
                using (proc = new ProcedureExecute("sp_GenerateContactUCC"))
                {

                    proc.AddVarcharPara("@UCC", 50, vUCC);
                    proc.AddVarcharPara("@result", 50, "", QueryParameterDirection.Output);

                    int i = proc.RunActionQuery();
                    rtrnvalue = proc.GetParaValue("@result").ToString();
                    return rtrnvalue;

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


        public void Insert_DummyAdress(string vcontacttype, string vInternalId)
        {
            ProcedureExecute proc;
            string rtrnvalue = "";
            try
            {
                using (proc = new ProcedureExecute("AdressDummyInsert"))
                {

                    proc.AddVarcharPara("@contacttype", 50, vcontacttype);
                    proc.AddVarcharPara("@InternalId", 20, vInternalId);
                    int i = proc.RunActionQuery();

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

        public void Insert_TransporterVehicles(string InternalId, string VehicleNos)
        {
            ProcedureExecute proc;
            string rtrnvalue = "";
            try
            {
                using (proc = new ProcedureExecute("prc_TransporterVechileNumber"))
                {
                    proc.AddVarcharPara("@action", 50, "InsertVehicles");
                    proc.AddVarcharPara("@InetrnalId", 50, InternalId);
                    proc.AddVarcharPara("@VehicleNos", 4000, VehicleNos);
                    int i = proc.RunActionQuery();

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
        public void Delete_TransporterVehicles(string InternalId)
        {
            ProcedureExecute proc;
           
            try
            {
                using (proc = new ProcedureExecute("prc_TransporterVechileNumber"))
                {
                    proc.AddVarcharPara("@action", 50, "DeleteVehicles");
                    proc.AddVarcharPara("@InetrnalId", 50, InternalId);                   
                    int i = proc.RunActionQuery();

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
        public DataTable Get_TransporterVehicles(string InternalId)
        {
            
            DataSet ds = new DataSet();
                 
               
                    ProcedureExecute proc = new ProcedureExecute("prc_TransporterVechileNumber");
                    proc.AddVarcharPara("@action", 50, "SelectVehicles");
                    proc.AddVarcharPara("@InetrnalId", 50, InternalId);  
                    ds = proc.GetDataSet();
                    return ds.Tables[0];
          
        }
        public DataTable DeleteDriver(string Cntid)
        {

            DataSet ds = new DataSet();


            ProcedureExecute proc = new ProcedureExecute("prc_TransporterVechileNumber");
            proc.AddVarcharPara("@action", 50, "DeleteDriver");
            proc.AddIntegerPara("@cnt_id", Convert.ToInt32(Cntid));
            ds = proc.GetDataSet();
            return ds.Tables[0];

        }

       public void Delete_Chargegroupmember(string vChargegroupmember_ID, string vCompany, string vClient, string vSegment,
                                          string vparam,string vTradprof_type )
       {
           ProcedureExecute proc;
           string rtrnvalue = "";
           try
           {

           
               using (proc = new ProcedureExecute("Chargegroupmember_delete"))
               {

                   proc.AddVarcharPara("@Chargegroupmember_ID", 20, vChargegroupmember_ID);
                   proc.AddVarcharPara("@Company", 100, vCompany);
                   proc.AddVarcharPara("@Client", 100, vClient);
                   if (!String.IsNullOrEmpty(vSegment))
                   {
                   proc.AddIntegerPara("@Segment",Convert.ToInt32(vSegment));
                   }
                   else { proc.AddIntegerPara("@Segment", 1); }
                   proc.AddVarcharPara("@param", 10, vparam);
                   proc.AddVarcharPara("@Tradprof_type", 10, vTradprof_type);
                 


                   int i = proc.RunActionQuery();
                   //rtrnvalue = proc.GetParaValue("@result").ToString();
                   //return rtrnvalue;


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

       
       public void Insert_Brokerage( string vChargeGroupMembers_CustomerID,string vChargeGroupMembers_CompanyID,string vChargeGroupMembers_SegmentID,
                                       string vChargeGroupMembers_GroupType,string vChargeGroupMembers_GroupCode,DateTime vChargeGroupMembers_FromDate,
                                       string vChargeGroupMembers_CreateUser)
       {
           ProcedureExecute proc;
           string rtrnvalue = "";
           try
           {
               

               using (proc = new ProcedureExecute("Chargegroupmember_delete"))
               {
                   
                   proc.AddVarcharPara("@ChargeGroupMembers_CustomerID", 10, vChargeGroupMembers_CustomerID);
                   proc.AddVarcharPara("@ChargeGroupMembers_CompanyID", 10, vChargeGroupMembers_CompanyID);
                   proc.AddIntegerPara("@ChargeGroupMembers_SegmentID", Convert.ToInt32(vChargeGroupMembers_SegmentID));
                   proc.AddIntegerPara("@ChargeGroupMembers_GroupType", Convert.ToInt32(vChargeGroupMembers_GroupType));
                   proc.AddVarcharPara("@ChargeGroupMembers_GroupCode", 50, vChargeGroupMembers_GroupCode);
                   proc.AddDateTimePara("@ChargeGroupMembers_FromDate", vChargeGroupMembers_FromDate);
                   proc.AddVarcharPara("@ChargeGroupMembers_CreateUser", 10, vChargeGroupMembers_CreateUser);



                   int i = proc.RunActionQuery();
                   //rtrnvalue = proc.GetParaValue("@result").ToString();
                   //return rtrnvalue;


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




       public void Insert_Trading(string vProfileMember_Type, string vProfileMember_CustomerID, string vProfileMember_Code,
                                      string vProfileMember_CreateUser, DateTime vProfileMember_DateFrom)
       {
           ProcedureExecute proc;
           string rtrnvalue = "";
           try
           {


               using (proc = new ProcedureExecute("insertTradingProfile"))
               {

                   proc.AddIntegerPara("@ProfileMember_Type",  Convert.ToInt32(vProfileMember_Type));
                   proc.AddVarcharPara("@ProfileMember_CustomerID", 10, vProfileMember_CustomerID);
                   proc.AddVarcharPara("@ProfileMember_Code",20, vProfileMember_Code);
                   proc.AddIntegerPara("@ProfileMember_CreateUser", Convert.ToInt32(vProfileMember_CreateUser));
                   proc.AddDateTimePara("@ProfileMember_DateFrom",  vProfileMember_DateFrom);
                  


                   int i = proc.RunActionQuery();
                   //rtrnvalue = proc.GetParaValue("@result").ToString();
                   //return rtrnvalue;


                  

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



       public void Insert_OtherChanges( string vOtherChargeMember_CompanyID,string vOtherChargeMember_CustomerID,string vOtherChargeMember_SegmentID,
               string vOtherChargeMember_CreateUser,DateTime vOtherChargeMember_DateFrom,string vOtherChargeMember_OtherChargeCode)
          {
           ProcedureExecute proc;
           string rtrnvalue = "";
           try
           {


               using (proc = new ProcedureExecute("insertOtherCharges"))
               {

                   proc.AddVarcharPara("@OtherChargeMember_CompanyID", 10, vOtherChargeMember_CompanyID);
                   proc.AddVarcharPara("@OtherChargeMember_CustomerID", 10, vOtherChargeMember_CustomerID);
                   proc.AddIntegerPara("@OtherChargeMember_SegmentID", Convert.ToInt32(vOtherChargeMember_SegmentID));
                   proc.AddVarcharPara("@OtherChargeMember_CreateUser", 10, vOtherChargeMember_CreateUser);
                   proc.AddDateTimePara("@OtherChargeMember_DateFrom", vOtherChargeMember_DateFrom);
                   proc.AddVarcharPara("@OtherChargeMember_OtherChargeCode",10, vOtherChargeMember_OtherChargeCode);
              



                   int i = proc.RunActionQuery();
                   //rtrnvalue = proc.GetParaValue("@result").ToString();
                   //return rtrnvalue;

                 
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


       public void GridContactPerson_RowUpdating_BL(string name, string Officephone, string Residencephone, string Mobilephone, string email,
           string cp_designation, string cp_relationShip, string cp_status, string cp_Pan, string cp_Din, string contactid, string userid)
       {
           ProcedureExecute proc;
           string rtrnvalue = "";
           try
           { 
               using (proc = new ProcedureExecute("insertTradingProfile"))
               {
                   proc.AddVarcharPara("@name", 100, name);
                   proc.AddVarcharPara("@Officephone", 100, Officephone);
                   proc.AddVarcharPara("@Residencephone", 100, Residencephone);
                   proc.AddVarcharPara("@Mobilephone", 100, Mobilephone);
                   proc.AddVarcharPara("@email", 100, email);
                   proc.AddVarcharPara("@name", 100, cp_designation);
                   proc.AddVarcharPara("@name", 100, cp_relationShip);
                   proc.AddVarcharPara("@name", 100, cp_status);
                   proc.AddVarcharPara("@name", 100, cp_Pan);
                   proc.AddVarcharPara("@name", 100, cp_Din);
                   proc.AddVarcharPara("@name", 100, contactid);
                   proc.AddVarcharPara("@name", 100, userid);  

                   int i = proc.RunActionQuery();  
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


    }
}
