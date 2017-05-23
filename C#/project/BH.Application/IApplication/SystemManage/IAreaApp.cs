using BH.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Application.IApplication
{
    public interface IAreaApp
    {
        List<AreaEntity> GetList();
        Task<AreaEntity> GetForm(string keyValue);
        void DeleteForm(string keyValue);
        void SubmitForm(AreaEntity areaEntity, string keyValue);
    }
}
