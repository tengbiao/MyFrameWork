using System.Collections.Generic;
using BH.Domain.Entity.SystemSecurity;

namespace BH.IApplication
{
    public interface IFilterIPApp
    {
        void DeleteForm(string keyValue);
        FilterIPEntity GetForm(string keyValue);
        List<FilterIPEntity> GetList(string keyword);
        void SubmitForm(FilterIPEntity filterIPEntity, string keyValue);
    }
}