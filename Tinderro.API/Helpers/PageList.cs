using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tinderro.API.Helpers
{
    public class PageList<T> : List<T> // zamiast PageList<User> dajemy ogolnie T zeby ta klasa mogla byc uniwersalna!
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; } // liczba uzytkownikow na strone
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public PageList(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount/(double)pageSize);
            this.AddRange(items); // dodajemy wszystkie elemnty
        }

        public static async Task<PageList<T>> CreateListAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(); // Skip - pomijamy iles uzytkownikow, Take - bierzemy iles kolejnych elementow i dajemy do listy
            return new PageList<T>(items, totalCount, pageNumber, pageSize);
        }   
    }
}