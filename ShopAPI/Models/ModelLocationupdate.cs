using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace ShopAPI.Models
{
    public class ModelLocationupdate
    {
        public string session_token { get; set; }
        public string user_id { get; set; }

        public List<Locationupdate> location_details { get; set; }

    }


    public class Locationupdate
    {

        public string location_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }

        public string distance_covered { get; set; }

        public string last_update_time { get; set; }
        public string date { get; set; }
        public string shops_covered { get; set; }
        //extra input for meeting attend start
        public string meeting_attended { get; set; }
        //extra input for meeting attend end
        public string home_distance { get; set; }

        public string network_status { get; set; }
        public string battery_percentage { get; set; }
        public string home_duration { get; set; }

    }

    public class LocationupdateOutput
    {
        public string status { get; set; }
        public string message { get; set; }
      
    }

    public class XmlConversion
    {
        #region ******************************************** Xml Conversion  ********************************************
        public static string ConvertToXml<T>(List<T> table, int metaIndex = 0)
        {
            XmlDocument ChoiceXML = new XmlDocument();
            ChoiceXML.AppendChild(ChoiceXML.CreateElement("root"));
            Type temp = typeof(T);

            foreach (var item in table)
            {
                XmlElement element = ChoiceXML.CreateElement("data");

                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    element.AppendChild(ChoiceXML.CreateElement(pro.Name)).InnerText = Convert.ToString(item.GetType().GetProperty(pro.Name).GetValue(item, null));
                }
                ChoiceXML.DocumentElement.AppendChild(element);
            }

            return ChoiceXML.InnerXml.ToString();
        }


        #endregion

    }

}