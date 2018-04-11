using System;
namespace StudentManagerBackEnd.DataAccess
{
    public interface IRepository<T>
    {
        T Create(T item);

        T Get(Guid id);

        PaginatedResult<T> Get(int page, int size);
    }
}
