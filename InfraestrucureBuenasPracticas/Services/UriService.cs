﻿using CoreBuenasPracticas.QueryFilters;
using InfraestructureBuenasPracticas.Interfaces;
using System;

namespace InfraestructureBuenasPracticas.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;
        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }


        public Uri GetPostPaginationUri(PostQueryFilter filter, string actionUrl)
        {
            string baseUrl = $"{_baseUri}{actionUrl}";

            return new Uri(baseUrl);

        }
    }
}
