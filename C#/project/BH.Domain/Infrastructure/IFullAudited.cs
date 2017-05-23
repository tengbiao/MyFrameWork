using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Infrastructure
{
    public interface IFullAudited<KeyType> : IEntityBase<KeyType>, ICreationAudited<KeyType>, IModificationAudited<KeyType>, IDeleteAudited<KeyType>
    {

    }

    public interface IFullAudited : IFullAudited<string>
    {

    }
}
