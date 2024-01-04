using System.Linq.Expressions;

namespace Yummi.UnitTest.Mock
{
    public class MockAsyncEnumerable<TEntity> : EnumerableQuery<TEntity>, IAsyncEnumerable<TEntity>, IQueryable<TEntity>
    {
        public MockAsyncEnumerable(IEnumerable<TEntity> enumerable)
            : base(enumerable)
        { }

        public MockAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            return new MockAsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new MockAsyncQueryProvider<TEntity>(this); }
        }
    }

}
