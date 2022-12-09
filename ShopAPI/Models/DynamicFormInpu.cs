using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class DynamicFormInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string dynamicFormName { get; set; }

    }
    public class DynamicFormEditInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string dynamicFormName { get; set; }
        public string id { get; set; }

    }

    public class DynamicFormSaveInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string dynamicFormName { get; set; }

        public List<DynamicFormView> view_list { get; set; }

    }

    public class DynamicFormSaveEditInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string dynamicFormName { get; set; }
        public string id { get; set; }
        public string attachments { get; set; }

        public List<DynamicFormView> view_list { get; set; }

    }
    public class DynamicFormSaveInputAttachment
    {
        public string data { get; set; }
        public HttpPostedFileBase attachments { get; set; }

    }

    public class DynamicFormView
    {
        public string grp_id { get; set; }
        public string id { get; set; }
        public string value { get; set; }

    }



    public class DynamicFormOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<view> view_list { get; set; }
    }

    public class DynamicFormSaveOutput
    {
        public string status { get; set; }
        public string message { get; set; }
    }



    public class FormList
    {
        public string id { get; set; }
        public string name { get; set; }

    }

    public class DynamicFormListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<FormList> form_list { get; set; }
    }


    public class FormWiseList
    {
        public string id { get; set; }
        public string super_id { get; set; }
        public string date { get; set; }
        public List<FormWiseItem> field_list { get; set; }

    }

    public class FormWiseItem
    {
        public string key { get; set; }

    }

    public class DynamicFormWiseListOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<FormWiseList> info_list { get; set; }
    }


    public class DynamicFormListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string dynamicFormName { get; set; }
    }
    public class DynamicFormEditListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string dynamicFormName { get; set; }
        public string id { get; set; }
    }
    public class DynamicFormWiseListInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string dynamicFormName { get; set; }
        public string id { get; set; }

    }

    public class view
    {
        public string id { get; set; }
        public string type { get; set; }
        public string header { get; set; }
        public string value { get; set; }
        public string text_type { get; set; }
        public string max_length { get; set; }

        public List<item> item_list { get; set; }

    }

    public class item
    {
        public string id { get; set; }
        public string items { get; set; }
        public string isSelected { get; set; }



    }

}