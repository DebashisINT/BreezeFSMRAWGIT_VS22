using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ChemistActivityModel
    {
        public string selectedusrid { get; set; }
        public List<string> StateId { get; set; }
        public List<string> empcode { get; set; }
        public string Is_PageLoad { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int Uniquecont { get; set; }
    }
}