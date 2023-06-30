using Eshop_Application.Common.Exceptions;
using Eshop_Application.Common.Interfaces;
using Eshop_Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Infrastructure.Common
{
    public class GenericRepository<TClass> : IGenericRepository<TClass> where TClass : class
    {
        protected readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IReadOnlyCollection<TClass>> GetAll() 
        {
            return await _context.Set<TClass>().AsNoTracking().ToListAsync();
        }

        public async Task<TClass?> GetEntityById(int id)
        {
            //FindAsync => is going to search by the id (must be tracking)
            TClass? entity = await _context.Set<TClass>().FindAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"the {typeof(TClass).Name} was not found");
            }

            return entity;
        }


        public async Task CreateEntity(TClass entity)
        {
            _context.Set<TClass>().Add(entity); 

            await _context.SaveChangesAsync(); 
        }

      

        public async Task UpdateEntity(TClass entity)
        {
            //First need to verify the entity exist by the id 
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityById(int id)
        {
            TClass? entity = await GetEntityById(id);

            _context.Set<TClass>().Remove(entity!);

            await _context.SaveChangesAsync();
        }

        public async Task InsertMany(List<TClass> entity)
        {
            _context.Set<TClass>().AddRange(entity);

            await _context.SaveChangesAsync();
        }
    }
}
