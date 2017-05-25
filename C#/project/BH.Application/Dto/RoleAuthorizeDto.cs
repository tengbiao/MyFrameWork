using BH.Domain.Infrastructure;
using System;

namespace BH.Application.Dto
{
    public class RoleAuthorizeDto
    {
        public string F_Id { get; set; }
        public int? F_ItemType { get; set; }
        public string F_ItemId { get; set; }
        public int? F_ObjectType { get; set; }
        public string F_ObjectId { get; set; }
        public int? F_SortCode { get; set; }
    }
}
