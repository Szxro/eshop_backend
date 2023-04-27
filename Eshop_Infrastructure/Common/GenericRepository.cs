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

        public async Task<TClass?> GetById(int id)
        {
            //FindAsync => is going to search by the id (must be tracking)
            TClass? entity = await _context.Set<TClass>().FindAsync(id);

            if (entity is null)
            {
                throw new NotFoundException($"the {typeof(TClass).Name} was not found");
            }

            return entity;
        }


        public async Task Create(TClass entity)
        {
            _context.Set<TClass>().Add(entity); 

            await _context.SaveChangesAsync(); 
        }

        public async Task Update(TClass entity)
        {
            //First need to verify the entity exist by the id 
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            TClass? entity = await GetById(id);

            _context.Set<TClass>().Remove(entity!);

            await _context.SaveChangesAsync();
        }

    }
}
