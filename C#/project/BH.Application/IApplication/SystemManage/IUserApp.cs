using System.Collections.Generic;
using BH.Code;
using BH.Domain.Entity;
using BH.Application.Dto;
using System.Threading.Tasks;

namespace BH.IApplication
{
    public interface IUserApp
    {
        Task<UserDto> CheckLogin(string username, string password);
        Task<int> DeleteForm(string keyValue);
        Task<UserDto> GetForm(string keyValue);
        Task<List<UserDto>> GetList(Pagination pagination, string keyword);
        Task<int> SubmitForm(UserDto userInputDto, UserLogOnDto userLogOnInputDto, string keyValue);
        Task<int> UpdateForm(UserDto userInputDto);
    }
}