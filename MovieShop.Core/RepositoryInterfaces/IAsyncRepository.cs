using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieShop.Core.RepositoryInterfaces
{
    //generic constraints
    public interface IAsyncRepository<T> where T : class
    {
        //CRUD
        //Reading
        Task<T> GetByIdAsync(int Id); // return one record under certain class on corresponding Id
        Task<IEnumerable<T>> ListAllAsync(); // return all records under certain class
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter); //filter: LINQ - where condition
        Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null); //filter=null means default value of filter is null
        Task<bool> GetExistingAsync(Expression<Func<T, bool>> filter = null);
        //Creating
        Task<T> AddAsync(T entity);
        //Updating
        Task<T> UpdateAsync(T entity);
        //Delete
        Task<T> DeleteAsync(T entity);


        ////CRUD

        //// C Creating
        //Task<T> AddAsync(T entity);

        ////R Reading
        //Task<T> GetByIdAsync(int id);

        //IEnumerable<T> ListAllAsync();
        //// LINQ list of movies on some where condition where m.title = "Avengere", m.revenue > 10000000
        //Task<IEnumerable<T>>  ListAsync(Expression<Func<T, bool>> filter);
        ////
        //Task<int> GetCountAsync(Expression<Func<T, bool>> filter = null); //if we dont pass in any condition, the default value is null
        ////    
        //Task<bool> GetExistsAsync(Expression<Func<T, bool>> filter = null);


        //// U Update
        //Task<T> UpdateAsync(T entity);

        //// D Delete
        //Task<T> DeleteAsync(T entity);
    }
}
