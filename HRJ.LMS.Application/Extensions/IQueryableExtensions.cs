using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HRJ.LMS.Application.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObj)
        {

            var pageSize = queryObj.PageSize ?? 10;
            var pageNo = queryObj.PageNo ?? 0;

            if (pageNo == -1) 
            {
                return query;
            }

            if(pageNo == 0) 
            {
                pageNo = 1;
            }
            
            if(pageSize <= 0) 
            {
                pageSize = 10;
            }

            return query.Skip((pageNo - 1) * pageSize).Take(pageSize);
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, IQueryObject queryObj, 
            Dictionary<string, Expression<Func<T, object>>> columnMaps)
        {
            if(string.IsNullOrWhiteSpace(queryObj.SortBy) || !columnMaps.ContainsKey(queryObj.SortBy))
            {
                return query;
            }

            if (queryObj.IsSortingAscending)
                return query.OrderBy(columnMaps[queryObj.SortBy]);
            else
                return query.OrderByDescending(columnMaps[queryObj.SortBy]);
        }

        public static IQueryable<T> ApplyFiltering<T, K>(this IQueryable<T> query, K queryParameter, Dictionary<string, Expression<Func<T, bool>>> columnMaps)
        {
            foreach(var key in columnMaps.Keys)
            {
                var Property = typeof(K).GetProperty(key, BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance);
                /* if(Property.GetType() == typeof(string))
                { */
                    if(Property.GetValue(queryParameter) != null 
                        && !String.IsNullOrWhiteSpace(Property.GetValue(queryParameter).ToString())
                        && Property.GetValue(queryParameter) != null)
                        query = query.Where(columnMaps[key]);
                /* } */

               /*  if(Property.GetType() == typeof(bool))
                {
                    if(Property.GetValue(queryParameter) != null)
                        query = query.Where(columnMaps[key]);
                } */
                
            }
            return query;
        }
    }
}