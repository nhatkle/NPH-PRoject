﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.EF
{
    public class ProductViewModel
    {
        public int ID { set; get; }
        public string Images { set; get; }
        public string Name { set; get; }
        public decimal? Price { set; get; }
        public string CateName { set; get; }
        public string CateMetaTitle { set; get; }
        public string MetaTitle { set; get; }
        public DateTime? CreatedDate { set; get; }
    }
}
