using Project_Pinterest.Entities;

namespace Project_Pinterest.Constants
{
    public class BaseEntitySoftDeletable : BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}
