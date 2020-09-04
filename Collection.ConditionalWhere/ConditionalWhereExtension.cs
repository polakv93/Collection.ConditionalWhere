using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Collection.ConditionalWhere
{
    public static class ConditionalWhereExtension
    {
        public static IQueryable<T> ConditionalWhere<T>(this IQueryable<T> query, string value, Expression<Func<T, bool>> predicate)
            => !string.IsNullOrWhiteSpace(value) ? query.Where(predicate) : query;

        public static IQueryable<T> ConditionalWhere<T, TK>(this IQueryable<T> query, TK value, Expression<Func<T, bool>> predicate)
            => EqualityComparer<TK>.Default.Equals(value, default)
                ? query
                : query.Where(predicate);

        public static IQueryable<T> ConditionalWhere<T, TK>(this IQueryable<T> query, IEnumerable<TK> value, Expression<Func<T, bool>> predicate)
            => value != null && value.Any() ? query.Where(predicate) : query;

        public static IQueryable<T> ConditionalWhere<T, TK>(this IQueryable<T> query, List<TK> value, Expression<Func<T, bool>> predicate)
            => value != null && value.Any() ? query.Where(predicate) : query;

        public static IQueryable<T> ConditionalWhere<T, TK>(this IQueryable<T> query, TK[] value, Expression<Func<T, bool>> predicate)
            => value != null && value.Any() ? query.Where(predicate) : query;

        public static IQueryable<T> ConditionalWhere<T, TK>(this IQueryable<T> query, IList<TK> value, Expression<Func<T, bool>> predicate)
            => value != null && value.Any() ? query.Where(predicate) : query;
    }
}
