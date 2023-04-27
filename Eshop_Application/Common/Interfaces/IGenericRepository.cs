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

        Task<TClass?> GetById(int id);

        Task Create(TClass entity);

        Task Update(TClass entity);

        Task DeleteById(int id);
    }
}
