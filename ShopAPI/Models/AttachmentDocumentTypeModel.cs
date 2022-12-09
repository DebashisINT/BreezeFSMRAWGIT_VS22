using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class AttachmentDocumentTypeModel
    {
        [Required]
        public String session_token { get; set; }
        [Required]
        public String user_id { get; set; }
        //Rev Debashis
        public String type_id { get; set; }
        //End of Rev Debashis
    }

    public class AttachmentDocumentTypeList
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<DocumentType> type_list { get; set; }
    }

    public class DocumentType
    {
        public String id { get; set; }
        public String type { get; set; }
        public String image { get; set; }
        //Rev Debashis
        public bool IsForOrganization { get; set; }
        public bool IsForOwn { get; set; }
        //End of Rev Debashis
    }

    public class AttachDocumnetModel
    {
        public string data { get; set; }
        public List<HttpPostedFileBase> attachment { get; set; }
    }

    public class AttachDocumnetData
    {
        public string id { get; set; }
        public string type_id { get; set; }
        public string date_time { get; set; }
        public string attachment { get; set; }
    }



    public class AttachmentDocumentList
    {
        public String status { get; set; }
        public String message { get; set; }
        public List<Documentes> doc_list { get; set; }
    }

    public class Documentes
    {
        public String id { get; set; }
        public String type_id { get; set; }
        public DateTime date_time { get; set; }
        public String attachment { get; set; }
        //Rev Debashis
        public String document_name { get; set; }
        //End of Rev Debashis
    }

    public class AttachmentDocumentDeleteInput
    {
        [Required]
        public String session_token { get; set; }
        [Required]
        public String user_id { get; set; }
        [Required]
        public String id { get; set; }
    }

    public class AttachmentDocumentDeleteOutput
    {
        public String status { get; set; }
        public String message { get; set; }
    }
}