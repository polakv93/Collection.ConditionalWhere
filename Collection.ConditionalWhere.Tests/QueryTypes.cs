using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Collection.ConditionalWhere.Tests
{
    public class QueryTypes
    {
        [Fact]
        public void Applied_on_simple_string()
        {
            var query = CreateQuery();
            var value = "string";

            var res = query.ConditionalWhere(value, q => q.String == value).ToList();

            res.Should().HaveCount(1);
            res.First().Id.Should().Be(2);
        }

        [Fact]
        public void Applied_on_list()
        {
            var query = CreateQuery();
            var value = new List<int> { 2 };

            var res = query.ConditionalWhere(value, q => value.Contains(q.Id));

            res.Should().HaveCount(1);
            res.First().Id.Should().Be(2);
        }

        [Fact]
        public void Not_applied_on_empty_list()
        {
            var query = CreateQuery();
            var value = new List<int>();

            var res = query.ConditionalWhere(value, q => q.Bool);

            res.Should().HaveCount(2);
        }

        [Fact]
        public void Not_applied_on_empty_ilist()
        {
            var query = CreateQuery();
            IList<int> value = new List<int>();

            var res = query.ConditionalWhere(value, q => q.Bool);

            res.Should().HaveCount(2);
        }

        [Fact]
        public void Not_applied_on_empty_array()
        {
            var query = CreateQuery();
            var value = new List<int>().ToArray();

            var res = query.ConditionalWhere(value, q => q.Bool);

            res.Should().HaveCount(2);
        }

        [Fact]
        public void Not_applied_on_empty_enumerable()
        {
            var query = CreateQuery();
            var value = new List<int>().AsEnumerable();

            var res = query.ConditionalWhere(value, q => q.Bool);

            res.Should().HaveCount(2);
        }

        [Fact]
        public void Applied_on_enumerable()
        {
            var query = CreateQuery();
            IEnumerable<int> value = new List<int> { 2 };

            var res = query.ConditionalWhere(value, q => value.Contains(q.Id));

            res.Should().HaveCount(1);
            res.First().Id.Should().Be(2);

        }

        private static IQueryable<QueryItem> CreateQuery()
        {
            var query = new List<QueryItem>
            {
                new QueryItem { Id = 1 },
                new QueryItem { Id = 2, Bool = true, String = "string" },
            }.AsQueryable();
            return query;
        }
    }

    internal class QueryItem    
    {
        public int Id { get; set; }
        public string String { get; set; }
        public bool Bool { get; set; }
    }
}
