using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS.Models
{
    public class LMSTopicsModel
    {
        public string Is_PageLoad { get; set; }
        public int TotalTopics { get; set; }
        public int TotalUsedTopics { get; set; }
        public int TotalUnusedTopics { get; set; }

        public int TopicBasedOnId { get; set; }
        public List<TopicBasedOnList> TopicBasedOnList { get; set; }

        public string Action {  get; set; }
        public string TopicID { get; set; }
        public string TopicName { get; set; }
        public string selectedTopicBasedOnMapList { get; set; }
        public string RETURN_VALUE { get; set; }
        public string RETURN_DUPLICATEMAPNAME { get; set; }

    }

    public class TopicBasedOnList
    {
        public String TOPICBASEDON_ID { get; set; }
        public String TOPICBASEDON_NAME { get; set; }
    }   

    public class TopicMapList
    {
        public string TOPICNAME {  get; set; }
        public string TOPICBASEDON_ID { get; set; }

        public string ID { get; set; }
        public string NAME { get; set; }
        public bool selected { get; set; }
        public string tabNameText { get; set; }
        public string rightTabNameText { get; set; }
        public bool searchPlaceholderText { get; set; }
    }

}