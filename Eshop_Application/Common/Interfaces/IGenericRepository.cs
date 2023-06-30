using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Common.Interfaces
{
    public interface IGenericRepository<TClass> where TClass : class
    {
        Task<IReadOnlyCollection<TClass>> GetAll();

        Task<TClass?> GetEntityById(int id);

        Task CreateEntity(TClass entity);

        Task UpdateEntity(TClass entity);

        Task DeleteEntityById(int id);

        Task InsertMany(List<TClass> entity);
    }
}
