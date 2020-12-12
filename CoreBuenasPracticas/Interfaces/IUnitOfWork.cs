using CoreBuenasPracticas.Entities;
using System;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IPostRepository PostRepository { get; }
        IRepository<User> UserRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        ISecurityRepository SecurityRepository { get; }
        void saveChanges();
        Task saveChangesAsync();
    }
}
