/********************************************************************************************************************
 * 1.0       10/09/2024        V2.0.48          Sanchita          27690: Quotation Notification issue @ Eurobond
 * *******************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class ChatListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

    }

    public class ChatListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ChatList> chat_user_list { get; set; }

    }

    public class ChatList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string isGroup { get; set; }
        public string image { get; set; }
        public string last_msg { get; set; }
        public DateTime last_msg_time { get; set; }
        public string last_msg_user_id { get; set; }
        public string last_msg_user_name { get; set; }

    }


    public class GroupChatListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<GroupChatList> group_user_list { get; set; }

    }

    public class GroupChatList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image { get; set; }
    }

    public class ChatSendInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string msg_id { get; set; }
        public string msg { get; set; }
        public string to_id { get; set; }
        public string time { get; set; }
        public string user_name { get; set; }



    }

    public class ChatSendOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    public class ChatListGetInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string opponent_id { get; set; }
        public string page_no { get; set; }
        public string page_count { get; set; }
    }

    public class ChatListGetOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<ChatListGet> chat_list { get; set; }


    }

    public class ChatListGet
    {
        public string id { get; set; }
        public string msg { get; set; }
        public DateTime time { get; set; }
        public string from_id { get; set; }
        public string from_name { get; set; }
        public string status { get; set; }
    }


    public class AddGroupInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string grp_name { get; set; }
        public string ids { get; set; }

    }

    public class AddGroupOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    public class AddMemberToGroupInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string grp_id { get; set; }
        public string ids { get; set; }

    }

    public class AddMemberToGroupOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    public class GroupSelectedUserInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string grp_id { get; set; }

    }

    public class GroupSelectedUserOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<GroupChatList> group_user_list { get; set; }

    }

    public class UpdateStatusInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string to_id { get; set; }

    }

    public class UpdateStatusOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    // Rev 1.0
    public class Data
    {

        public string body { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string header { get; set; }
        public string type { get; set; }
        public string msg_id { get; set; }
        public string msg { get; set; }
        public string time { get; set; }

        public string from_user_id { get; set; }
        public string isGroup { get; set; }
        public string from_id { get; set; }
        public string from_name { get; set; }
        public string from_user_name { get; set; }
    }

    public class Message
    {
        public string token { get; set; }
        public Data data { get; set; }
        public Notification notification { get; set; }
    }

    public class Notification
    {
        public string title { get; set; }
        public string body { get; set; }
    }

    public class Root
    {
        public Message message { get; set; }

    }
    // End of Rev 1.0

}
