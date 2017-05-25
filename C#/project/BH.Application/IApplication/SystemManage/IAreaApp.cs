using BH.Application.Dto;
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
        List<AreaDto> GetList();
        Task<AreaDto> GetForm(string keyValue);
        void DeleteForm(string keyValue);
        void SubmitForm(AreaDto areaEntity, string keyValue);
    }
}
