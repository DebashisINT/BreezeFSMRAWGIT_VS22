using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class SystemsettingsModel
    {
        public string Description { get; set; }
        public string CONTROL_TYPE { get; set; }
        public string VALUE { get; set; }
        public string Key { get; set; }

        // Rev Sanchita
        public string ControlType { get; set; }
        public string txtValue { get; set; }
        public string selBitValue { get; set; }
        public string selValue { get; set; }
        public int numValue { get; set; }
        public string dtValue { get; set; }
        public string tmValue { get; set; }
        public string response_code { get; set; }
        public string response_msg { get; set; }
        // End of Rev Sanchita
    }
}