using Autofac.Integration.Mvc;
using Autofac;

namespace BH.Code.Autofac
{
    public class IocManage
    {
        public static T Resolve<T>()
        {
            return AutofacDependencyResolver.Current.ApplicationContainer.Resolve<T>();
        }
    }
}
