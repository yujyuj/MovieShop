using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MovieShop.Core.RepositoryInterfaces
{
    //generic constraints
    public interface IAsyncRepository<T> where T : class
    {
        //CRUD

        // C Creating
        T AddAsync(T entity);

        //R Reading
        T GetByIdAsync(int id);
        IEnumerable<T> ListAllAsync();
        // LINQ list of movies on some where condition where m.title = "Avengere", m.revenue > 10000000
        IEnumerable<T> ListAsync(Expression<Func<T, bool>> filter);
        //
        int GetCountAsync(Expression<Func<T, bool>> filter = null); //if we dont pass in any condition, the default value is null
        //    
        bool GetExistsAsync(Expression<Func<T, bool>> filter = null);


        // U Update
        T UpdateAsync(T entity);

        // D Delete
        T DeleteAsync(T entity);
    }
}
