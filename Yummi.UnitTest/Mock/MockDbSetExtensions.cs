using Microsoft.EntityFrameworkCore;
using Moq;

namespace Yummi.UnitTest.Mock
{
    public static class MockDbSetExtensions
    {
        public static Mock<DbSet<T>> ReturnsDbSet<T>(this Mock<DbSet<T>> mockSet, IQueryable<T> entities) where T : class
        {
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => entities.GetEnumerator());

            return mockSet;
        }
    }
}
