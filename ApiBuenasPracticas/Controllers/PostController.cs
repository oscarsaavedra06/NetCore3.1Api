﻿using ApiBuenasPracticas.Response;
using AutoMapper;
using CoreBuenasPracticas.DTOs;
using CoreBuenasPracticas.Entities;
using CoreBuenasPracticas.Interfaces;
using CoreBuenasPracticas.QueryFilters;
using InfraestructureBuenasPracticas.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiBuenasPracticas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetPosts([FromQuery]PostQueryFilter filters)
        {

            var posts = _postService.GetPosts(filters);
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
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
