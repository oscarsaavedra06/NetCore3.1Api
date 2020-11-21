using CoreBuenasPracticas.QueryFilters;
using System;

namespace InfraestructureBuenasPracticas.Interfaces
{
    public interface IUriService
    {
        Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl);
    }
}