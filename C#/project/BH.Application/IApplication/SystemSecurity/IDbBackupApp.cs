using BH.Application.Dto;
using System.Collections.Generic;

namespace BH.IApplication
{
    public interface IDbBackupApp
    {
        void DeleteForm(string keyValue);
        DbBackupDto GetForm(string keyValue);
        List<DbBackupDto> GetList(string queryJson);
        void SubmitForm(DbBackupDto dbBackupInputDto);
    }
}