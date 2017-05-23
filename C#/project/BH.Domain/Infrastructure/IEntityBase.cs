using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Infrastructure
{
    public interface IEntityBase<KeyType>
    {
        KeyType F_Id { get; set; }
        void Create();
        void Modify(KeyType keyValue);
        void Remove();
    }

    public interface IEntityBase : IEntityBase<string>
    {

    }
}
