using AutoMapper;
using CoreBuenasPracticas.DTOs;
using CoreBuenasPracticas.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InfraestructureBuenasPracticas.Mappings
{
    public class AutomapperProfile : Profile
    {

        public AutomapperProfile()
        {
            CreateMap<Post,PostDto>();
            CreateMap<PostDto,Post>();
        }
    }
}
