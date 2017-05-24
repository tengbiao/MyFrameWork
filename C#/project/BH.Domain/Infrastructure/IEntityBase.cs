using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Domain.Infrastructure
{
    public interface IEntityBase<KeyType>
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        KeyType F_Id { get; set; }
        void Create();
        void Modify(KeyType keyValue);
        void Remove();
    }

    public interface IEntityBase : IEntityBase<string>
    {

    }
}
