﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShop.Models
{
    public class ErpSettingList
    {
        public List<ErpSettProp> ErpSettProp { get; set; }
    }

    public class ErpSettProp
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}