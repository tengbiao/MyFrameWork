﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Application.Dto
{
    public class AreaDto
    {
        public string F_Id { get; set; }
        public string F_ParentId { get; set; }
        public int? F_Layers { get; set; }
        public string F_EnCode { get; set; }
        public string F_FullName { get; set; }
        public string F_SimpleSpelling { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
    }
}
