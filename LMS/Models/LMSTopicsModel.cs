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
        public string TopicStatus { get; set; }
        public string selectedTopicBasedOnMapList { get; set; }
        public string RETURN_VALUE { get; set; }
        public string RETURN_DUPLICATEMAPNAME { get; set; }
        public string RETURN_NEWASSIGN { get; set; }


        public string TopicStatusOld { get; set; }
        public string TopicCompDay { get; set; }
        public string DefaultTopic { get; set; }
        public string TopicSequence { get; set; }

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
        public string searchPlaceholderText { get; set; }
        public bool TOPICSTATUS { get; set; }
        public string TOPIC_COMP_DAY { get; set; }
        public bool TOPIC_ISDEFAULT { get; set; }
        public string TOPIC_SEQ { get; set; }


    }

    public class Data
    {

        public string body
        {
            get;
            set;
        }

        public string title
        {
            get;
            set;
        }

        public string key_1
        {
            get;
            set;
        }

        public string key_2
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string UserID
        {
            get;
            set;
        }

        public string header
        {
            get;
            set;
        }

        public string type
        {
            get;
            set;
        }

        public string imgNotification_Icon
        {
            get;
            set;
        }

    }

    public class Message
    {

        public string token
        {
            get;
            set;
        }

        public Data data
        {
            get;
            set;
        }

        public Notification notification
        {
            get;
            set;
        }

        
    }

    public class Notification
    {

        public string title
        {
            get;
            set;
        }

        public string body
        {
            get;
            set;
        }

    }

    public class Root
    {

        public Message message
        {
            get;
            set;
        }

    }
}