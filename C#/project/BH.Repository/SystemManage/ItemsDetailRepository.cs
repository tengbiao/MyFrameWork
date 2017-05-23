/*******************************************************************************
 * Copyright © 2016 BH.Framework 版权所有
 * Author: BH
 * Description: BH快速开发平台
 * Website：http://www.BH.cn
*********************************************************************************/
using BH.Data;
using BH.Domain.Entity.SystemManage;
using BH.Repository.IRepository.SystemManage;
using BH.Repository.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace BH.Repository.SystemManage
{
    public class ItemsDetailRepository : Repository<ItemsDetailEntity>, IItemsDetailRepository
    {
        public List<ItemsDetailEntity> GetItemList(string enCode)
        {
            return (from detail in this.Table
                    join item in Context.Set<ItemsEntity>()
                    on detail.F_ItemId equals item.F_Id
                    where item.F_EnCode == enCode && detail.F_EnabledMark.Value && !detail.F_DeleteMark.Value
                    orderby detail.F_SortCode ascending
                    select detail
             ).ToList();

            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(@"SELECT  d.*
            //                FROM    Sys_ItemsDetail d
            //                        INNER  JOIN Sys_Items i ON i.F_Id = d.F_ItemId
            //                WHERE   1 = 1
            //                        AND i.F_EnCode = @enCode
            //                        AND d.F_EnabledMark = 1
            //                        AND d.F_DeleteMark = 0
            //                ORDER BY d.F_SortCode ASC");
            //DbParameter[] parameter = 
            //{
            //     new SqlParameter("@enCode",enCode)
            //};
            //return this.FindList(strSql.ToString(), parameter);
        }
    }
}
