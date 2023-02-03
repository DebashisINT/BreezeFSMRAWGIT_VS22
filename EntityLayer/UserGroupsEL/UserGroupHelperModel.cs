﻿/*************************************************************************************************************
Rev 1.0     Sanchita   V2.0.28    27/01/2023      Bulk modification feature is required in Parties menu. Refer: 25609
*****************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.UserGroupsEL
{
    public class UserGroupHelperModel
    {
        public string mode { get; set; }
    }

    public class GetUserGroupModel
    {
        public int grp_id { get; set; }

        public string grp_name { get; set; }
    }

    public class TranAccessByGroupModel
    {
        public int MenuId { get; set; }

        public bool CanAdd { get; set; }

        public bool CanEdit { get; set; }

        public bool CanDelete { get; set; }

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
    }

    public class UserGroupSaveModel
    {
        public int grp_id { get; set; }

        public int grp_segmentId { get; set; }

        public string grp_name { get; set; }

        public int? CreateUser { get; set; }

        public int? LastModifyUser { get; set; }

        public string UserGroupRights { get; set; }

        public string mode { get; set; }
    }

    public class GroupUserListModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public int UserGroupId { get; set; }
    }

    public class GetContactPersListModel
    {

        public string ContactId { get; set; }

        public string name { get; set; }
    }
}
