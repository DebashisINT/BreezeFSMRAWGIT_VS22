using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopAPI.Models
{
    public class MicrolearningInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
    }

    public class MicrolearningOutput
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<Microlearning> micro_learning_list { get; set; }
    }

    public class Microlearning
    {
        public string id { get; set; }
        public string description { get; set; }
        public string category_name { get; set; }
        public string file_name { get; set; }
        public string file_size { get; set; }
        public string thumbnail { get; set; }
        public string note { get; set; }
        public bool isVideo { get; set; }
        public string current_window { get; set; }
        public bool play_when_ready { get; set; }
        public string play_back_position { get; set; }
        public string url { get; set; }
        public bool isDownloaded { get; set; }
        public string video_duration { get; set; }

    }


    public class NoteInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
        public string note { get; set; }
        public string date_time { get; set; }

    }

    public class NoteOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    public class VideoPositionInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
        public string current_window { get; set; }
        public string play_back_position { get; set; }
        public bool play_when_ready { get; set; }
        public string percentage { get; set; }

    }

    public class VideoPositionOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }

    public class UpdateViewInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
        public string open_date_time { get; set; }
        public string close_date_time { get; set; }
    }

    public class UpdateViewOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }
    public class DownloadHiostoryInput
    {
        public string session_token { get; set; }
        public string user_id { get; set; }
        public string id { get; set; }
        public bool isDownloaded { get; set; }
    }

    public class DownloadHiostoryOutput
    {
        public string status { get; set; }
        public string message { get; set; }

    }


}


