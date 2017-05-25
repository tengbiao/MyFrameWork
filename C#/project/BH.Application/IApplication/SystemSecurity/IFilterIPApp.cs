using System.Collections.Generic;
using BH.Domain.Entity;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IFilterIPApp
    {
        void DeleteForm(string keyValue);
        FilterIPDto GetForm(string keyValue);
        List<FilterIPDto> GetList(string keyword);
        void SubmitForm(FilterIPDto filterIpInputDto, string keyValue);
    }
}