﻿using System.Text.Json.Serialization;

namespace MeuBolso.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(TData? data, int totalCount, int currentPage = 1, int pageSize = Configuration.DEFAULT_PAGE_SIZE)
            :base(data)
        {
            Data = data;
            CurrentPage = currentPage;
            PageSize = Configuration.DEFAULT_PAGE_SIZE;
            TotalCount = totalCount;
        }
        public PagedResponse(TData? data, int code = Configuration.DEFAULT_STATUS_CODE, string? message = null)
            :base(data, code, message)
        {
            
        }
        public int CurrentPage { get; set; }
        public int TotalPages => (int) Math.Ceiling(TotalCount / (double) PageSize);
        public int PageSize { get; set; } = Configuration.DEFAULT_PAGE_SIZE;
        public int TotalCount { get; set; }
    }
}
