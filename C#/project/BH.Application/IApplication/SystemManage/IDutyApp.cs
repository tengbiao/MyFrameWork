﻿using System.Collections.Generic;
using BH.Domain.Entity;

namespace BH.IApplication
{
    public interface IDutyApp
    {
        void DeleteForm(string keyValue);
        Sys_Role GetForm(string keyValue);
        List<Sys_Role> GetList(string keyword = "");
        void SubmitForm(Sys_Role Sys_Role, string keyValue);
    }
}