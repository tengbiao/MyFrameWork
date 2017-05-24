using BH.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Application.IApplication
{
    public interface IAreaApp
    {
        List<Sys_Area> GetList();
        Task<Sys_Area> GetForm(string keyValue);
        void DeleteForm(string keyValue);
        void SubmitForm(Sys_Area areaEntity, string keyValue);
    }
}
