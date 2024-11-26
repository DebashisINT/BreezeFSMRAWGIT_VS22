/*************************************************************************************************************
Rev 1.0     Sanchita   V2.0.28    27/01/2023      Bulk modification feature is required in Parties menu. Refer: 25609
Rev 2.0     Sanchita   V2.0.44    19/12/2023      Beat related tab will be added in the security roles of Parties. Mantis: 27080 
Rev 3.0     Sanchita   V2.0.47    30/05/2024      Mass Delete related tabs will be added in the security roles of Parties. Mantis: 27489 
Rev 4.0     Sanchita   V2.0.47    03/06/2024      27500: Attendance/ Leave Clear tab need to add in security Role of "Users" 
Rev 5.0     Sanchuta   V2.0.49    17/09/2024      27698: Customization work of New Order Status Update module    
*****************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.CommonELS
{
    public class CommonEL
    {
        public int? userid { get; set; }

        public int? usergroupid { get; set; }

        public string url { get; set; }
    }

    public class UserRightsForPage
    {
        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

        public bool CanAdd { get; set; }

        public bool CanView { get; set; }
        public bool CanIndustry { get; set; }

        public bool CanCreateActivity { get; set; }

        public bool CanContactPerson { get; set; }

        public bool CanHistory { get; set; }

        public bool CanAddUpdateDocuments { get; set; }
        public bool CanMembers { get; set; }
        public bool CanOpeningAddUpdate { get; set; }
        public bool CanAssetDetails { get; set; }
        public bool CanExport { get; set; }
        public bool CanPrint { get; set; }

        public bool CanBudget { get; set; }
        public bool CanAssignbranch { get; set; }

        public bool Cancancelassignmnt { get; set; }
        public bool CanReassign { get; set; }
        public bool CanClose { get; set; }
        public bool CanSpecialEdit { get; set; }

        public bool CanCancel { get; set; }
        //Mantis Issue 24832
        public bool CanAssign { get; set; }
        //End of Mantis Issue 24832
        // Rev 1.0
        public bool CanBulkUpdate { get; set; }
        // End of Rev 1.0
        // Rev 2.0
        public bool CanReassignedBeatParty { get; set; }
        public bool CanReassignedBeatPartyLog { get; set; }
        public bool CanReassignedAreaRouteBeat { get; set; }
        public bool CanReassignedAreaRouteBeatLog { get; set; }

        // End of Rev 2.0
        // Rev 3.0
        public bool CanMassDelete { get; set; }
        public bool CanMassDeleteDownloadImport { get; set; }
        // End of Rev 3.0
        // Rev 4.0
        public bool CanAttendanceLeaveClear { get; set; }
        // End of Rev 4.0
        // Rev 5.0
        public bool CanInvoice { get; set; }
        public bool CanReadyToDispatch { get; set; }
        public bool CanDispatch { get; set; }
        public bool CanDeliver { get; set; }
        // End of Rev 5.0

    }
    public  class RightEL
    {
        public RightEL()
        {
            //this.Map_UserGroup_Rights = new HashSet<Map_UserGroup_RightsEL>();
        }

        public int Id { get; set; }
        public string Rights { get; set; }
        public Nullable<bool> IsActive { get; set; }
      
    }
}
