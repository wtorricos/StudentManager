using System;
namespace StudentManagerBackEnd.DataAccess
{
    public interface IRepository<T>
    {
        T Create(T item);

        //TODO: It will be better to implement or use a third party library that allows to use Maybe<T>
        T Get(Guid id);

        void Delete(Guid id);

        PaginatedResult<T> Get(int page, int size);
    }
}
