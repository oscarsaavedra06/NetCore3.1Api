using CoreBuenasPracticas.CustomEntities;
using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Exceptions;
using CoreBuenasPracticas.Interfaces;
using CoreBuenasPracticas.QueryFilters;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBuenasPracticas.Services
{
    public class PostService : IPostService
    {
        //Reglas de negocio, validaciones, en los repositorios solo va la intereaccion con la bd
        //En esta clase se determina si es valido ir al repositorio o no
        //Api => Servicios => Repositorio
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions;

        public PostService(IUnitOfWork unitOfWork, IOptions<PaginationOptions> options )
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }
        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public PagedList<Post> GetPosts(PostQueryFilter filters)
        {
            filters.PageNumber = filters.PageNumber == 0 ? _paginationOptions.DefaultPageNumber : filters.PageNumber;
            filters.PageSize = filters.PageSize == 0 ? _paginationOptions.DefaultPageSize : filters.PageSize;

            var post = _unitOfWork.PostRepository.GetAll();
            if (filters.UserId != null)
            {
                post = post.Where(x => x.UserId == filters.UserId);
            }
            
            if (filters.Date != null)
            {
                post = post.Where(x => x.Date.ToShortDateString() == filters.Date?.ToShortDateString());
            }

            if (filters.Description != null)
            {
                post = post.Where(x => x.Description.ToLower().Contains( filters.Description.ToLower()));
            }

            var pagedPost = PagedList<Post>.Create(post, filters.PageNumber, filters.PageSize);  

            return pagedPost;
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);
            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }

            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count() < 10)
            {
                var lastpost = userPost.OrderByDescending(x => x.Date).FirstOrDefault();
                if ((DateTime.Now - lastpost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish the post");
                }
            }

            if (post.Description.Contains("Sexo"))
            {
                throw new BusinessException("Content not allowed");
            }
            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.saveChangesAsync();
        }

        public async Task<bool> Updatepost(Post post)
        {
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.saveChangesAsync();
            return true;
        }
        public async Task<bool> Deletepost(int id)
        {
            await _unitOfWork.PostRepository.Delete(id);
            return true;
        }
    }
}
