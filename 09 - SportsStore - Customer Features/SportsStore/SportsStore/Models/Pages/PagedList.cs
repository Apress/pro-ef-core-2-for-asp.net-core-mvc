using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Diagnostics;

namespace SportsStore.Models.Pages {

    public class PagedList<T> : List<T> {

        public PagedList(IQueryable<T> query, QueryOptions options = null) {
            CurrentPage = options.CurrentPage;
            PageSize = options.PageSize;
            Options = options;

            if (options != null) {
                if (!string.IsNullOrEmpty(options.OrderPropertyName)) {
                    query = Order(query, options.OrderPropertyName, 
                        options.DescendingOrder);
                }
                if (!string.IsNullOrEmpty(options.SearchPropertyName) 
                        && !string.IsNullOrEmpty(options.SearchTerm)) {
                    query = Search(query, options.SearchPropertyName, 
                        options.SearchTerm);
                }
            }

            //Stopwatch sw = Stopwatch.StartNew();
            //Console.Clear();

            TotalPages = query.Count() / PageSize;
            AddRange(query.Skip((CurrentPage - 1) * PageSize).Take(PageSize));

            //Console.WriteLine($"Query Time: {sw.ElapsedMilliseconds} ms");
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public QueryOptions Options { get; set; }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        private static IQueryable<T> Search(IQueryable<T> query, string propertyName, 
                string searchTerm) {
            var parameter = Expression.Parameter(typeof(T), "x");
            var source = propertyName.Split('.').Aggregate((Expression) parameter, 
                Expression.Property);
            var body = Expression.Call(source, "Contains", Type.EmptyTypes, 
                Expression.Constant(searchTerm, typeof(string)));
            var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
            return query.Where(lambda);
        }

        private static IQueryable<T> Order(IQueryable<T> query, string propertyName, 
                bool desc) {
            var parameter = Expression.Parameter(typeof(T), "x");
            var source = propertyName.Split('.').Aggregate((Expression) parameter, 
                Expression.Property);
            var lambda = Expression.Lambda(typeof(Func<,>).MakeGenericType(typeof(T), 
                source.Type), source, parameter);
            return typeof(Queryable).GetMethods().Single(
                      method => method.Name == (desc ? "OrderByDescending" 
                                  : "OrderBy")
                      && method.IsGenericMethodDefinition
                      && method.GetGenericArguments().Length == 2
                      && method.GetParameters().Length == 2)
                  .MakeGenericMethod(typeof(T), source.Type)
                  .Invoke(null, new object[] { query, lambda }) as IQueryable<T>;
        }
    }
}
