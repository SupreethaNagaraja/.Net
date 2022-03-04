using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Search.Interfaces;
using Search.Models;

namespace Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {   
            this._searchService = searchService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm searchTerm)
        {
            var result = await _searchService.SearchAsync(searchTerm.CustomerId);
            if(result.IsSuccess)
            {
                return Ok(result.SearchResults);
            }
            return NotFound();
        }
        
    }
}
