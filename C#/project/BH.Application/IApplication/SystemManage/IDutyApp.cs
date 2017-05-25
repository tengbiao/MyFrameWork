using System.Collections.Generic;
using BH.Domain.Entity;
using BH.Application.Dto;

namespace BH.IApplication
{
    public interface IDutyApp
    {
        void DeleteForm(string keyValue);
        DutyDto GetForm(string keyValue);
        List<DutyDto> GetList(string keyword = "");
        void SubmitForm(DutyDto dutyInput, string keyValue);
    }
}