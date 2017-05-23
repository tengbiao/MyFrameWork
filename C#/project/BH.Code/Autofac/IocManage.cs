using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;
using Autofac;

namespace BH.Code.Autofac1
{
    public class IocManage
    {
        public static T Resolve<T>()
        {
            return AutofacDependencyResolver.Current.ApplicationContainer.Resolve<T>();
        }
    }
}
