using BH.Application.Dto;
using BH.Code;
using BH.Data;
using BH.Domain.Entity;
using BH.IApplication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BH.Application.SystemSecurity
{
    public class LogApp : ILogApp
    {
        private readonly IRepository<Sys_Log> _repository;
        public LogApp(IRepository<Sys_Log> repository)
        {
            this._repository = repository;
        }

        public async Task<List<LogDto>> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<Sys_Log>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.F_Account.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty())
            {
                string timeType = queryParam["timeType"].ToString();
                DateTime startTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                switch (timeType)
                {
                    case "1":
                        break;
                    case "2":
                        startTime = DateTime.Now.AddDays(-7);
                        break;
                    case "3":
                        startTime = DateTime.Now.AddMonths(-1);
                        break;
                    case "4":
                        startTime = DateTime.Now.AddMonths(-3);
                        break;
                    default:
                        break;
                }
                expression = expression.And(t => t.F_Date >= startTime && t.F_Date <= endTime);
            }
            return (await _repository.FindListAsync(expression, pagination)).MapToList<LogDto>();
        }
        public async Task<int> RemoveLog(string keepTime)
        {
            DateTime operateTime = DateTime.Now;
            if (keepTime == "7")            //保留近一周
            {
                operateTime = DateTime.Now.AddDays(-7);
            }
            else if (keepTime == "1")       //保留近一个月
            {
                operateTime = DateTime.Now.AddMonths(-1);
            }
            else if (keepTime == "3")       //保留近三个月
            {
                operateTime = DateTime.Now.AddMonths(-3);
            }
            var expression = ExtLinq.True<Sys_Log>();
            expression = expression.And(t => t.F_Date <= operateTime);
            return await _repository.DeleteAsync(expression);
        }
        public async Task<int> WriteDbLog(bool result, string resultLog)
        {
            Sys_Log Sys_Log = new Sys_Log();
            Sys_Log.F_Id = Common.GuId();
            Sys_Log.F_Date = DateTime.Now;
            Sys_Log.F_Account = OperatorProvider.Provider.GetCurrent().UserCode;
            Sys_Log.F_NickName = OperatorProvider.Provider.GetCurrent().UserName;
            Sys_Log.F_IPAddress = Net.Ip;
            Sys_Log.F_IPAddressName = Net.GetLocation(Sys_Log.F_IPAddress);
            Sys_Log.F_Result = result;
            Sys_Log.F_Description = resultLog;
            Sys_Log.Create();
            await _repository.InsertAsync(Sys_Log);
            return 1;
        }
        public async Task<int> WriteDbLog(LogDto logInputDto)
        {
            var model = logInputDto.MapTo<Sys_Log>();
            model.F_Id = Common.GuId();
            model.F_Date = DateTime.Now;
            model.F_IPAddress = Net.Ip;
            model.F_IPAddressName = Net.GetLocation(model.F_IPAddress);
            model.Create();
            await _repository.InsertAsync(model);
            return 1;
        }
    }
}
