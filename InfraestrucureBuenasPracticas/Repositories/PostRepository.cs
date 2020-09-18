using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Interfaces;
using InfraestructureBuenasPracticas.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureBuenasPracticas.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository //se hereda del repositorio base
    {
        //como el repositorio base tiene un constructor el cual pide un contexto, se crea un constructor
        //de post repository y se recibe un objeto context , la palabra :base lo que hace es enviarlo al
        //constructor de base repository el objeto context
        public PostRepository(SocialMediaContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Post>> GetPostsByUser(int userId)
        {
            return await _entities.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
