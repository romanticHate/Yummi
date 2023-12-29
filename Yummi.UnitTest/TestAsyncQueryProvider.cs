using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Yummi.UnitTest
{
    public class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        public TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
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
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
        {
            return _inner.Execute<TResult>(expression);
        }

        //public ValueTask<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        //{
        //    return new ValueTask<TResult>(_inner.Execute<TResult>(expression));
        //}
       
    }
}
