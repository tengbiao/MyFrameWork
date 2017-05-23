using System;

namespace BH.Domain.Infrastructure
{
    public class CreationAudited<KeyType> : EntityBase<KeyType>, ICreationAudited<KeyType>
    {
        public string F_CreatorUserId { get; set; }
        public DateTime? F_CreatorTime { get; set; }
    }

    public class CreationAudited : CreationAudited<string>
    {

    }
}