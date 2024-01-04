using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Yummi.UnitTest.Mock
{
    public class MockAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public MockAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new MockAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new MockAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new MockAsyncEnumerable<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            return _inner.Execute<TResult>(expression);
        }
        //public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        //{
        //    return Task.FromResult(Execute<TResult>(expression));
        //}       

    }
}
