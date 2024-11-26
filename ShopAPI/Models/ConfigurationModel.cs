#region======================================Revision History=====================================================================
//1.0   V2.0.32     Debashis    17/01/2023      A new parameter has been added.Row: 797
//2.0   V2.0.38     Debashis    25/01/2023      A new parameter has been added.Row: 808
//3.0   V2.0.38     Debashis    02/02/2023      Some new parameters have been added.Row: 809
//4.0   V2.0.38     Debashis    08/02/2023      Some new parameter has been added.Row: 812
//5.0   V2.0.38     Debashis    22/03/2023      A new parameter has been added.Row: 815
//6.0   V2.0.39     Debashis    06/04/2023      A new parameter has been added.Row: 817
//7.0   V2.0.39     Debashis    24/04/2023      A new parameter has been added.Row: 824
//8.0   V2.0.39     Debashis    08/05/2023      Some new parameters have been added.Row: 825,826,827
//9.0   V2.0.39     Debashis    16/05/2023      A new parameter has been added.Row: 833
//10.0  V2.0.39     Debashis    16/05/2023      A new parameter has been added.Row: 838
//11.0  V2.0.39     Debashis    19/05/2023      A new parameter has been added.Row: 841
//12.0  V2.0.39     Debashis    02/06/2023      A new parameter has been added.Row: 848
//13.0  V2.0.40     Debashis    30/06/2023      A new parameter has been added.Row: 851
//14.0  V2.0.41     Debashis    17/07/2023      A new parameter has been added.Row: 856
//15.0  V2.0.41     Debashis    01/08/2023      Some new parameters have been added.Row: 860
//16.0  V2.0.41     Debashis    17/08/2023      Some new parameters have been added.Row: 863
//17.0  V2.0.41     Debashis    21/07/2023      A new parameter has been added.Row: 864
//18.0  V2.0.42     Debashis    06/10/2023      Some new parameters have been added.Row: 871
//19.0  V2.0.42     Debashis    16/11/2023      Some new parameters have been added.Row: 879
//20.0  V2.0.42     Debashis    04/12/2023      A new parameter has been added.Row: 866
//21.0  V2.0.42     Debashis    22/12/2023      Some new parameters have been added.Row: 891
//22.0  V2.0.44     Debashis    05/01/2024      A new parameter has been added.Row: 899 & Refer: 0027139
//23.0  V2.0.44     Debashis    20/02/2024      A new parameter has been added.Row: 900 & Refer: 0027245
//24.0  V2.0.45     Debashis    14/03/2024      Some new parameters have been added.Row: 901 & Refer: 0027279,0027285 & 0027282
//25.0  V2.0.45     Debashis    03/04/2024      Some new parameters have been added.Row: 903
//26.0  V2.0.44     Debashis    03/04/2024      A new parameter has been added.Row: 904
//27.0  V2.0.44     Debashis    10/08/2024      Some new parameters have been added.Row: 968
//28.0  V2.0.49     Debashis    17/09/2024      A new parameter has been added.Row: 979
//29.0  V2.0.49     Debashis    27/09/2024      A new parameter has been added.Row: 981
//30.0  V2.0.49     Debashis    01/10/2024      Some new parameters have been added.Row: 983
//31.0  V2.0.49     Debashis    09/10/2024      Some new parameters have been added.Row: 985
//32.0  V2.0.49     Debashis    23/10/2024      A new parameter has been added.Row: 987
//33.0  V2.0.49     Debashis    15/11/2024      A new parameter has been added.Row: 1005
#endregion===================================End of Revision History==============================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ConfigurationModel
    {

        public string status { get; set; }

        public string message { get; set; }
        public string min_accuracy { get; set; }
        public string max_accuracy { get; set; }
        public string min_distance { get; set; }
        public string max_distance { get; set; }
        public string idle_time { get; set; }
        public bool isRevisitCaptureImage { get; set; }
        public bool isShowAllProduct { get; set; }
        public bool isPrimaryTargetMandatory { get; set; }
        public bool isStockAvailableForAll { get; set; }

        public bool isStockAvailableForPopup { get; set; }
        public bool isOrderAvailableForPopup { get; set; }
        public bool isCollectionAvailableForPopup { get; set; }

        public bool isDDFieldEnabled { get; set; }

        public bool willStockShow { get; set; }

        public string maxFileSize { get; set; }

        public bool willKnowYourStateShow { get; set; }

        //Start Add two settings for Attachment in Invoice Mandatory and Order with Multiple Invoice Allowed
        public bool willAttachmentCompulsory { get; set; }
        public bool canAddBillingFromBillingList { get; set; }
        //End Add two settings for Attachment in Invoice Mandatory and Order with Multiple Invoice Allowed

        //Start Add three settings for Primary found plan
        public bool willShowUpdateDayPlan { get; set; }
        public string updateDayPlanText { get; set; }
        public string dailyPlanListHeaderText { get; set; }
        //End Add three settings for Primary found plan

        //Start Add one settings for add shop header text
        public string addShopHeaderText { get; set; }
        //End Add one settings for add shop header text

        //Start Add one settings for plan all list header text
        public string allPlanListHeaderText { get; set; }
        //End Add one settings for plan all list header text

        //Start rev for set youer todays target visible or not
        public bool willSetYourTodaysTargetVisible { get; set; }
        //End rev for set youer todays target visible or not

        //Start rev for set youer todays target visible or not
        public String attendenceAlertHeading { get; set; }
        public String attendenceAlertText { get; set; }
        //End rev for set youer todays target visible or not

        //Start rev for MEETING HEADER TEXT
        public String meetingText { get; set; }
        //End rev for set MEETING HEADER TEXT

        //Start rev for MEETING distance to complete meeting
        public String meetingDistance { get; set; }
        //End rev for set MEETING distance to complete meeting

        //Start rev for Active PJP Feature
        public bool isActivatePJPFeature { get; set; }
        //End rev for Active PJP Feature

        //Start rev for Reimbursement Feature show hide in app
        public bool willReimbursementShow { get; set; }
        //End rev for Reimbursement Feature show hide in app

        //Start rev for order to billing button Text
        public String updateBillingText { get; set; }
        //End rev for order to billing button Text

        //Start rev for Product Rater online offline
        public bool isRateOnline { get; set; }
        //End rev for Product Rater online offline

        //Start rev for PP Text and DD Text
        public String ppText { get; set; }
        public String ddText { get; set; }
        //End rev for PP Text and DD Text

        //Start rev for Shop name Replace with given Text
        public String shopText { get; set; }
        //End rev for Shop name Replace with given Text

        //Start rev for Customer feature and area in APP
        public bool isCustomerFeatureEnable { get; set; }
        public bool isAreaVisible { get; set; }
        //End rev for Customer feature and area in APP

        //Start rev for GST Percentage for Rajgarhia
        public String CGSTPercentage { get; set; }
        public String SGSTPercentage { get; set; }
        public String TCSPercentage { get; set; }
        //End rev for GST Percentage for Rajgarhia

        //Start rev for docAttachmentNo in APP
        public Int32 docAttachmentNo { get; set; }
        //End rev for docAttachmentNo in APP

        //Start rev for chatBotMsg in APP
        public String chatBotMsg { get; set; }
        //End rev for chatBotMsg in APP

        //Start rev for contactMail in APP
        public String contactMail { get; set; }
        //End rev for contactMail in APP

        //Start rev for bot voice in APP
        public bool isVoiceEnabledForAttendanceSubmit { get; set; }
        public bool isVoiceEnabledForOrderSaved { get; set; }
        public bool isVoiceEnabledForInvoiceSaved { get; set; }
        public bool isVoiceEnabledForCollectionSaved { get; set; }
        public bool isVoiceEnabledForHelpAndTipsInBot { get; set; }
        //End rev for bot voice in APP
        //Rev Debashis
        public bool GPSAlert { get; set; }
        public bool IsDuplicateShopContactnoAllowedOnline { get; set; }
        public bool BatterySetting { get; set; }
        public bool PowerSaverSetting { get; set; }
        public bool IsnewleadtypeforRuby { get; set; }
        public bool Show_App_Logout_Notification { get; set; }
        public bool IsReturnActivatedforPP { get; set; }
        public bool IsReturnActivatedforDD { get; set; }
        public bool IsReturnActivatedforSHOP { get; set; }
        public bool MRPInOrder { get; set; }
        public bool FaceRegistrationFrontCamera { get; set; }
        public bool IsShowMyDetails { get; set; }
        public bool IsAttendVisitShowInDashboard { get; set; }
        public bool IsShowInPortalManualPhotoRegn { get; set; }
        public decimal SqMtrRateCalculationforQuotEuro { get; set; }
        public string NewQuotationRateCaption { get; set; }
        public bool NewQuotationShowTermsAndCondition { get; set; }
        public bool IsCollectionEntryConsiderOrderOrInvoice { get; set; }
        public string contactNameText { get; set; }
        public string contactNumberText { get; set; }
        public string emailText { get; set; }
        public string dobText { get; set; }
        public string dateOfAnniversaryText { get; set; }
        public bool ShopScreenAftVisitRevisit { get; set; }
        public bool IsSurveyRequiredforNewParty { get; set; }
        public bool IsSurveyRequiredforDealer { get; set; }
        public bool IsShowHomeLocationMap { get; set; }
        public bool IsBeatRouteAvailableinAttendance { get; set; }
        public bool IsAllBeatAvailable { get; set; }
        public string BeatText { get; set; }
        public string TodaysTaskText { get; set; }
        public bool IsDistributorSelectionRequiredinAttendance { get; set; }
        public bool IsAllowNearbyshopWithBeat { get; set; }
        public bool IsGSTINPANEnableInShop { get; set; }
        public bool IsMultipleImagesRequired { get; set; }
        public bool IsALLDDRequiredforAttendance { get; set; }
        public bool IsShowNewOrderCart { get; set; }
        public bool IsmanualInOutTimeRequired { get; set; }
        public string surveytext { get; set; }
        public bool IsDiscountInOrder { get; set; }
        public bool IsViewMRPInOrder { get; set; }
        public bool IsShowStateInTeam { get; set; }
        public bool IsShowBranchInTeam { get; set; }
        public bool IsShowDesignationInTeam { get; set; }
        public bool IsAllowZeroRateOrder { get; set; }
        //End of Rev Debashis
        //Rev 1.0 Row:797
        public bool IsBeatAvailable { get; set; }
        //End of Rev 1.0 Row:797
        //Rev 2.0 Row:808
        public bool isExpenseFeatureAvailable { get; set; }
        //End of Rev 2.0 Row:808
        //Rev 3.0 Row:809
        public bool IsDiscountEditableInOrder { get; set; }
        public bool IsRouteStartFromAttendance { get; set; }
        //End of Rev 3.0 Row:809
        //Rev 4.0 Row:812
        public bool IsShowOtherInfoinShopMaster { get; set; }
        public bool IsShowQuotationFooterforEurobond { get; set; }
        //End of Rev 4.0 Row:812
        //Rev 5.0 Row:815
        public bool ShowApproxDistanceInNearbyShopList { get; set; }
        //End of Rev 5.0 Row:815
        //Rev 6.0 Row:817
        public bool IsAssignedDDAvailableForAllUser { get; set; }
        //End of Rev 6.0 Row:817
        //Rev 7.0 Row:824
        public bool IsShowEmployeePerformance { get; set; }
        //End of Rev 7.0 Row:824
        //Rev 8.0 Row:825,826,827
        public bool IsShowPrivacyPolicyInMenu { get; set; }
        public bool IsAttendanceCheckedforExpense { get; set; }
        public bool IsShowLocalinExpense { get; set; }
        public bool IsShowOutStationinExpense { get; set; }
        public bool IsTAAttachment1Mandatory { get; set; }
        public bool IsTAAttachment2Mandatory { get; set; }
        public bool IsSingleDayTAApplyRestriction { get; set; }
        public string NameforConveyanceAttachment1 { get; set; }
        public string NameforConveyanceAttachment2 { get; set; }
        public bool IsTaskManagementAvailable { get; set; }
        //End of Rev 8.0 Row:825,826,827
        //Rev 9.0 Row:833
        public bool IsAttachmentAvailableForCurrentStock { get; set; }
        //End of Rev 9.0 Row:833
        //Rev 10.0 Row:838
        public bool IsShowReimbursementTypeInAttendance { get; set; }
        //End of Rev 10.0 Row:838
        //Rev 11.0 Row:841
        public bool IsBeatPlanAvailable { get; set; }
        //End of Rev 11.0 Row:841
        //Rev 12.0 Row:848
        public bool IsUpdateVisitDataInTodayTable { get; set; }
        //End of Rev 12.0 Row:848
        //Rev 13.0 Row:851
        public bool ConsiderInactiveShopWhileLogin { get; set; }
        //End of Rev 13.0 Row:851
        //Rev 14.0 Row:856
        public string ShopSyncIntervalInMinutes { get; set; }
        //End of Rev 14.0 Row:856
        //Rev 15.0 Row:860
        public bool IsShowWhatsAppIconforVisit { get; set; }
        public bool IsAutomatedWhatsAppSendforRevisit { get; set; }
        //End of Rev 15.0 Row:860
        //Rev 16.0 Row:863
        public bool IsAllowBackdatedOrderEntry { get; set; }
        public int Order_Past_Days { get; set; }
        //End of Rev 16.0 Row:863
        //Rev 17.0 Row:864
        public bool Show_distributor_scheme_with_Product { get; set; }
        //End of Rev 17.0 Row:864
        //Rev 18.0 Row:871
        public bool GSTINPANMandatoryforSHOPTYPE4 { get; set; }
        public bool FSSAILicNoEnableInShop { get; set; }
        public bool FSSAILicNoMandatoryInShop4 { get; set; }
        //End of Rev 18.0 Row:871
        //Rev 19.0 Row:879
        public bool isLeadContactNumber { get; set; }
        public bool isModelEnable { get; set; }
        public bool isPrimaryApplicationEnable { get; set; }
        public bool isSecondaryApplicationEnable { get; set; }
        public bool isBookingAmount { get; set; }
        public bool isLeadTypeEnable { get; set; }
        public bool isStageEnable { get; set; }
        public bool isFunnelStageEnable { get; set; }
        //End of Rev 19.0 Row:879
        //Rev 20.0 Row:866
        public int MultiVisitIntervalInMinutes { get; set; }
        //End of Rev 20.0 Row:866
        //Rev 21.0 Row:891
        public bool IsGPSRouteSync { get; set; }
        public bool IsSyncBellNotificationInApp { get; set; }
        //End of Rev 21.0 Row:891
        //Rev 22.0 Row: 899 & Refer: 0027139
        public bool IsShowCustomerLocationShare { get; set; }
        //End of Rev 22.0 Row: 899 & Refer: 0027139
        //Rev 23.0 Row: 900 & Refer: 0027245
        public bool AdditionalInfoRequiredForTimelines { get; set; }
        //End of Rev 23.0 Row: 900 & Refer: 0027245
        //Rev 24.0 Row: 901 & Refer: 0027279,0027285 & 0027282
        public bool ShowPartyWithGeoFence { get; set; }
        public bool ShowPartyWithCreateOrder { get; set; }
        public string Allow_past_days_for_apply_reimbursement { get; set; }
        //End of Rev 24.0 Row: 901 & Refer: 0027279,0027285 & 0027282
        //Rev 25.0 Row: 903
        public string loc_k { get; set; }
        public string firebase_k { get; set; }
        //End of Rev 25.0 Row: 903
        //Rev 26.0 Row: 904
        public bool IsShowLeaderBoard { get; set; }
        //End of Rev 26.0 Row: 904
        //Rev 27.0 Row: 968
        public int QuestionAfterNoOfContentForLMS { get; set; }
        public bool IsAllowGPSTrackingInBackgroundForLMS { get; set; }
        //End of Rev 27.0 Row: 968
        //Rev 28.0 Row: 979
        public bool IsRetailOrderStatusRequired { get; set; }
        //End of Rev 28.0 Row: 979
        //Rev 29.0 Row: 981
        public bool IsVideoAutoPlayInLMS { get; set; }
        //End of Rev 29.0 Row: 981
        //Rev 30.0 Row: 983
        public bool IsStockCheckFeatureOn { get; set; }
        public bool IsShowDistributorWiseCurrentStockInOrder { get; set; }
        public bool IsAllowNegativeStock { get; set; }
        public bool StockCheckOnOrder1OrInvioce0 { get; set; }
        //End of Rev 30.0 Row: 983
        //Rev 31.0 Row: 985
        public string AllowedCreditDays { get; set; }
        public bool WillCreditDaysFollow { get; set; }
        public bool AllowOrderOnOutstandingAndClosingStockDifference { get; set; }
        //End of Rev 31.0 Row: 985
        //Rev 32.0 Row: 987
        public bool ShowRetryIncorrectQuiz { get; set; }
        //End of Rev 32.0 Row: 987
        //Rev 33.0 Row: 1005
        public bool WillShowLoanDetailsInParty { get; set; }
        //End of Rev 33.0 Row: 1005
    }

    public class ConfigurationModelInput
    {
        public string user_id { get; set; }

    }

    public class ConfigurationModelUser
    {

        public string status { get; set; }

        public string message { get; set; }

        public List<ConfigurationUser> getconfigure { get; set; }

    }

    public class ConfigurationUser
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }

    public class MeetingtypeInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class MeetingTypeOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<meetingList> meeting_type_list { get; set; }
    }

    public class meetingList
    {
        public string type_id { get; set; }
        public String type_text { get; set; }
    }

    public class LoginSettingsInput
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }

    public class LoginSettingsOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public bool isFingerPrintMandatoryForAttendance { get; set; }
        public bool isFingerPrintMandatoryForVisit { get; set; }
        public bool isSelfieMandatoryForAttendance { get; set; }
        public bool isAddAttendence { get; set; }
    }
}