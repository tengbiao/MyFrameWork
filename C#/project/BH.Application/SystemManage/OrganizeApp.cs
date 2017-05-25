using BH.Application.Dto;
using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BH.Application.SystemManage
{
    public class OrganizeApp : IOrganizeApp
    {
        private readonly IRepository<Sys_Organize> _repository;
        public OrganizeApp(IRepository<Sys_Organize> repository)
        {
            _repository = repository;
        }

        public List<OrganizaDto> GetList()
        {
            return _repository.IQueryable().OrderBy(t => t.F_CreatorTime).MapToList<OrganizaDto>();
        }
        public OrganizaDto GetForm(string keyValue)
        {
            return _repository.FindKey(keyValue).MapTo<OrganizaDto>();
        }
        public void DeleteForm(string keyValue)
        {
            if (_repository.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                _repository.Delete(t => t.F_Id == keyValue);
            }
        }
        public void SubmitForm(OrganizaDto organizaInputDto, string keyValue)
        {
            var model = organizaInputDto.MapTo<Sys_Organize>();
            if (!string.IsNullOrEmpty(keyValue))
            {
                model.Modify(keyValue);
                _repository.Update(model);
            }
            else
            {
                model.Create();
                _repository.Insert(model);
            }
        }
    }
}
