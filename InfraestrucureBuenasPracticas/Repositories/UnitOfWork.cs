using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Interfaces;
using InfraestructureBuenasPracticas.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfraestructureBuenasPracticas.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SocialMediaContext _context;
        private readonly IPostRepository _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;
        public UnitOfWork(SocialMediaContext context)
        {
            _context = context;
        }
        //se vuelve al repositorio, ya que se necesitan métodos específicos, en la clase se hereda de Base
        //Repository para que se sigan teniendo los metodos crud de post
        public IPostRepository PostRepository => _postRepository ?? new PostRepository(_context);

        //se maneja repositorio base porque solo se necesitan los metodos crud
        public IRepository<User> UserRepository => _userRepository ?? new BaseRepository<User>(_context);

        public IRepository<Comment> CommentRepository => _commentRepository ?? new BaseRepository<Comment>(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void saveChanges()
        {
            _context.SaveChanges();
        }

        public async Task saveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
