using BH.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Infrastructure
{
    public class EntityBase<KeyType> : IEntityBase<KeyType>
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public KeyType F_Id { set; get; }

        public void Create()
        {
            dynamic entity = GetEntityCurrent();
            entity.F_Id = Common.GuId();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_CreatorUserId = LoginInfo.UserId;
            }
            entity.F_CreatorTime = DateTime.Now;
        }

        public void Modify(KeyType keyValue)
        {
            dynamic entity = GetEntityCurrent();
            entity.F_Id = keyValue;
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_LastModifyUserId = LoginInfo.UserId;
            }
            entity.F_LastModifyTime = DateTime.Now;
        }

        public void Remove()
        {
            dynamic entity = GetEntityCurrent();
            var LoginInfo = OperatorProvider.Provider.GetCurrent();
            if (LoginInfo != null)
            {
                entity.F_DeleteUserId = LoginInfo.UserId;
            }
            entity.F_DeleteTime = DateTime.Now;
            entity.F_DeleteMark = true;
        }

        private dynamic GetEntityCurrent()
        {
            dynamic entity = null;
            if ((this as IFullAudited<KeyType>) != null)
                entity = this as IFullAudited;
            else if ((this as ICreationAudited<KeyType>) != null)
                entity = this as ICreationAudited<KeyType>;
            else if ((this as IModificationAudited<KeyType>) != null)
                entity = this as IModificationAudited<KeyType>;
            else if ((this as IDeleteAudited<KeyType>) != null)
                entity = this as IDeleteAudited<KeyType>;
            return entity;
        }
    }

    public class EntityBase : EntityBase<string>
    {

    }
}
