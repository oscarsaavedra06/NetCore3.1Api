using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.QueryFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Interfaces
{
    public interface IPostService
    {
        IEnumerable<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPost(int id);
        Task InsertPost(Post post);
        Task<bool> Updatepost(Post post);
        Task<bool> Deletepost(int id);
    }
}