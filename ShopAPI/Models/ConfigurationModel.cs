#region======================================Revision History=========================================================
//1.0   V2.0.32     Debashis    17/01/2023      A new parameter has been added.Row: 797
//2.0   V2.0.38     Debashis    25/01/2023      A new parameter has been added.Row: 808
//3.0   V2.0.38     Debashis    02/02/2023      Some new parameters have been added.Row: 809
//4.0   V2.0.38     Debashis    08/02/2023      Some new parameter has been added.Row: 812
//5.0   V2.0.38     Debashis    22/03/2023      A new parameter has been added.Row: 815
#endregion===================================End of Revision History==================================================
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