using System.Diagnostics.CodeAnalysis;

namespace Project_Pinterest.Entities
{
    public class Relationship : BaseEntity
    {
        [MaybeNull]
        public User? Follower { get; set; }
        [MaybeNull]
        public User? Following { get; set; }
    }
}
