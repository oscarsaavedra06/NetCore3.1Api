using CoreBuenasPracticas.CustomEntities;
using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.QueryFilters;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Interfaces
{
    public interface IPostService
    {
        PagedList<Post> GetPosts(PostQueryFilter filters);
        Task<Post> GetPost(int id);
        Task InsertPost(Post post);
        Task<bool> Updatepost(Post post);
        Task<bool> Deletepost(int id);
    }
}