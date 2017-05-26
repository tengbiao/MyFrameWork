using BH.Code;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BH.Data
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    public class Repository<TEntity> : DbContextRepository<BHDbContext, TEntity>, IRepository<TEntity> where TEntity : class, new()
    {

    }
}
