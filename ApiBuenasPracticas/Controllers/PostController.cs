using ApiBuenasPracticas.Response;
using AutoMapper;
using CoreBuenasPracticas.CustomEntities;
using CoreBuenasPracticas.DTOs;
using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Interfaces;
using CoreBuenasPracticas.QueryFilters;
using InfraestructureBuenasPracticas.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ApiBuenasPracticas.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public PostController(IPostService postService, IMapper mapper, IUriService uriService)
        {
            _postService = postService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Retrieve all posts
        /// </summary>
        /// <param name="filters">Filters to apply.</param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK,Type =typeof(ApiResponse<IEnumerable<PostDto>>))] //muestra en el swagger el modelo de respuesta, no solo muestra un 200, muestra el objeto como responde el api
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet(Name =nameof(GetPost))] //nombre del metodo, se usa para identificar un endpiont
        public IActionResult GetPosts([FromQuery] PostQueryFilter filters)
        {

            var posts = _postService.GetPosts(filters);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);

            var metadata = new Metadata
            {
                TotalCount = posts.TotalCount,
                PageSize = posts.PageSize,
                CurrentPage = posts.CurrentPage,
                TotalPages = posts.TotalPages,
                HasNextPage = posts.HasNextpage,
                HasPreviousPage = posts.HasPreviouspage,
                NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPost))).ToString(),
                PreviosPageUrl=_uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPost))).ToString()

            };
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto)
            {
                Meta= metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {

            var post = await _postService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            await _postService.InsertPost(post);
            postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;
            var result = await _postService.Updatepost(post);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.Deletepost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
