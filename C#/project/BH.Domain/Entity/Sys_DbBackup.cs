using BH.Domain.Infrastructure;
using System;

namespace BH.Domain.Entity
{
    public class Sys_FilterIP : FullAudited
    {
        public bool? F_Type { get; set; }
        public string F_StartIP { get; set; }
        public string F_EndIP { get; set; }
        public int? F_SortCode { get; set; }
        public bool? F_EnabledMark { get; set; }
        public string F_Description { get; set; }
    }
}
